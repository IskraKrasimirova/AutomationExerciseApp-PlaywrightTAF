using AutomationApp.UiTests.Utilities;
using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace AutomationApp.UiTests.Pages
{
    public class AccountCreatedPage : BasePage
    {
        private ILocator AccountCreatedHeader => _page.GetByRole(AriaRole.Heading, new() { Name = "Account Created!" });
        private ILocator GreetingMessage => _page.GetByText("Congratulations! Your new account has been successfully created!");
        private ILocator ContinueButton => _page.Locator("[data-qa='continue-button']");

        public AccountCreatedPage(IPage page) : base(page)
        {
        }

        public async Task ClickContinue() => await ContinueButton.ClickAsync();

        public async Task VerifyAccountCreated()
        {
            await Expect(_page).ToHaveURLAsync(UiConstants.AccountCreatedUrl);
            await Expect(AccountCreatedHeader).ToBeVisibleAsync();
            await Expect(GreetingMessage).ToBeVisibleAsync();
            await Expect(ContinueButton).ToBeVisibleAsync();
        }
    }
}
