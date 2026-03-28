using Allure.NUnit;
using AutomationApp.UiTests.Models.Factories;
using AutomationApp.UiTests.Pages;

namespace AutomationApp.UiTests.Tests
{
    [AllureNUnit]
    [Category("ContactUs")]
    public class ContactUsTests : BaseTest
    {
        private HomePage _homePage;
        private ContactUsPage _contactUsPage;

        [SetUp]
        public async Task TestSetUp()
        {
            _homePage = new HomePage(Page);
            _contactUsPage = new ContactUsPage(Page);
            await Page.GotoAsync("/");
            await _homePage.AcceptCookiesIfPresent();
            await _homePage.NavBar.GoToContactUsPage();
            await _contactUsPage.VerifyIsAtContactUsPage();
        }

        [Test]
        [Category("Smoke")]
        public async Task SubmitContactUsForm_WithValidData_RedirectsToHomePage()
        {
            var contactData = ContactFormFactory.CreateDefault();
            contactData.FilePath = null;

            await _contactUsPage.SubmitContactForm(contactData);
            await _contactUsPage.VerifySuccessMessage();
            await _contactUsPage.GoToHomePge();
            await _homePage.VerifyIsAtHomePage();
        }

        [Test]
        [Category("Smoke")]
        public async Task SubmitContactUsForm_WithValidDataAndUploadFile_RedirectsToHomePage()
        {
            var contactData = ContactFormFactory.CreateDefault();

            await _contactUsPage.SubmitContactForm(contactData);
            await _contactUsPage.VerifySuccessMessage();
            await _contactUsPage.GoToHomePge();
            await _homePage.VerifyIsAtHomePage();
        }
    }
}
