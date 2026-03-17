using Newtonsoft.Json;

namespace AutomationApp.ApiTests.Models.Brands
{
    public class GetBrandsResponse
    {
        [JsonProperty("responseCode")]
        public int ResponseCode { get; set; }

        [JsonProperty("brands")]
        public List<BrandModel> Brands { get; set; }
    }
}
