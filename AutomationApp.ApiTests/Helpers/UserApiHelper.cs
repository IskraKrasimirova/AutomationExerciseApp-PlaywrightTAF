using AutomationApp.ApiTests.Models;
using AutomationApp.ApiTests.Models.Users;
using AutomationApp.ApiTests.Models.Users.Factories;
using AutomationApp.ApiTests.Utilities;
using RestSharp;

namespace AutomationApp.ApiTests.Helpers
{
    public class UserApiHelper
    {
        private readonly RestClient _client;

        public UserApiHelper(RestClient client)
        {
            _client = client;
        }

        public async Task<UserModel> CreateUserAsync()
        {
            var user = UserFactory.CreateDefault();

            var request = new RestRequest(ApiConstants.CreateAccountEndpoint);
            request.AddParameter("name", user.Name);
            request.AddParameter("email", user.Email);
            request.AddParameter("password", user.Password);
            request.AddParameter("title", user.Title);
            request.AddParameter("birth_date", user.DayOfBirth);
            request.AddParameter("birth_month", user.MonthOfBirth);
            request.AddParameter("birth_year", user.YearOfBirth);
            request.AddParameter("firstname", user.FirstName);
            request.AddParameter("lastname", user.LastName);
            request.AddParameter("company", user.Company);
            request.AddParameter("address1", user.Address);
            request.AddParameter("address2", user.Address2);
            request.AddParameter("country", user.Country);
            request.AddParameter("state", user.State);
            request.AddParameter("city", user.City);
            request.AddParameter("zipcode", user.Zipcode);
            request.AddParameter("mobile_number", user.MobileNumber);

            var response = await _client.ExecutePostAsync<ApiResponse>(request);

            if (response.Data?.ResponseCode != 201)
            {
                throw new Exception($"Failed to create user. ResponseCode: {response.Data?.ResponseCode}, Message: {response.Data?.Message}");
            }

            return user;
        }

        public async Task DeleteUserAsync(string email, string password)
        {
            var request = new RestRequest(ApiConstants.DeleteAccountEndpoint);
            request.AddParameter("email", email);
            request.AddParameter("password", password);
            var response = await _client.ExecuteDeleteAsync<ApiResponse>(request);

            if (response.Data?.ResponseCode != 200)
            {
                throw new Exception($"Failed to delete user. ResponseCode: {response.Data?.ResponseCode}, Message: {response.Data?.Message}");
            }
        }
    }
}
