using Newtonsoft.Json;

namespace AutomationApp.ApiTests.Models.Products
{
    public class ProductModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("category")]
        public CategoryModel Category { get; set; }
    }
}
