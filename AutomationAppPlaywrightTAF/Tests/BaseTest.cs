using Microsoft.Playwright;

namespace AutomationApp.UiTests.Tests
{
    public class BaseTest
    {
        protected IPage Page = null!;
        private IPlaywright _playwright = null!;
        private IBrowser _browser = null!;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });
        }

        [SetUp]
        public async Task SetUp()
        {
            Page = await _browser.NewPageAsync();
        }

        [TearDown]
        public async Task TearDown()
        {
            await Page.CloseAsync();
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
            await _browser.DisposeAsync();
            _playwright.Dispose();
        }
    }
}
