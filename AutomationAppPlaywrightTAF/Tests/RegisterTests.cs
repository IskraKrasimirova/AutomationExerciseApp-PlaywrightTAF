using AutomationApp.UiTests.Models;
using AutomationApp.UiTests.Models.Factories;
using AutomationApp.UiTests.Pages;

namespace AutomationApp.UiTests.Tests
{
    [Category("Register")]
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
        [Category("Smoke")]
        public async Task UserCanRegisterSuccessfully()
        {
            var newUser = UserFactory.CreateDefault();

            await RegisterUser(newUser);

            await DeleteUserAccount();
        }

        [Test]
        [Category("E2E")]
        public async Task NewlyRegisteredUserCanLoginSuccessfully()
        {
            var user = UserFactory.CreateDefault();
            await RegisterUser(user);
            await _homePage.NavBar.Logout();

            await _loginPage.VerifyIsAtLoginPage();
            await _loginPage.Login(user.Email, user.Password);

            await _homePage.VerifyIsAtHomePage();
            await _homePage.NavBar.VerifyUserIsLoggedIn(user.Name);

            await DeleteUserAccount();
        }

        [Test]
        [Category("E2E")]
        public async Task UserCannotRegisterWithExistingEmail()
        {
            var user = UserFactory.CreateDefault();
            await RegisterUser(user);
            await _homePage.NavBar.Logout();

            await _loginPage.VerifyIsAtLoginPage();
            await _loginPage.Signup(user.Name, user.Email);
            await _loginPage.VerifyEmailAlreadyExistsError();

            await _homePage.NavBar.GoToLoginPage();
            await _loginPage.VerifyIsAtLoginPage();
            await _loginPage.Login(user.Email, user.Password);
            await _homePage.VerifyIsAtHomePage();
            await _homePage.NavBar.VerifyUserIsLoggedIn(user.Name);

            await DeleteUserAccount();
        }

        private async Task RegisterUser(UserModel user)
        {
            await _homePage.VerifyIsAtHomePage();
            await _homePage.NavBar.GoToLoginPage();

            await _loginPage.VerifyIsAtLoginPage();
            await _loginPage.Signup(user.Name, user.Email);

            await _signupPage.VerifyIsAtSignupPage(user.Name, user.Email);
            await _signupPage.CreateAccount(user);

            await _accountCreatedPage.VerifyAccountCreated();
            await _accountCreatedPage.ClickContinue();

            await _homePage.VerifyIsAtHomePage();
            await _homePage.NavBar.VerifyUserIsLoggedIn(user.Name);
        }

        private async Task DeleteUserAccount()
        {
            await _homePage.NavBar.DeleteAccount();
            await _accountDeletedPage.VerifyAccountDeleted();
            await _accountDeletedPage.ClickContinue();
            await _homePage.VerifyIsAtHomePage();
        }
    }
}
