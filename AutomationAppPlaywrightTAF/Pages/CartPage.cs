using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace AutomationApp.UiTests.Pages
{
    public class CartPage : BasePage
    {
        private ILocator CartSection => _page.Locator("#cart_items");
        private ILocator HomeLink => CartSection.GetByRole(AriaRole.Link, new() { Name = "Home" });
        private ILocator ShoppingCartBreadcrumb => CartSection.GetByText("Shopping Cart");
        private ILocator CartTable => CartSection.Locator("#cart_info_table");
        private ILocator CartRows => CartTable.Locator("tbody tr");
        private ILocator ProceedToCheckoutButton => CartSection.GetByRole(AriaRole.Link, new() { Name = "Proceed To Checkout" });

        private ILocator ProductRow(int index) => CartRows.Nth(index);
        private ILocator ProductName(int index) => ProductRow(index).Locator(".cart_description h4");
        private ILocator ProductPrice(int index) => ProductRow(index).Locator(".cart_price p");
        private ILocator ProductQuantity(int index) => ProductRow(index).Locator(".cart_quantity button");
        private ILocator ProductTotalPrice(int index) => ProductRow(index).Locator(".cart_total p");
        private ILocator DeleteProductButton(int index) => ProductRow(index).Locator(".cart_delete a");
        private ILocator EmptyCartMessage => CartSection.Locator("#empty_cart");
        private ILocator LinkToProductsPage => CartSection.GetByRole(AriaRole.Link, new() { Name = "here" });

        public CartPage(IPage page) : base(page)
        {
        }

        public async Task NavigateToProductsPage()
        {
            await LinkToProductsPage.ClickAsync();
        }

        public async Task ProceedToCheckout()
        {
            await ProceedToCheckoutButton.ClickAsync();
        }

        public async Task VerifyIsAtCartPage()
        {
            await Expect(_page).ToHaveURLAsync("/view_cart");
            await Expect(HomeLink).ToBeVisibleAsync();
            await Expect(ShoppingCartBreadcrumb).ToBeVisibleAsync();
        }

        public async Task VerifyProductsCount(int expectedCount)
        {
            await Expect(CartRows).ToHaveCountAsync(expectedCount);
        }

        public async Task VerifyProductInCart(int rowIndex, string expectedName, string expectedPrice, string expectedQuantity, string expectedTotalPrice)
        {
            await Expect(ProductName(rowIndex)).ToContainTextAsync(expectedName);
            await Expect(ProductPrice(rowIndex)).ToContainTextAsync(expectedPrice);
            await Expect(ProductQuantity(rowIndex)).ToContainTextAsync(expectedQuantity);
            await Expect(ProductTotalPrice(rowIndex)).ToContainTextAsync(expectedTotalPrice);
            await Expect(DeleteProductButton(rowIndex)).ToBeVisibleAsync();
        }

        public async Task VerifyCartIsEmpty()
        {
            await Expect(EmptyCartMessage).ToBeVisibleAsync();
            await Expect(CartTable).ToBeHiddenAsync();
        }
    }
}
