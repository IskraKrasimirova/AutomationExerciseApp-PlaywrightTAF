using AutomationApp.Common.Utilities;
using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace AutomationApp.UiTests.Pages
{
    public class HomePage: BasePage
    {
        private ILocator LogoImage => _page.GetByRole(AriaRole.Link, new() { Name = "Home" });
        private ILocator Header => _page.Locator(".item.active h1");
        private ILocator ConsentButton => _page.GetByRole(AriaRole.Button, new() { Name = "Consent" });

        public HomePage(IPage page) : base(page) 
        { 
        }

        public async Task AcceptCookiesIfPresent()
        {
            if (await ConsentButton.IsVisibleAsync())
            {
                await ConsentButton.ClickAsync();
            }     
        }

        public async Task VerifyIsAtHomePage()
        {
            await Expect(_page).ToHaveURLAsync(ConfigurationSettings.Instance.SettingsModel.BaseUrl);
            await Expect(LogoImage).ToBeVisibleAsync();
            await Expect(Header).ToContainTextAsync("AutomationExercise");
        }
    }
}
