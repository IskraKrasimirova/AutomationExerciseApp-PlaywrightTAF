using Microsoft.Playwright;

namespace AutomationApp.UiTests.Hooks
{
    public class BaseTest
    {
        protected IPage Page = null!;
        private PlaywrightFixture _fixture = null!;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            _fixture = new PlaywrightFixture();
            await _fixture.InitializeAsync();
        }

        [SetUp]
        public async Task SetUp()
        {
            Page = await _fixture.Browser.NewPageAsync();
        }

        [TearDown]
        public async Task TearDown()
        {
            await Page.CloseAsync();
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
            await _fixture.DisposeAsync();
        }
    }
}
