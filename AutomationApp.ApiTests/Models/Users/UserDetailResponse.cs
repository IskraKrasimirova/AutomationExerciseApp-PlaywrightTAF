using Newtonsoft.Json;

namespace AutomationApp.ApiTests.Models.Users
{
    public class UserDetailResponse
    {
        [JsonProperty("responseCode")]
        public int ResponseCode { get; set; }

        [JsonProperty("user")]
        public UserDetailModel User { get; set; }
    }
}
