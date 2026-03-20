using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace AutomationApp.UiTests.Pages
{
    public class CheckoutModal : BasePage
    {
        private ILocator Modal => _page.Locator("#checkoutModal");
        private ILocator ModalHeader => Modal.GetByRole(AriaRole.Heading, new() { Name = "Checkout" });
        private ILocator RegisterLoginLink => Modal.GetByRole(AriaRole.Link, new() { Name = "Register / Login" });
        private ILocator ContinueOnCartButton => Modal.GetByRole(AriaRole.Button, new() { Name = "Continue On Cart" });

        public CheckoutModal(IPage page) : base(page)
        {
        }

        public async Task ClickRegisterLogin()
        {
            await RegisterLoginLink.ClickAsync();
        }

        public async Task ContinueOnCart()
        {
            await ContinueOnCartButton.ClickAsync();
        }

        public async Task VerifyIsVisible()
        {
            await Expect(Modal).ToBeVisibleAsync();
            await Expect(ModalHeader).ToBeVisibleAsync();
        }
    }
}
