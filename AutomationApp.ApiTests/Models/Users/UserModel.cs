using Newtonsoft.Json;

namespace AutomationApp.ApiTests.Models.Users
{
    public class UserModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Title { get; set; }

        [JsonProperty("birth_date")]
        public string DayOfBirth { get; set; }

        [JsonProperty("birth_month")]
        public string MonthOfBirth { get; set; }

        [JsonProperty("birth_year")]
        public string YearOfBirth { get; set; }

        [JsonProperty("firstname")]
        public string FirstName { get; set; }

        [JsonProperty("lastname")]
        public string LastName { get; set; }

        public string Company { get; set; }

        [JsonProperty("address1")]
        public string Address { get; set; }

        [JsonProperty("address2")]
        public string Address2 { get; set; }

        public string Country { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Zipcode { get; set; }

        [JsonProperty("mobile_number")]
        public string MobileNumber { get; set; }
    }
}
