using Microsoft.Playwright;

namespace AutomationApp.UiTests.Pages
{
    public class BasePage
    {
        protected readonly IPage _page;

        public NavBar NavBar { get; }

        public BasePage(IPage page)
        {
            _page = page;
            NavBar = new NavBar(page);
        }
    }
}
