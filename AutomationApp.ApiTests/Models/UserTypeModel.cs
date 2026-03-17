using Newtonsoft.Json;

namespace AutomationApp.ApiTests.Models
{
    public class UserTypeModel
    {
        [JsonProperty("usertype")]
        public string UserType { get; set; }
    }
}
