using AutomationApp.UiTests.Models;
using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace AutomationApp.UiTests.Pages
{
    public class PaymentPage : BasePage
    {
        private ILocator PaymentHeader => _page.GetByRole(AriaRole.Heading, new() { Name = "Payment" });
        private ILocator PaymentForm => _page.Locator(".payment-information");
        private ILocator NameOnCardInput => PaymentForm.Locator("[data-qa='name-on-card']");
        private ILocator CardNumberInput => PaymentForm.Locator("[data-qa='card-number']");
        private ILocator CvcInput => PaymentForm.Locator("[data-qa='cvc']");
        private ILocator ExpirationMonthInput => PaymentForm.Locator("[data-qa='expiry-month']");
        private ILocator ExpirationYearInput => PaymentForm.Locator("[data-qa='expiry-year']");
        private ILocator PayAndConfirmButton => PaymentForm.GetByRole(AriaRole.Button, new() { Name = "Pay and Confirm Order" });


        public PaymentPage(IPage page) : base(page)
        {
        }

        public async Task EnterPaymentDetailsAndConfirm(PaymentModel payment)
        {
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await NameOnCardInput.FillAsync(payment.NameOnCard);
            await CardNumberInput.FillAsync(payment.CardNumber);
            await CvcInput.FillAsync(payment.Cvc);
            await ExpirationMonthInput.FillAsync(payment.ExpirationMonth);
            await ExpirationYearInput.FillAsync(payment.ExpirationYear);

            await PayAndConfirmButton.ClickAsync();
        }

        public async Task VerifyIsAtPaymentPage()
        {
            await Expect(_page).ToHaveURLAsync("/payment");
            await Expect(PaymentHeader).ToBeVisibleAsync();
            await Expect(PaymentForm).ToBeVisibleAsync();
        }
    }
}
