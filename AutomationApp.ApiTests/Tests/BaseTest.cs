using AutomationApp.Common.Utilities;
using FluentAssertions;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using System.Net;

namespace AutomationApp.ApiTests.Tests
{
    public class BaseTest
    {
        protected RestClient Client;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var options = new RestClientOptions(ConfigurationSettings.Instance.SettingsModel.ApiBaseUrl);
            Client = new RestClient(options, configureSerialization: s => s.UseNewtonsoftJson());
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Client.Dispose();
        }

        protected void AssertStatusCode(RestResponse response, HttpStatusCode expectedStatusCode)
        {
            response.StatusCode.Should().Be(expectedStatusCode);
        }
    }
}
