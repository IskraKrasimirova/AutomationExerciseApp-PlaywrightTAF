using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace AutomationApp.UiTests.Pages
{
    public class Sidebar
    {
        private readonly IPage _page;

        private ILocator CategorySidebar => _page.Locator("#accordian");
        private ILocator WomenCategoryLink => CategorySidebar.Locator("a[href='#Women']");
        private ILocator MenCategoryLink => CategorySidebar.Locator("a[href='#Men']");
        private ILocator KidsCategoryLink => CategorySidebar.Locator("a[href='#Kids']");
        private ILocator WomenSubCategoryPanel => _page.Locator("#Women");
        private ILocator MenSubCategoryPanel => _page.Locator("#Men");
        private ILocator KidsSubCategoryPanel => _page.Locator("#Kids");
        private ILocator WomenSubCategoryLink(string name) => WomenSubCategoryPanel.GetByRole(AriaRole.Link, new() { Name = name });
        private ILocator MenSubCategoryLink(string name) => MenSubCategoryPanel.GetByRole(AriaRole.Link, new() { Name = name });
        private ILocator KidsSubCategoryLink(string name) => KidsSubCategoryPanel.GetByRole(AriaRole.Link, new() { Name = name });

        public Sidebar(IPage page)
        {
            _page = page;
        }

        public async Task ExpandAndClickWomenSubCategory(string subCategory)
        {
            await WomenCategoryLink.ClickAsync();
            await WomenSubCategoryPanel.WaitForAsync(new() { State = WaitForSelectorState.Visible });
            await WomenSubCategoryLink(subCategory).ClickAsync();
        }

        public async Task ExpandAndClickMenSubCategory(string subCategory)
        {
            await MenCategoryLink.ClickAsync();
            await MenSubCategoryPanel.WaitForAsync(new() { State = WaitForSelectorState.Visible });
            await MenSubCategoryLink(subCategory).ClickAsync();
        }

        public async Task ExpandAndClickKidsSubCategory(string subCategory)
        {
            await KidsCategoryLink.ClickAsync();
            await KidsSubCategoryPanel.WaitForAsync(new() { State = WaitForSelectorState.Visible });
            await KidsSubCategoryLink(subCategory).ClickAsync();
        }
    }
}
