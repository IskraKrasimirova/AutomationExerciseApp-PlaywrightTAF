namespace AutomationApp.UiTests.Models
{
    public class ProductData
    {
        public static readonly string[] ProductNames =
        [
            "Blue Top",
            "Men Tshirt",
            "Sleeveless Dress",
            "Dress",
            "Top",
            "Polo",
            "Tshirt",
            "Jeans",
            "Saree"
        ];

        public static string GetRandomProduct()
        {
            var random = new Random();
            return ProductNames[random.Next(ProductNames.Length)];
        }
    }
}
