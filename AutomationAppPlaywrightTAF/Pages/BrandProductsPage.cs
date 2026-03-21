using Microsoft.Playwright;
using System.Text.RegularExpressions;
using static Microsoft.Playwright.Assertions;

namespace AutomationApp.UiTests.Pages
{
    public class BrandProductsPage : BasePage
    {
        private ILocator BrandHeader => _page.Locator(".features_items h2.title");
        private ILocator ProductsList => _page.Locator(".features_items");
        private ILocator ProductItems => ProductsList.Locator(".product-image-wrapper");
        private ILocator ProductsLink => _page.Locator(".breadcrumbs").GetByRole(AriaRole.Link);
        private ILocator BrandBreadcrumbText => _page.Locator(".breadcrumbs li.active");
        public Sidebar Sidebar { get; }

        public BrandProductsPage(IPage page) : base(page)
        {
            Sidebar = new Sidebar(page);
        }

        public async Task VerifyIsAtBrandPage(string brandName, int expectedCount)
        {
            await Expect(_page).ToHaveURLAsync(new Regex($"/brand_products/", RegexOptions.IgnoreCase));
            await Expect(BrandHeader).ToContainTextAsync($"Brand - {brandName} Products", new() { IgnoreCase = true });
            await Expect(ProductsList).ToBeVisibleAsync();
            await Expect(ProductsLink).ToBeVisibleAsync();
            await Expect(BrandBreadcrumbText).ToContainTextAsync(brandName, new() { IgnoreCase = true });
            var productsCount = await ProductItems.CountAsync();
            await Expect(ProductItems).ToHaveCountAsync(expectedCount);
        }
    }
}
