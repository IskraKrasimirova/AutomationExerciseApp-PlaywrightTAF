using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using AutomationApp.Common.Utilities;
using AutomationApp.UiTests.Utilities;
using Microsoft.Playwright;

namespace AutomationApp.UiTests.Tests
{
    public class BaseTest
    {
        protected IPage Page = null!;
        private IPlaywright _playwright = null!;
        private IBrowser _browser = null!;
        private string _browserName = UiConstants.BrowserChromium;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            var isCi = Environment.GetEnvironmentVariable("CI") == "true";

            _playwright = await Playwright.CreateAsync();
            _browserName = Environment.GetEnvironmentVariable("BROWSER") ?? UiConstants.BrowserChromium;

            switch (_browserName)
            {
                case UiConstants.BrowserChromium:
                    _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                    {
                        Headless = isCi,
                        Args = isCi ? [] : ["--start-maximized"]
                    });
                    break;
                case UiConstants.BrowserFirefox:
                    _browser = await _playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions
                    {
                        Headless = isCi,
                        Args = isCi ? [] : ["--start-maximized"]
                    });
                    break;
                case UiConstants.BrowserWebkit:
                    _browser = await _playwright.Webkit.LaunchAsync(new BrowserTypeLaunchOptions
                    {
                        Headless = isCi,
                        Args = isCi ? [] : ["--start-maximized"]
                    });
                    break;
                default:
                    throw new ArgumentException($"Unsupported browser: '{_browserName}'. Valid options are: chromium, firefox, webkit.");
            }
        }

        [SetUp]
        public async Task SetUp()
        {
            var context = await _browser.NewContextAsync(new BrowserNewContextOptions
            {
                BaseURL = ConfigurationSettings.Instance.SettingsModel.BaseUrl,
                ViewportSize = ViewportSize.NoViewport
            });

            await context.RouteAsync("**/*", async route =>
            {
                var url = route.Request.Url;
                if (route.Request.ResourceType == "script" && (
                    url.Contains("googlesyndication") ||
                    url.Contains("doubleclick.net") ||
                    url.Contains("googleadservices") ||
                    url.Contains("adnxs.com") ||
                    url.Contains("amazon-adsystem")))
                {
                    await route.AbortAsync();
                }
                else
                {
                    await route.ContinueAsync();
                }
            });

            context.SetDefaultTimeout(10000);

            Page = await context.NewPageAsync();

            AllureLifecycle.Instance.UpdateTestCase(x =>
            {
                x.labels.RemoveAll(l => l.name == "suite" || l.name == "parentSuite");

                x.labels.Add(new Label { name = "parentSuite", value = "UI Tests" });
                x.labels.Add(new Label { name = "suite", value = _browserName });

                x.labels.RemoveAll(l => l.name == "layer");
                x.labels.Add(new Label { name = "layer", value = "ui" });
            });
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
