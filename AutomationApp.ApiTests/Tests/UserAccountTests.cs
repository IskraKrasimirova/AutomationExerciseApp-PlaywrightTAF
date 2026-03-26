using Allure.NUnit;
using Allure.NUnit.Attributes;
using AutomationApp.ApiTests.Helpers;
using AutomationApp.ApiTests.Models;
using AutomationApp.ApiTests.Models.Users;
using AutomationApp.ApiTests.Models.Users.Factories;
using AutomationApp.ApiTests.Utilities;
using FluentAssertions;
using FluentAssertions.Execution;
using RestSharp;
using System.Net;

namespace AutomationApp.ApiTests.Tests
{
    [AllureNUnit]
    [AllureSuite("API Tests")]
    [AllureSubSuite("UserAccount")]
    [Category("UserAccountApi")]
    public class UserAccountTests: BaseTest
    {
        private UserModel _testUser;
        private UserApiHelper _userApiHelper;

        [OneTimeSetUp]
        public async Task CreateUser()
        {
            _userApiHelper = new UserApiHelper(Client);
            _testUser = await _userApiHelper.CreateUserAsync();
        }

        [OneTimeTearDown]
        public async Task DeleteUser()
        {
            await _userApiHelper.DeleteUserAsync(_testUser.Email, _testUser.Password);
        }

        [Test]
        public async Task GetUserDetailsByExistingEmail_ReturnsCorrectUserDetails()
        {
            var request = new RestRequest(ApiConstants.GetUserDetailByEmailEndpoint);
            request.AddParameter("email", _testUser.Email);

            var response = await Client.ExecuteGetAsync<UserDetailResponse>(request);

            AssertStatusCode(response, HttpStatusCode.OK);
            response.Data.Should().NotBeNull();
            response.Data!.ResponseCode.Should().Be(200);

            var user = response.Data.User;
            user.Should().NotBeNull();

            using (new AssertionScope())
            {
                user.Email.Should().Be(_testUser.Email);
                user.Name.Should().Be(_testUser.Name);
                user.Title.Should().Be(_testUser.Title);
                user.FirstName.Should().Be(_testUser.FirstName);
                user.LastName.Should().Be(_testUser.LastName);
                user.Company.Should().Be(_testUser.Company);
                user.Address1.Should().Be(_testUser.Address);
                user.Country.Should().Be(_testUser.Country);
                user.State.Should().Be(_testUser.State);
                user.City.Should().Be(_testUser.City);
                user.Zipcode.Should().Be(_testUser.Zipcode);
                user.BirthDay.Should().Be(_testUser.DayOfBirth);
                user.BirthMonth.Should().Be(_testUser.MonthOfBirth);
                user.BirthYear.Should().Be(_testUser.YearOfBirth);
            }       
        }

        [Test]
        public async Task GetUserDetailsByNotExistingEmail_ReturnsUserNotFound()
        {
            var request = new RestRequest(ApiConstants.GetUserDetailByEmailEndpoint);
            request.AddParameter("email", "notexisting@test.com");

            var response = await Client.ExecuteGetAsync<ApiResponse>(request);

            AssertStatusCode(response, HttpStatusCode.OK);
            response.Data.Should().NotBeNull();
            response.Data!.ResponseCode.Should().Be(404);
            response.Data!.Message.Should().Be("Account not found with this email, try another email!");
        }

        [Test]
        public async Task UpdateUserAccount_WithValidData_ReturnsUserUpdated()
        {
            var updatedUser = UserFactory.CreateDefault();
            updatedUser.Email = _testUser.Email;

            var request = new RestRequest(ApiConstants.UpdateAccountEndpoint);
            request.AddParameter("name", updatedUser.Name);
            request.AddParameter("email", updatedUser.Email);
            request.AddParameter("password", _testUser.Password);
            request.AddParameter("title", updatedUser.Title);
            request.AddParameter("birth_date", updatedUser.DayOfBirth);
            request.AddParameter("birth_month", updatedUser.MonthOfBirth);
            request.AddParameter("birth_year", updatedUser.YearOfBirth);
            request.AddParameter("firstname", updatedUser.FirstName);
            request.AddParameter("lastname", updatedUser.LastName);
            request.AddParameter("company", updatedUser.Company);
            request.AddParameter("address1", updatedUser.Address);
            request.AddParameter("address2", updatedUser.Address2);
            request.AddParameter("country", updatedUser.Country);
            request.AddParameter("state", updatedUser.State);
            request.AddParameter("city", updatedUser.City);
            request.AddParameter("zipcode", updatedUser.Zipcode);
            request.AddParameter("mobile_number", updatedUser.MobileNumber);

            var response = await Client.ExecutePutAsync<ApiResponse>(request);

            AssertStatusCode(response, HttpStatusCode.OK);
            response.Data.Should().NotBeNull();
            response.Data!.ResponseCode.Should().Be(200);
            response.Data.Message.Should().Be("User updated!");
        }

        [Test]
        public async Task CreateAccount_WithValidData_ReturnsUserCreated()
        {
            var newUser = UserFactory.CreateDefault();
            var request = new RestRequest(ApiConstants.CreateAccountEndpoint);
            request.AddParameter("name", newUser.Name);
            request.AddParameter("email", newUser.Email);
            request.AddParameter("password", newUser.Password);
            request.AddParameter("title", newUser.Title);
            request.AddParameter("birth_date", newUser.DayOfBirth);
            request.AddParameter("birth_month", newUser.MonthOfBirth);
            request.AddParameter("birth_year", newUser.YearOfBirth);
            request.AddParameter("firstname", newUser.FirstName);
            request.AddParameter("lastname", newUser.LastName);
            request.AddParameter("company", newUser.Company);
            request.AddParameter("address1", newUser.Address);
            request.AddParameter("address2", newUser.Address2);
            request.AddParameter("country", newUser.Country);
            request.AddParameter("state", newUser.State);
            request.AddParameter("city", newUser.City);
            request.AddParameter("zipcode", newUser.Zipcode);
            request.AddParameter("mobile_number", newUser.MobileNumber);

            var response = await Client.ExecutePostAsync<ApiResponse>(request);

            AssertStatusCode(response, HttpStatusCode.OK);
            response.Data!.ResponseCode.Should().Be(201);
            response.Data.Message.Should().Be("User created!");

            // Clean up - delete the created user
            await _userApiHelper.DeleteUserAsync(newUser.Email, newUser.Password);
        }

        [Test, Ignore("deleteAccount endpoint does not work outside Postman.")]
        public async Task DeleteAccount_WithValidCredentials_ReturnsAccountDeleted()
        {
            var userToDelete = await _userApiHelper.CreateUserAsync();

            var request = new RestRequest(ApiConstants.DeleteAccountEndpoint, Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("_method", "DELETE");
            request.AddParameter("email", userToDelete.Email);
            request.AddParameter("password", userToDelete.Password);

            var response = await Client.ExecuteAsync<ApiResponse>(request);

            AssertStatusCode(response, HttpStatusCode.OK);
            response.Data!.ResponseCode.Should().Be(200);
            response.Data.Message.Should().Be("Account deleted!");
        }
    }
}
