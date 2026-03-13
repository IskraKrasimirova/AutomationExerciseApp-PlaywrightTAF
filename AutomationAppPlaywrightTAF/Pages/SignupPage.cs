using AutomationApp.UiTests.Models;
using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace AutomationApp.UiTests.Pages
{
    public class SignupPage : BasePage
    {
        private ILocator AccountInfoForm => _page.Locator(".login-form");
        private ILocator AccountInfoHeader => AccountInfoForm.GetByRole(AriaRole.Heading, new() { Name = "Enter Account Information" });
        private ILocator MrRadioButton => AccountInfoForm.GetByLabel("Mr.");
        private ILocator MrsRadioButton => AccountInfoForm.GetByLabel("Mrs.");

        private ILocator NameInput => AccountInfoForm.Locator("[data-qa='name']");
        private ILocator EmailInput => AccountInfoForm.GetByLabel("Email");
        private ILocator PasswordInput => AccountInfoForm.GetByLabel("Password");

        private ILocator DaySelect => AccountInfoForm.Locator("#days");
        private ILocator MonthSelect => AccountInfoForm.Locator("#months");
        private ILocator YearSelect => AccountInfoForm.Locator("#years");

        private ILocator NewsletterCheckbox => AccountInfoForm.Locator("#newsletter");
        private ILocator OffersCheckbox => AccountInfoForm.Locator("#optin");

        private ILocator AddressInfoHeader => AccountInfoForm.GetByRole(AriaRole.Heading, new() { Name = "Address Information" });

        private ILocator FirstNameInput => AccountInfoForm.Locator("[data-qa='first_name']");
        private ILocator LastNameInput => AccountInfoForm.Locator("[data-qa='last_name']");
        private ILocator CompanyInput => AccountInfoForm.Locator("[data-qa='company']");
        private ILocator AddressInput => AccountInfoForm.Locator("[data-qa='address']");
        private ILocator Address2Input => AccountInfoForm.Locator("[data-qa='address2']");
        private ILocator CountrySelect => AccountInfoForm.Locator("[data-qa='country']");
        private ILocator StateInput => AccountInfoForm.Locator("[data-qa='state']");
        private ILocator CityInput => AccountInfoForm.Locator("[data-qa='city']");
        private ILocator ZipcodeInput => AccountInfoForm.Locator("[data-qa='zipcode']");
        private ILocator MobileNumberInput => AccountInfoForm.Locator("[data-qa='mobile_number']");
        private ILocator CreateAccountButton => AccountInfoForm.GetByRole(AriaRole.Button, new() { Name = "Create Account" });

        public SignupPage(IPage page) : base(page)
        {
        }

        public async Task CreateAccount(UserModel model)
        {
            await SelectTitle(model.Title);
            await PasswordInput.FillAsync(model.Password);
            await SelectDateOfBirth(model.DayOfBirth, model.MonthOfBirth, model.YearOfBirth);

            if (model.SubscribeToNewsletter)
            {
                await NewsletterCheckbox.CheckAsync();
            }

            if (model.ReceiveSpecialOffers)
            {
                await OffersCheckbox.CheckAsync();
            }

            await FirstNameInput.FillAsync(model.FirstName);
            await LastNameInput.FillAsync(model.LastName);
            await CompanyInput.FillAsync(model.Company);
            await AddressInput.FillAsync(model.Address);
            await Address2Input.FillAsync(model.Address2);
            await SelectCountry(model.Country);
            await StateInput.FillAsync(model.State);
            await CityInput.FillAsync(model.City);
            await ZipcodeInput.FillAsync(model.Zipcode);
            await MobileNumberInput.FillAsync(model.MobileNumber);

            await CreateAccountButton.ClickAsync();
        }

        public async Task VerifyIsAtSignupPage(string name, string email)
        {
            await Expect(_page).ToHaveURLAsync("/signup");
            //await Expect(AccountInfoForm).ToBeVisibleAsync();
            await Expect(AccountInfoHeader).ToBeVisibleAsync();
            await Expect(AddressInfoHeader).ToBeVisibleAsync();
            await Expect(NameInput).ToHaveValueAsync(name);
            await Expect(EmailInput).ToHaveValueAsync(email);
        }

        private async Task SelectTitle(string title)
        {
            if (title == "Mr.")
            {
                await MrRadioButton.CheckAsync();
            }
            else
            {
                await MrsRadioButton.CheckAsync();
            }
        }

        private async Task SelectDateOfBirth(string day, string month, string year)
        {
            await DaySelect.SelectOptionAsync(day);
            await MonthSelect.SelectOptionAsync(month);
            await YearSelect.SelectOptionAsync(year);
        }

        private async Task SelectCountry(string country)
        {
            await CountrySelect.SelectOptionAsync(country);
        }
    }
}
