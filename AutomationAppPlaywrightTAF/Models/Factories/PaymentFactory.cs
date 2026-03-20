using Bogus;

namespace AutomationApp.UiTests.Models.Factories
{
    public class PaymentFactory
    {
        private static readonly Faker Faker = new();

        public static PaymentModel CreateDefault()
        {
            var paymentModel = new PaymentModel
            {
                NameOnCard = Faker.Name.FullName(),
                CardNumber = Faker.Finance.CreditCardNumber(), //"4242424242424242"
                Cvc = Faker.Finance.CreditCardCvv(), //"123"
                ExpirationMonth = DateTime.Now.AddMonths(1).ToString("MM"),
                ExpirationYear = DateTime.Now.AddYears(1).ToString("yyyy")
            };

            return paymentModel;
        }
    }
}
