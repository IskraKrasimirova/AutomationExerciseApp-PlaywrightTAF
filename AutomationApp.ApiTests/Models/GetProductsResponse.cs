using Newtonsoft.Json;

namespace AutomationApp.ApiTests.Models
{
    public class GetProductsResponse
    {
        [JsonProperty("responseCode")]
        public int ResponseCode { get; set; }

        [JsonProperty("products")]
        public List<ProductModel> Products { get; set; }
    }
}
