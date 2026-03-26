using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace AutomationApp.UiTests.Pages
{
    public class CartModal : BasePage
    {
        private ILocator Modal => _page.Locator("#cartModal");
        private ILocator ModalHeader => Modal.GetByRole(AriaRole.Heading, new() { Name = "Added!" });
        private ILocator ContinueShoppingButton => Modal.GetByRole(AriaRole.Button, new() { Name = "Continue Shopping" });
        private ILocator ViewCartLink => Modal.GetByRole(AriaRole.Link, new() { Name = "View Cart" });

        public CartModal(IPage page) : base(page)
        {
        }

        public async Task ContinueShopping()
        {
            await ContinueShoppingButton.ClickAsync();
        }

        public async Task ViewCart()
        {
            await ViewCartLink.ClickAsync();
        }

        public async Task VerifyIsVisible()
        {
            await Expect(Modal).ToBeVisibleAsync();
            await Expect(ModalHeader).ToBeVisibleAsync();
        }
    }
}
