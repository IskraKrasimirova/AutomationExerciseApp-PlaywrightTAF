using Allure.NUnit.Attributes;
using AutomationApp.UiTests.Pages;

namespace AutomationApp.UiTests.Tests
{
    [AllureSuite("UI Tests")]
    [AllureSubSuite("Cart")]
    [Category("Cart")]
    public class CartTests : BaseTest
    {
        private HomePage _homePage;
        private ProductsPage _productsPage;
        private ProductDetailsPage _productDetailsPage;
        private CartModal _cartModal;
        private CartPage _cartPage;

        [SetUp]
        public async Task TestSetUp()
        {
            _homePage = new HomePage(Page);
            _productsPage = new ProductsPage(Page);
            _productDetailsPage = new ProductDetailsPage(Page);
            _cartModal = new CartModal(Page);
            _cartPage = new CartPage(Page);

            await Page.GotoAsync("/");
            await _homePage.AcceptCookiesIfPresent();
        }

        [Test]
        [Category("Smoke")]
        public async Task AddTwoProductsToCart_ShowsCorrectProductDetails()
        {
            await _homePage.VerifyIsAtHomePage();
            await _homePage.NavBar.GoToProductsPage();
            await _productsPage.VerifyIsAtProductsPage();

            var firstProductName = await _productsPage.GetFirstProductName();
            var firstProductPrice = await _productsPage.GetFirstProductPrice();

            await _productsPage.HoverAndAddToCart(0);
            await _cartModal.VerifyIsVisible();
            await _cartModal.ContinueShopping();

            await _productsPage.VerifyIsAtProductsPage();

            var secondProductName = await _productsPage.GetProductName(1);
            var secondProductPrice = await _productsPage.GetProductPrice(1);

            await _productsPage.HoverAndAddToCart(1);
            await _cartModal.VerifyIsVisible();
            await _cartModal.ViewCart();

            await _cartPage.VerifyIsAtCartPage();
            await _cartPage.VerifyProductsCount(2);
            await _cartPage.VerifyProductInCart(0, firstProductName, firstProductPrice, "1", firstProductPrice);
            await _cartPage.VerifyProductInCart(1, secondProductName, secondProductPrice, "1", secondProductPrice);
        }

        [Test]
        public async Task NavigateToCart_WithNoProducts_ShowsEmptyCartAndLinkToProducts()
        {
            await _homePage.VerifyIsAtHomePage();
            await _homePage.NavBar.GoToCartPage();
            await _cartPage.VerifyIsAtCartPage();
            await _cartPage.VerifyCartIsEmpty();
            await _cartPage.NavigateToProductsPage();
            await _productsPage.VerifyIsAtProductsPage();
        }

        [Test]
        public async Task AddProductToCart_WithCustomQuantity_ShowsCorrectQuantityAndTotalPriceInCart()
        {
            await _homePage.VerifyIsAtHomePage();
            await _homePage.NavBar.GoToProductsPage();
            await _productsPage.VerifyIsAtProductsPage();

            var firstProductName = await _productsPage.GetFirstProductName();
            var firstProductPrice = await _productsPage.GetFirstProductPrice();

            await _productsPage.ClickViewFirstProduct();
            await _productDetailsPage.VerifyIsAtProductDetailsPage();

            var quantity = new Random().Next(2, 10);
            await _productDetailsPage.SetQuantity(quantity);
            await _productDetailsPage.AddToCart();

            await _cartModal.VerifyIsVisible();
            await _cartModal.ViewCart();
            await _cartPage.VerifyIsAtCartPage();
            await _cartPage.VerifyProductsCount(1);

            var singlePrice = decimal.Parse(firstProductPrice.Replace("Rs.", "").Trim());
            var expectedTotalPrice = $"Rs. {singlePrice * quantity}";

            await _cartPage.VerifyProductInCart(0, firstProductName, firstProductPrice, quantity.ToString(), expectedTotalPrice);
        }

        [Test]
        public async Task RemovingProductFromCart_LeavesCartEmpty()
        {
            await _homePage.VerifyIsAtHomePage();
            await _homePage.NavBar.GoToProductsPage();
            await _productsPage.VerifyIsAtProductsPage();

            var firstProductName = await _productsPage.GetFirstProductName();
            var firstProductPrice = await _productsPage.GetFirstProductPrice();

            await _productsPage.HoverAndAddToCart(0);
            await _cartModal.VerifyIsVisible();
            await _cartModal.ViewCart();
            await _cartPage.VerifyIsAtCartPage();
            await _cartPage.VerifyProductsCount(1);
            await _cartPage.VerifyProductInCart(0, firstProductName, firstProductPrice, "1", firstProductPrice);

            await _cartPage.RemoveProduct(0);
            await _cartPage.VerifyCartIsEmpty();
        }

        [Test]
        public async Task RemovingOneProductFromCart_OtherProductsRemainInCart()
        {
            await _homePage.VerifyIsAtHomePage();
            await _homePage.NavBar.GoToProductsPage();
            await _productsPage.VerifyIsAtProductsPage();

            var firstProductName = await _productsPage.GetFirstProductName();
            var firstProductPrice = await _productsPage.GetFirstProductPrice();
            var secondProductName = await _productsPage.GetProductName(1);
            var secondProductPrice = await _productsPage.GetProductPrice(1);

            await _productsPage.HoverAndAddToCart(0);
            await _cartModal.VerifyIsVisible();
            await _cartModal.ContinueShopping();

            await _productsPage.HoverAndAddToCart(1);
            await _cartModal.VerifyIsVisible();
            await _cartModal.ViewCart();

            await _cartPage.VerifyIsAtCartPage();
            await _cartPage.VerifyProductsCount(2);
            await _cartPage.VerifyProductInCart(0, firstProductName, firstProductPrice, "1", firstProductPrice);
            await _cartPage.VerifyProductInCart(1, secondProductName, secondProductPrice, "1", secondProductPrice);

            await _cartPage.RemoveProduct(0);
            await _cartPage.VerifyProductsCount(1);
            await _cartPage.VerifyProductNotInCart(firstProductName);
            await _cartPage.VerifyProductInCart(0, secondProductName, secondProductPrice, "1", secondProductPrice);
        }
    }
}
