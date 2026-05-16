using AutomationApp.ApiTests.Utilities;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using System.Net;

namespace AutomationApp.ApiTests.Tests
{
    public class BaseTest
    {
        protected IServiceProvider ServiceProvider = null!;
        protected RestClient Client = null!;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var services = DependencyContainer.RegisterDependencies();
            ServiceProvider = services.BuildServiceProvider();
            Client = ServiceProvider.GetRequiredService<RestClient>();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Client.Dispose();

            if (ServiceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        protected void AssertStatusCode(RestResponse response, HttpStatusCode expectedStatusCode)
        {
            response.StatusCode.Should().Be(expectedStatusCode);
        }
    }
}
