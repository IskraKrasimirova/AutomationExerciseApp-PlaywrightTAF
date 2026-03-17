using AutomationApp.ApiTests.Models;
using FluentAssertions;
using RestSharp;
using System.Net;
using AutomationApp.ApiTests.Utilities;
using AutomationApp.ApiTests.Models.Products;

namespace AutomationApp.ApiTests.Tests
{
    [Category("ProductsApi")]
    public class ProductsTests : BaseTest
    {
        private RestResponse<GetProductsResponse> _response;

        [SetUp]
        public async Task TestSetUp()
        {
            var request = new RestRequest(ApiConstants.ProductsListEndpoint);
            _response = await Client.ExecuteGetAsync<GetProductsResponse>(request);
        }

        [Test]
        public void GetAllProducts_ReturnsStatusCode200()
        {
            AssertStatusCode(_response, HttpStatusCode.OK);
        }

        [Test]
        public void GetAllProducts_ReturnsNonEmptyProductsList()
        {
            _response.Data.Should().NotBeNull();
            _response.Data!.Products.Should().NotBeEmpty();
        }

        [Test]
        public void GetAllProducts_EachProductHasRequiredFields()
        {
            _response.Data.Should().NotBeNull();
            _response.Data!.Products.Should().NotBeEmpty();

            foreach (var product in _response.Data.Products)
            {
                product.Id.Should().BeGreaterThan(0);
                product.Name.Should().NotBeNullOrWhiteSpace();
                product.Price.Should().NotBeNullOrWhiteSpace();
                product.Brand.Should().NotBeNullOrWhiteSpace();
                product.Category.Should().NotBeNull();
                product.Category.Category.Should().NotBeNullOrWhiteSpace();
                product.Category.UserType.Should().NotBeNull();
                product.Category.UserType.UserType.Should().NotBeNullOrWhiteSpace();
            }
        }

        [Test]
        public async Task PostToAllProducts_Returns405InResponseBody()
        {
            var request = new RestRequest(ApiConstants.ProductsListEndpoint);
            var response = await Client.ExecutePostAsync<ErrorResponse>(request);

            AssertStatusCode(response, HttpStatusCode.OK);
            response.Data.Should().NotBeNull();
            response.Data!.ResponseCode.Should().Be(405);
            response.Data!.Message.Should().Be("This request method is not supported.");
        }
    }
}
