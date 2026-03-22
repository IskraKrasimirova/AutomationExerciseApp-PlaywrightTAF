using AutomationApp.UiTests.Utilities;
using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace AutomationApp.UiTests.Pages
{
    public class OrderConfirmationPage : BasePage
    {
        private ILocator OrderHeader => _page.GetByRole(AriaRole.Heading, new() { Name = "Order Placed!" });
        private ILocator ConfirmationMessage => _page.GetByText("Congratulations! Your order has been confirmed!");
        private ILocator DownloadInvoiceButton => _page.GetByRole(AriaRole.Link, new() { Name = "Download Invoice" });
        private ILocator ContinueButton => _page.GetByRole(AriaRole.Link, new() { Name = "Continue" });

        public OrderConfirmationPage(IPage page) : base(page)
        {
        }

        public async Task DownloadInvoce()
        {
            await DownloadInvoiceButton.ClickAsync();
        }

        public async Task ClickContinue()
        {
            await ContinueButton.ClickAsync();
        }

        public async Task VerifyIsAtOrderConfirmationPage()
        {
            await Expect(_page).ToHaveURLAsync(new System.Text.RegularExpressions.Regex($"{UiConstants.OrderConfirmationUrl}\\d+"));
            await Expect(OrderHeader).ToBeVisibleAsync();
            await Expect(ConfirmationMessage).ToBeVisibleAsync();
            await Expect(DownloadInvoiceButton).ToBeVisibleAsync();
            await Expect(ContinueButton).ToBeVisibleAsync();
        }
    }
}
