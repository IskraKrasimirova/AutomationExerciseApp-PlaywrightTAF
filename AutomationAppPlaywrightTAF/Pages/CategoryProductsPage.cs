using Microsoft.Playwright;
using System.Text.RegularExpressions;
using static Microsoft.Playwright.Assertions;

namespace AutomationApp.UiTests.Pages
{
    public class CategoryProductsPage : BasePage
    {
        private ILocator CategoryHeader => _page.Locator(".features_items h2.title");
        private ILocator ProductsList => _page.Locator(".features_items");
        private ILocator ProductItems => ProductsList.Locator(".product-image-wrapper");
        private ILocator ProductsLink => _page.Locator(".breadcrumbs").GetByRole(AriaRole.Link);
        private ILocator CategoryBreadcrumbText => _page.Locator(".breadcrumbs li.active");
        public Sidebar Sidebar { get; }

        public CategoryProductsPage(IPage page) : base(page)
        {
            Sidebar = new Sidebar(page);
        }

        public async Task VerifyIsAtCategoryPage(string expectedCategory, string expectedSubCategory)
        {
            var expectedHeader = $"{expectedCategory} - {expectedSubCategory} Products";
            var expectedBreadcrumbText = $"{expectedCategory} > {expectedSubCategory}";
            await Expect(_page).ToHaveURLAsync(new Regex("/category_products/\\d+"));
            await Expect(CategoryHeader).ToContainTextAsync(expectedHeader, new() { IgnoreCase = true });
            await Expect(ProductsList).ToBeVisibleAsync();
            await Expect(ProductsLink).ToBeVisibleAsync();
            await Expect(CategoryBreadcrumbText).ToContainTextAsync(expectedBreadcrumbText, new() { IgnoreCase = true });
            var productsCount = await ProductItems.CountAsync();
            Assert.That(productsCount, Is.GreaterThan(0), $"Expected at least 1 product in {expectedCategory} - {expectedSubCategory}");
        }
    }
}
