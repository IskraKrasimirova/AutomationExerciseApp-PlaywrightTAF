using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace AutomationApp.UiTests.Pages
{
    public class AccountDeletedPage : BasePage
    {
        private ILocator AccountDeletedHeader => _page.GetByRole(AriaRole.Heading, new() { Name = "Account Deleted!" });
        private ILocator DeletingMessage => _page.GetByText("Your account has been permanently deleted!");
        private ILocator ContinueButton => _page.Locator("[data-qa='continue-button']");

        public AccountDeletedPage(IPage page) : base(page)
        {
        }

        public async Task ClickContinue() => await ContinueButton.ClickAsync();

        public async Task VerifyAccountDeleted()
        {
            await Expect(_page).ToHaveURLAsync("/delete_account");
            await Expect(AccountDeletedHeader).ToBeVisibleAsync();
            await Expect(DeletingMessage).ToBeVisibleAsync();
            await Expect(ContinueButton).ToBeVisibleAsync();
        }
    }
}
