using Newtonsoft.Json;

namespace AutomationApp.ApiTests.Models.Brands
{
    public class BrandModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("brand")]
        public string Brand { get; set; }
    }
}
