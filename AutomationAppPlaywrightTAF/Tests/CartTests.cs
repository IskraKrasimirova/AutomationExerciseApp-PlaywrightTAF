using AutomationApp.UiTests.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationApp.UiTests.Tests
{
    public class CartTests : BaseTest
    {
        private HomePage _homePage;
        private ProductsPage _productsPage;
        private CartModal _cartModal;
        private CartPage _cartPage;

        [SetUp]
        public async Task TestSetUp()
        {
            _homePage = new HomePage(Page);
            _productsPage = new ProductsPage(Page);
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
    }
}
