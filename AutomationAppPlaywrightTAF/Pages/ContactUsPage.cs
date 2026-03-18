using AutomationApp.UiTests.Models;
using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace AutomationApp.UiTests.Pages
{
    public class ContactUsPage : BasePage
    {
        private ILocator ContactUsForm => _page.Locator("#contact-us-form");
        private ILocator ContactUsHeader => _page.GetByRole(AriaRole.Heading, new() { Name = "CONTACT US" });
        private ILocator GetInTouchHeader => _page.GetByRole(AriaRole.Heading, new() { Name = "GET IN TOUCH" });
        private ILocator FeedbackHeader => _page.GetByRole(AriaRole.Heading, new() { Name = "Feedback For Us" });

        private ILocator NameInput => ContactUsForm.GetByPlaceholder("Name");
        private ILocator EmailInput => ContactUsForm.GetByPlaceholder("Email");
        private ILocator SubjectInput => ContactUsForm.GetByPlaceholder("Subject");
        private ILocator MessageInput => ContactUsForm.GetByPlaceholder("Your Message Here");
        private ILocator FileUploadInput => ContactUsForm.Locator("input[type='file']");
        private ILocator SubmitButton => ContactUsForm.GetByText("Submit");
        private ILocator SuccessMessage => _page.Locator(".contact-form").GetByText("Success! Your details have been submitted successfully.");
        private ILocator HomeButton => _page.Locator("#form-section").GetByRole(AriaRole.Link, new() { Name = "Home" });

        public ContactUsPage(IPage page) : base(page)
        {
        }

        public async Task SubmitContactForm(ContactFormModel form)
        {
            _page.Dialog += async (_, dialog) => await dialog.AcceptAsync();

            await NameInput.WaitForAsync();
            await NameInput.FillAsync(form.Name);
            await EmailInput.FillAsync(form.Email);
            await SubjectInput.FillAsync(form.Subject);
            await MessageInput.FillAsync(form.Message);

            if (!string.IsNullOrEmpty(form.FilePath))
            {
                await FileUploadInput.SetInputFilesAsync(form.FilePath);
            }

            await SubmitButton.ClickAsync();
        }

        public async Task GoToHomePge()
        {
            await HomeButton.ClickAsync();
        }

        public async Task VerifyIsAtContactUsPage()
        {
            await Expect(_page).ToHaveURLAsync("/contact_us");
            await Expect(ContactUsForm).ToBeVisibleAsync();
            await Expect(ContactUsHeader).ToBeVisibleAsync();
            await Expect(GetInTouchHeader).ToBeVisibleAsync();
            await Expect(FeedbackHeader).ToBeVisibleAsync();
        }

        public async Task VerifySuccessMessage()
        {
            await Expect(SuccessMessage).ToBeVisibleAsync();
        }
    }
}
