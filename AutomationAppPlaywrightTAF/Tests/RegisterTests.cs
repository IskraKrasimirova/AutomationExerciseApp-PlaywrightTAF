using AutomationApp.UiTests.Models.Factories;
using AutomationApp.UiTests.Pages;

namespace AutomationApp.UiTests.Tests
{
    public class RegisterTests : BaseTest
    {
        private HomePage _homePage;
        private LoginPage _loginPage;
        private SignupPage _signupPage;
        private AccountCreatedPage _accountCreatedPage;
        private AccountDeletedPage _accountDeletedPage;

        [SetUp]
        public async Task TestSetUp()
        {
            _homePage = new HomePage(Page);
            _loginPage = new LoginPage(Page);
            _signupPage = new SignupPage(Page);
            _accountCreatedPage = new AccountCreatedPage(Page);
            _accountDeletedPage = new AccountDeletedPage(Page);
            await Page.GotoAsync("/");
            await _homePage.AcceptCookiesIfPresent();
        }

        [Test]
        public async Task UserCanRegisterSuccessfully()
        {
            var newUser = UserFactory.CreateDefault();

            await _homePage.VerifyIsAtHomePage();
            await _homePage.NavBar.GoToLoginPage();

            await _loginPage.VerifyIsAtLoginPage();
            await _loginPage.Signup(newUser.Name, newUser.Email);

            await _signupPage.VerifyIsAtSignupPage(newUser.Name, newUser.Email);
            await _signupPage.CreateAccount(newUser);

            await _accountCreatedPage.VerifyAccountCreated();
            await _accountCreatedPage.ClickContinue();

            await _homePage.VerifyIsAtHomePage();
            await _homePage.NavBar.VerifyUserIsLoggedIn(newUser.Name);

            await _homePage.NavBar.DeleteAccount();
            await _accountDeletedPage.VerifyAccountDeleted();
            await _accountDeletedPage.ClickContinue();

            await _homePage.VerifyIsAtHomePage();
        }
    }
}
