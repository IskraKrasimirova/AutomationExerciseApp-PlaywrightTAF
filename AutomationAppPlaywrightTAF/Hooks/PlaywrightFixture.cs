using Microsoft.Playwright;

namespace AutomationApp.UiTests.Hooks
{
    public class PlaywrightFixture : IAsyncDisposable
    {
        private IPlaywright? _playwright;
        private IBrowser? _browser;

        public IBrowser Browser => _browser!;

        public async Task InitializeAsync()
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });
        }

        public async ValueTask DisposeAsync()
        {
            if (_browser != null) await _browser.DisposeAsync();
            _playwright?.Dispose();
        }
    }
}
