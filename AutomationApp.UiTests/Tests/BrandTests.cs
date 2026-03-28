using Allure.NUnit;
using AutomationApp.UiTests.Pages;

namespace AutomationApp.UiTests.Tests
{
    [AllureNUnit]
    [Category("BrandProducts")]
    public class BrandTests : BaseTest
    {
        private HomePage _homePage;
        private ProductsPage _productsPage;
        private BrandProductsPage _brandProductsPage;

        [SetUp]
        public async Task TestSetUp()
        {
            _homePage = new HomePage(Page);
            _productsPage = new ProductsPage(Page);
            _brandProductsPage = new BrandProductsPage(Page);

            await Page.GotoAsync("/");
            await _homePage.AcceptCookiesIfPresent();
        }

        [Test]
        [Category("Smoke")]
        public async Task NavigatingBetweenBrands_DisplaysCorrectBrandPageWithExpectedProductCount()
        {
            await _homePage.VerifyIsAtHomePage();
            await _homePage.NavBar.GoToProductsPage();
            await _productsPage.VerifyIsAtProductsPage();

            var brandsCount = await _productsPage.Sidebar.GetBrandsCount();
            var randomIndex = new Random().Next(brandsCount);
            var (firstBrand, firstBrandCount) = await _productsPage.Sidebar.GetBrandInfo(randomIndex);
            await _productsPage.Sidebar.ClickBrand(firstBrand);
            await _brandProductsPage.VerifyIsAtBrandPage(firstBrand, firstBrandCount);

            var secondIndex = (randomIndex + 1) % brandsCount;
            var (secondBrand, secondBrandCount) = await _brandProductsPage.Sidebar.GetBrandInfo(secondIndex);
            await _brandProductsPage.Sidebar.ClickBrand(secondBrand);
            await _brandProductsPage.VerifyIsAtBrandPage(secondBrand, secondBrandCount);
        }
    }
}
