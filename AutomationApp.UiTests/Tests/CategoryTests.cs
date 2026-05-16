using Allure.NUnit;
using Allure.NUnit.Attributes;
using AutomationApp.UiTests.Pages;
using Microsoft.Extensions.DependencyInjection;

namespace AutomationApp.UiTests.Tests
{
    [AllureNUnit]
    [Category("CategoryProducts")]
    public class CategoryTests : BaseTest
    {
        private HomePage _homePage = null!;
        private ProductsPage _productsPage = null!;
        private CategoryProductsPage _categoryProductsPage = null!;

        [SetUp]
        public async Task TestSetUp()
        {
            _homePage = ServiceProvider.GetRequiredService<HomePage>();
            _productsPage = ServiceProvider.GetRequiredService<ProductsPage>();
            _categoryProductsPage = ServiceProvider.GetRequiredService<CategoryProductsPage>();

            await Page.GotoAsync("/");
            await _homePage.AcceptCookiesIfPresent();
        }

        [Test]
        [Category("Smoke")]
        [AllureFeature("Smoke")]
        public async Task NavigatingBetweenCategories_DisplaysCorrectCategoryPage()
        {
            await _homePage.VerifyIsAtHomePage();
            await _homePage.NavBar.GoToProductsPage();
            await _productsPage.VerifyIsAtProductsPage();

            await _productsPage.Sidebar.ExpandAndClickSubCategory("Women", "Tops");
            await _categoryProductsPage.VerifyIsAtCategoryPage("Women", "Tops");

            await _categoryProductsPage.Sidebar.ExpandAndClickSubCategory("Men", "Tshirts");
            await _categoryProductsPage.VerifyIsAtCategoryPage("Men", "Tshirts");
        }
    }
}
