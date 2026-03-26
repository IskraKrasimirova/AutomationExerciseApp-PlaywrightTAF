using Allure.NUnit.Attributes;
using AutomationApp.UiTests.Models;
using AutomationApp.UiTests.Models.Factories;
using AutomationApp.UiTests.Pages;

namespace AutomationApp.UiTests.Tests
{

    [AllureSuite("UI Tests")]
    [AllureSubSuite("Login")]
    [Category("Login")]
    public class LoginTests: BaseTest
    {
        private HomePage _homePage;
        private LoginPage _loginPage;
        private SignupPage _signupPage;
        private AccountCreatedPage _accountCreatedPage;
        private AccountDeletedPage _accountDeletedPage;
        private UserModel _registeredUser;

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

            _registeredUser = UserFactory.CreateDefault();
            await RegisterUser(_registeredUser);
            await _homePage.NavBar.Logout();
            await _loginPage.VerifyIsAtLoginPage();
        }

        [TearDown]
        public async Task TestTearDown()
        {
            if (await _homePage.NavBar.IsUserLoggedIn())
            {
                await _homePage.NavBar.Logout();
                await _loginPage.VerifyIsAtLoginPage();
            }   
               
            await _loginPage.Login(_registeredUser.Email, _registeredUser.Password);
            await _homePage.VerifyIsAtHomePage();
            await _homePage.NavBar.VerifyUserIsLoggedIn(_registeredUser.Name);
            await _homePage.NavBar.DeleteAccount();
            await _accountDeletedPage.VerifyAccountDeleted();
            await _accountDeletedPage.ClickContinue();
            await _homePage.VerifyIsAtHomePage();
        }

        [Test]
        [Category("Smoke")]
        public async Task UserCanLoginSuccessfully()
        {
            await _loginPage.Login(_registeredUser.Email, _registeredUser.Password);
            await _homePage.VerifyIsAtHomePage();
            await _homePage.NavBar.VerifyUserIsLoggedIn(_registeredUser.Name);
        }

        [Test]
        [Category("Smoke")]
        public async Task UserCannotLoginWithWrongPassword()
        {
            await _loginPage.Login(_registeredUser.Email, "wrongpassword");
            await _loginPage.VerifyInvalidCredentialsError();
        }

        [Test]
        [Category("Smoke")]
        public async Task UserCannotLoginWithNonExistingEmail()
        {
            await _loginPage.Login("nonexisting@email.com", "somepassword");
            await _loginPage.VerifyInvalidCredentialsError();
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
    }
}
