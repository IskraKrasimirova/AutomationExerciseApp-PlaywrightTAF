using Newtonsoft.Json;

namespace AutomationApp.ApiTests.Models
{
    public class ErrorResponse
    {
        [JsonProperty("responseCode")]
        public int ResponseCode { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
