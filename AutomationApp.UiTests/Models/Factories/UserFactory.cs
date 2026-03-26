using Bogus;

namespace AutomationApp.UiTests.Models.Factories
{
    public class UserFactory
    {
        private static readonly Faker Faker = new();
        private static readonly string[] Titles = ["Mr.", "Mrs."];
        private static readonly List<string> ValidCountries = ["India", "United States", "Canada", "Australia", "Israel", "New Zealand", "Singapore"];

        public static UserModel CreateDefault()
        {
            var user = new UserModel
            {
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                Password = Faker.Internet.Password(),
                Title = Faker.PickRandom(Titles),
                DayOfBirth = Faker.Random.Int(1, 28).ToString(),
                MonthOfBirth = Faker.Date.Between(DateTime.Now.AddYears(-60), DateTime.Now.AddYears(-18)).ToString("MMMM"),
                YearOfBirth = Faker.Date.Between(DateTime.Now.AddYears(-60), DateTime.Now.AddYears(-18)).Year.ToString(),
                SubscribeToNewsletter = true,
                ReceiveSpecialOffers = true,
                FirstName = Faker.Name.FirstName(),
                LastName = Faker.Name.LastName(),
                Company = Faker.Company.CompanyName(),
                Address = Faker.Address.StreetAddress(),
                Address2 = Faker.Address.SecondaryAddress(),
                Country = Faker.PickRandom(ValidCountries),
                State = Faker.Address.State(),
                City = Faker.Address.City(),
                Zipcode = Faker.Address.ZipCode(),
                MobileNumber = Faker.Phone.PhoneNumber()
            };

            return user;
        }
    }
}
