using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace AutomationApp.UiTests.Pages
{
    public class NavBar
    {
        private readonly IPage _page;

        public NavBar(IPage page)
        {
            _page = page;
        }

        private ILocator HomeLink => _page.GetByRole(AriaRole.Link, new() { Name = "Home" });
        private ILocator ProductsLink => _page.GetByRole(AriaRole.Link, new() { Name = "Products" });
        private ILocator CartLink => _page.GetByRole(AriaRole.Link, new() { Name = "Cart" });
        private ILocator LoginLink => _page.GetByRole(AriaRole.Link, new() { Name = "Signup / Login" });
        private ILocator TestCasesLink => _page.GetByRole(AriaRole.Link, new() { Name = "Test Cases" });
        private ILocator ApiTestingLink => _page.GetByRole(AriaRole.Link, new() { Name = "API Testing" });
        private ILocator ContactUsLink => _page.GetByRole(AriaRole.Link, new() { Name = "Contact us" });

        // The "Video Tutorials" link opens a youtube page in a new tab, which is not handled in the current test suite, so it's commented out for now. It can be implemented in the future if needed.
        // private ILocator VideoTutorialsLink => _page.GetByRole(AriaRole.Link, new() { Name = "Video Tutorials" });

        private ILocator LogoutLink => _page.GetByRole(AriaRole.Link, new() { Name = "Logout" });
        private ILocator DeleteAccountLink => _page.GetByRole(AriaRole.Link, new() { Name = "Delete Account" });

        private ILocator LoggedInAs => _page.GetByText("Logged in as");

        public async Task GoToProductsPage() => await ProductsLink.ClickAsync();
        public async Task GoToCartPage() => await CartLink.ClickAsync();
        public async Task GoToLoginPage() => await LoginLink.ClickAsync();
        public async Task GoToTestCasesPage() => await TestCasesLink.ClickAsync();
        public async Task GoToApiTestingPage() => await ApiTestingLink.ClickAsync();
        public async Task GoToContactUsPage() => await ContactUsLink.ClickAsync();
        public async Task GoToHomePage() => await HomeLink.ClickAsync();

        public async Task Logout() => await LogoutLink.ClickAsync();
        public async Task DeleteAccount() => await DeleteAccountLink.ClickAsync();

        public async Task<bool> IsUserLoggedIn() => await LogoutLink.IsVisibleAsync();

        public async Task VerifyUserIsLoggedIn(string name)
        {
            await Expect(LogoutLink).ToBeVisibleAsync();
            await Expect(DeleteAccountLink).ToBeVisibleAsync();
            await Expect(LoggedInAs).ToContainTextAsync(name);
        }
    }
}
