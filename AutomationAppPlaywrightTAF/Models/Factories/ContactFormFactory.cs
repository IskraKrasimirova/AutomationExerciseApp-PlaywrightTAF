using Bogus;

namespace AutomationApp.UiTests.Models.Factories
{
    public class ContactFormFactory
    {
        private static readonly Faker Faker = new();

        public static ContactFormModel CreateDefault()
        {
            var contactFormModel = new ContactFormModel
            {
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                Subject = Faker.Lorem.Sentence(),
                Message = Faker.Lorem.Paragraph(),
                FilePath = "TestData/test-file.txt"
            };

            return contactFormModel;
        }  
    }
}
