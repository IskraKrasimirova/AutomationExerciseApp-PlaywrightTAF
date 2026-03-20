namespace AutomationApp.UiTests.Models
{
    public class PaymentModel
    {
        public string NameOnCard { get; set; }
        public string CardNumber { get; set; }
        public string Cvc { get; set; }
        public string ExpirationMonth { get; set; }
        public string ExpirationYear { get; set; }
    }
}
