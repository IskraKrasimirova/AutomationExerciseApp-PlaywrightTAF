using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace AutomationApp.UiTests.Pages
{
    public class LoginPage : BasePage
    {
        private ILocator LoginForm => _page.Locator(".login-form");
        private ILocator LoginHeader => LoginForm.GetByRole(AriaRole.Heading, new() { Name = "Login to your account" });
        private ILocator EmailInput => LoginForm.GetByPlaceholder("Email Address");
        private ILocator PasswordInput => LoginForm.GetByPlaceholder("Password");
        private ILocator LoginButton => LoginForm.GetByRole(AriaRole.Button, new() { Name = "Login" });

        private ILocator SignupForm => _page.Locator(".signup-form");
        private ILocator SignupHeader => SignupForm.GetByRole(AriaRole.Heading, new() { Name = "New User Signup!" });
        private ILocator NameInput => SignupForm.GetByPlaceholder("Name");
        private ILocator SignupEmailInput => SignupForm.GetByPlaceholder("Email Address");
        private ILocator SignupButton => SignupForm.GetByRole(AriaRole.Button, new() { Name = "Signup" });
        private ILocator ExistingEmailErrorMessage => SignupForm.GetByText("Email Address already exist!");
        private ILocator InvalidCredentialsErrorMessage => LoginForm.GetByText("Your email or password is incorrect!");

        public LoginPage(IPage page) : base(page)
        {
        }

        public async Task Login(string email, string password)
        {
            await EmailInput.FillAsync(email);
            await PasswordInput.FillAsync(password);
            await LoginButton.ClickAsync();
        }

        public async Task Signup(string name, string email)
        {
            await NameInput.FillAsync(name);
            await SignupEmailInput.FillAsync(email);
            await SignupButton.ClickAsync();
        }

        public async Task VerifyLoginFormIsDisplayed()
        {
            await Expect(LoginForm).ToBeVisibleAsync();
        }

        public async Task VerifySignupFormIsDisplayed()
        {
            await Expect(SignupForm).ToBeVisibleAsync();
        }

        public async Task VerifyIsAtLoginPage()
        {
            await Expect(_page).ToHaveURLAsync("/login");
            await Expect(LoginHeader).ToBeVisibleAsync();
            await Expect(SignupHeader).ToBeVisibleAsync();
            //await VerifyLoginFormIsDisplayed();
            //await VerifySignupFormIsDisplayed();
        }

        public async Task VerifyEmailAlreadyExistsError()
        {
            await Expect(ExistingEmailErrorMessage).ToBeVisibleAsync();
        }

        public async Task VerifyInvalidCredentialsError()
        {
            await Expect(InvalidCredentialsErrorMessage).ToBeVisibleAsync();
        }
    }
}
