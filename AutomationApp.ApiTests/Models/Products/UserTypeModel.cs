using Newtonsoft.Json;

namespace AutomationApp.ApiTests.Models.Products
{
    public class UserTypeModel
    {
        [JsonProperty("usertype")]
        public string UserType { get; set; }
    }
}
