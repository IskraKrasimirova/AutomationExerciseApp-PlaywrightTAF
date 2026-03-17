using Newtonsoft.Json;

namespace AutomationApp.ApiTests.Models
{
    public class ApiResponse
    {
        [JsonProperty("responseCode")]
        public int ResponseCode { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
