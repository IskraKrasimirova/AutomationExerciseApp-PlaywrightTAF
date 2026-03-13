using AutomationApp.UiTests.Utilities;
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
                Headless = false,
                Args = ["--start-maximized"]
            });
        }

        [SetUp]
        public async Task SetUp()
        {
            var context = await _browser.NewContextAsync(new BrowserNewContextOptions
            {
                BaseURL = ConfigurationSettings.Instance.SettingsModel.BaseUrl,
                ViewportSize = ViewportSize.NoViewport
            });

            context.SetDefaultTimeout(10000);

            Page = await context.NewPageAsync();
        }

        [TearDown]
        public async Task TearDown()
        {
            await Page.Context.CloseAsync();
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
            await _browser.DisposeAsync();
            _playwright.Dispose();
        }
    }
}
