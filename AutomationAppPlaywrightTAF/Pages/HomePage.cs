using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace AutomationApp.UiTests.Pages
{
    public class HomePage: BasePage
    {
        private ILocator LogoImage => _page.GetByAltText("Website for automation practice");
        private ILocator Header => _page.Locator("#slider-carousel .item.active h2");
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
            await Expect(_page).ToHaveURLAsync("/");
            await Expect(LogoImage).ToBeVisibleAsync();
            await Expect(Header).ToContainTextAsync("Full-Fledged practice website for Automation Engineers");
        }
    }
}
