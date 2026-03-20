using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace AutomationApp.UiTests.Pages
{
    public class CheckoutPage : BasePage
    {
        private ILocator AddressDetailsHeader => _page.GetByRole(AriaRole.Heading, new() { Name = "Address Details" });
        private ILocator DeliveryAddressHeader => _page.GetByRole(AriaRole.Heading, new() { Name = "Your delivery address" });
        private ILocator BillingAddressHeader => _page.GetByRole(AriaRole.Heading, new() { Name = "Your billing address" });
        private ILocator ReviewOrderHeader => _page.GetByRole(AriaRole.Heading, new() { Name = "Review Your Order" });
        private ILocator CartInfoTable => _page.Locator("#cart_info");
        private ILocator CommentTextArea => _page.Locator("textarea[name='message']");
        private ILocator PlaceOrderButton => _page.GetByRole(AriaRole.Link, new() { Name = "Place Order" });

        public CheckoutPage(IPage page) : base(page)
        {
        }

        public async Task EnterComment(string comment)
        {
            await CommentTextArea.FillAsync(comment);
        }

        public async Task PlaceOrder()
        {
            await PlaceOrderButton.ClickAsync();
        }

        public async Task VerifyIsAtCheckoutPage()
        {
            await Expect(_page).ToHaveURLAsync("/checkout");
            await Expect(AddressDetailsHeader).ToBeVisibleAsync();
            await Expect(DeliveryAddressHeader).ToBeVisibleAsync();
            await Expect(BillingAddressHeader).ToBeVisibleAsync();
            await Expect(ReviewOrderHeader).ToBeVisibleAsync();
            await Expect(CartInfoTable).ToBeVisibleAsync();
            await Expect(CommentTextArea).ToBeVisibleAsync();
            await Expect(PlaceOrderButton).ToBeVisibleAsync();
        }
    }
}
