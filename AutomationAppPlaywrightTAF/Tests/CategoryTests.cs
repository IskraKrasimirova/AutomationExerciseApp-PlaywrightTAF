using AutomationApp.UiTests.Pages;

namespace AutomationApp.UiTests.Tests
{
    [Category("CategoryProducts")]
    public class CategoryTests : BaseTest
    {
        private HomePage _homePage;
        private ProductsPage _productsPage;
        private CategoryProductsPage _categoryProductsPage;

        [SetUp]
        public async Task TestSetUp()
        {
            _homePage = new HomePage(Page);
            _productsPage = new ProductsPage(Page);
            _categoryProductsPage = new CategoryProductsPage(Page);

            await Page.GotoAsync("/");
            await _homePage.AcceptCookiesIfPresent();
        }

        [Test]
        [Category("Smoke")]
        public async Task BrowsingCategoryProducts_ShowsCorrectProductsForEachCategory()
        {
            await _homePage.VerifyIsAtHomePage();
            await _homePage.NavBar.GoToProductsPage();
            await _productsPage.VerifyIsAtProductsPage();

            await _productsPage.Sidebar.ExpandAndClickWomenSubCategory("Tops");
            await _categoryProductsPage.VerifyIsAtCategoryPage("Women", "Tops");

            await _categoryProductsPage.Sidebar.ExpandAndClickMenSubCategory("Tshirts");
            await _categoryProductsPage.VerifyIsAtCategoryPage("Men", "Tshirts");
        }
    }
}
