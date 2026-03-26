using Microsoft.Playwright;

namespace AutomationApp.UiTests.Pages
{
    public class Sidebar
    {
        private readonly IPage _page;

        private ILocator CategorySidebar => _page.Locator("#accordian");
        private ILocator CategoryLink(string category) => CategorySidebar.Locator($"a[href='#{category}']");
        private ILocator SubCategoryPanel(string category) => _page.Locator($"#{category}");
        private ILocator SubCategoryLink(string category, string name) => SubCategoryPanel(category).GetByRole(AriaRole.Link, new() { Name = name });
        private ILocator BrandsSidebar => _page.Locator(".brands_products");
        private ILocator BrandLinks => BrandsSidebar.Locator(".brands-name a");
        private ILocator BrandLinkByName(string brandName) => BrandsSidebar.Locator($"a[href='/brand_products/{brandName}' i]");
        private ILocator BrandLinkCount(int index) => BrandLinks.Nth(index).Locator("span.pull-right");

        public Sidebar(IPage page)
        {
            _page = page;
        }

        public async Task ExpandAndClickSubCategory(string category, string subCategory)
        {
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await CategoryLink(category).ClickAsync();
            await SubCategoryPanel(category).WaitForAsync(new() { State = WaitForSelectorState.Visible });
            await SubCategoryLink(category, subCategory).ClickAsync();
        }

        public async Task<(string Name, int Count)> GetBrandInfo(int index)
        {
            var link = BrandLinks.Nth(index);
            var fullText = await link.InnerTextAsync();
            var countText = await BrandLinkCount(index).InnerTextAsync();
            var name = fullText.Replace(countText, "").Trim();
            var count = int.Parse(countText.Trim('(', ')'));

            return (name, count);
        }

        public async Task<int> GetBrandsCount() => await BrandLinks.CountAsync();

        public async Task ClickBrand(string brandName)
        {
            await BrandsSidebar.ScrollIntoViewIfNeededAsync();
            await BrandLinkByName(brandName).ClickAsync();
        }
    }
}
