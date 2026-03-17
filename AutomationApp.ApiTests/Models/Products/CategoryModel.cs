using Newtonsoft.Json;

namespace AutomationApp.ApiTests.Models.Products
{
    public class CategoryModel
    {
        [JsonProperty("usertype")]
        public UserTypeModel UserType { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }
    }
}
