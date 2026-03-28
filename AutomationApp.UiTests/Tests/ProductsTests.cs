using Allure.NUnit;
using Allure.NUnit.Attributes;
using AutomationApp.UiTests.Models;
using AutomationApp.UiTests.Pages;

namespace AutomationApp.UiTests.Tests
{
    [AllureNUnit]
    [Category("Products")]
    public class ProductsTests: BaseTest
    {
        private HomePage _homePage = null!;
        private ProductsPage _productsPage = null!;
        private ProductDetailsPage _productDetailsPage = null!;

        [SetUp]
        public async Task TestSetUp()
        {
            _homePage = new HomePage(Page);
            _productsPage = new ProductsPage(Page);
            _productDetailsPage = new ProductDetailsPage(Page);

            await Page.GotoAsync("/");
            await _homePage.AcceptCookiesIfPresent();
        }

        [Test]
        [Category("Smoke")]
        [AllureTag("Smoke")]
        public async Task ViewProductDetails_ForTheFirstProduct_DisplaysCorrectDetails()
        {
            await _homePage.VerifyIsAtHomePage();
            await _homePage.NavBar.GoToProductsPage();
            await _productsPage.VerifyIsAtProductsPage();

            var productName = await _productsPage.GetFirstProductName();
            var productPrice = await _productsPage.GetFirstProductPrice();
            var productImageSrc = await _productsPage.GetFirstProductImageSrc();

            await _productsPage.ClickViewFirstProduct();
            await _productDetailsPage.VerifyIsAtProductDetailsPage();
            await _productDetailsPage.VerifyProductNameIs(productName);
            await _productDetailsPage.VerifyProductPriceIs(productPrice);
            await _productDetailsPage.VerifyProductImageIs(productImageSrc);
        }

        [Test]
        [Category("Smoke")]
        [AllureTag("Smoke")]
        public async Task SearchProduct_WithValidSearchTerm_DisplaysSearchResults()
        {
            await _homePage.VerifyIsAtHomePage();
            await _homePage.NavBar.GoToProductsPage();
            await _productsPage.VerifyIsAtProductsPage();

            var productToSearch = ProductData.GetRandomProduct();
            await _productsPage.SearchProduct(productToSearch);
            await _productsPage.VerifySearchResultsAreDisplayed(productToSearch);
        }
    }
}
