using Allure.NUnit;
using Allure.NUnit.Attributes;
using AutomationApp.ApiTests.Models;
using AutomationApp.ApiTests.Models.Brands;
using AutomationApp.ApiTests.Utilities;
using FluentAssertions;
using RestSharp;
using System.Net;

namespace AutomationApp.ApiTests.Tests
{
    [AllureNUnit]
    [AllureSuite("API Tests")]
    [AllureSubSuite("Brands")]
    [Category("BrandsApi")]
    public class BrandsTests : BaseTest
    {
        private RestResponse<GetBrandsResponse> _response = null!;

        [SetUp]
        public async Task TestSetUp()
        {
            var request = new RestRequest(ApiConstants.BrandsListEndpoint);
            _response = await Client.ExecuteGetAsync<GetBrandsResponse>(request);
        }

        [Test]
        public void GetAllBrands_ReturnsStatusCode200()
        {
            AssertStatusCode(_response, HttpStatusCode.OK);
        }

        [Test]
        public void GetAllBrands_ReturnsNonEmptyBrandsList()
        {
            _response.Data.Should().NotBeNull();
            _response.Data!.Brands.Should().NotBeEmpty();
        }

        [Test]
        public void GetAllBrands_EachBrandHasRequiredFields()
        {
            _response.Data.Should().NotBeNull();
            _response.Data!.Brands.Should().NotBeEmpty();

            foreach (var brand in _response.Data.Brands)
            {
                brand.Id.Should().BeGreaterThan(0);
                brand.Brand.Should().NotBeNullOrWhiteSpace();
            }
        }

        [Test]
        public async Task PutToAllBrands_Returns405InResponseBody()
        {
            var request = new RestRequest(ApiConstants.BrandsListEndpoint);
            var response = await Client.ExecutePutAsync<ApiResponse>(request);

            AssertStatusCode(response, HttpStatusCode.OK);
            response.Data.Should().NotBeNull();
            response.Data!.ResponseCode.Should().Be(405);
            response.Data!.Message.Should().Be("This request method is not supported.");
        }
    }
}
