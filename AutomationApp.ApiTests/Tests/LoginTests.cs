using Allure.NUnit;
using Allure.NUnit.Attributes;
using AutomationApp.ApiTests.Helpers;
using AutomationApp.ApiTests.Models;
using AutomationApp.ApiTests.Models.Users;
using AutomationApp.ApiTests.Utilities;
using FluentAssertions;
using RestSharp;
using System.Net;

namespace AutomationApp.ApiTests.Tests
{
    [AllureNUnit]
    [AllureSuite("API Tests")]
    [AllureSubSuite("Login")]
    [Category("LoginApi")]
    public class LoginTests : BaseTest
    {
        private UserModel _testUser = null!;
        private UserApiHelper _userApiHelper = null!;

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
        public async Task Login_WithValidCredentials_ReturnsUserExists()
        {
            var request = new RestRequest(ApiConstants.VerifyLoginEndpoint);
            request.AddParameter("email", _testUser.Email);
            request.AddParameter("password", _testUser.Password);
            var response = await Client.ExecutePostAsync<ApiResponse>(request);

            AssertStatusCode(response, HttpStatusCode.OK);
            response.Data.Should().NotBeNull();
            response.Data!.ResponseCode.Should().Be(200);
            response.Data.Message.Should().Be("User exists!");
        }

        [TestCase("email")]
        [TestCase("password")]
        public async Task Login_WithMissingParameter_ReturnsBadRequest(string missingParam)
        {
            var request = new RestRequest(ApiConstants.VerifyLoginEndpoint);

            if (missingParam == "email")
            {
                request.AddParameter("password", _testUser.Password);
            }
            else if (missingParam == "password")
            {
                request.AddParameter("email", _testUser.Email);
            }
            else
            {
                throw new ArgumentException("Invalid parameter name for test case.");
            }


            var response = await Client.ExecutePostAsync<ApiResponse>(request);

            AssertStatusCode(response, HttpStatusCode.OK);
            response.Data.Should().NotBeNull();
            response.Data!.ResponseCode.Should().Be(400);
            response.Data!.Message.Should().Be("Bad request, email or password parameter is missing in POST request.");
        }

        [Test]
        public async Task Login_WithInvalidEmailAndValidPassword_ReturnsUserNotFound()
        {
            var request = new RestRequest(ApiConstants.VerifyLoginEndpoint);
            request.AddParameter("email", "invalid@email.com");
            request.AddParameter("password", _testUser.Password);
            var response = await Client.ExecutePostAsync<ApiResponse>(request);

            AssertStatusCode(response, HttpStatusCode.OK);
            response.Data!.ResponseCode.Should().Be(404);
            response.Data.Message.Should().Be("User not found!");
        }

        [Test]
        public async Task Login_WithValidEmailAndInvalidPassword_ReturnsUserNotFound()
        {
            var request = new RestRequest(ApiConstants.VerifyLoginEndpoint);
            request.AddParameter("email", _testUser.Email);
            request.AddParameter("password", "wrongpassword");
            var response = await Client.ExecutePostAsync<ApiResponse>(request);

            AssertStatusCode(response, HttpStatusCode.OK);
            response.Data!.ResponseCode.Should().Be(404);
            response.Data.Message.Should().Be("User not found!");
        }
        [Test]
        public async Task Login_WithInvalidCredentials_ReturnsUserNotFound()
        {
            var request = new RestRequest(ApiConstants.VerifyLoginEndpoint);
            request.AddParameter("email", "invalidemail@test.com");
            request.AddParameter("password", "wrongpassword");
            var response = await Client.ExecutePostAsync<ApiResponse>(request);

            AssertStatusCode(response, HttpStatusCode.OK);
            response.Data.Should().NotBeNull();
            response.Data!.ResponseCode.Should().Be(404);
            response.Data!.Message.Should().Be("User not found!");
        }

        [Test]
        public async Task Login_WithDeleteMethod_Returns405InResponseBody()
        {
            var request = new RestRequest(ApiConstants.VerifyLoginEndpoint);
            var response = await Client.ExecuteDeleteAsync<ApiResponse>(request);

            AssertStatusCode(response, HttpStatusCode.OK);
            response.Data.Should().NotBeNull();
            response.Data!.ResponseCode.Should().Be(405);
            response.Data!.Message.Should().Be("This request method is not supported.");
        }
    }
}
