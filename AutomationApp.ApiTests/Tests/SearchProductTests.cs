using Allure.NUnit.Attributes;
using AutomationApp.ApiTests.Models;
using AutomationApp.ApiTests.Models.Products;
using AutomationApp.ApiTests.Utilities;
using FluentAssertions;
using RestSharp;
using System.Net;

namespace AutomationApp.ApiTests.Tests
{
    [AllureSuite("API Tests")]
    [AllureSubSuite("SearchProduct")]
    [Category("SearchProductApi")]
    public class SearchProductTests: BaseTest
    {
        [TestCase("top")]
        [TestCase("tshirt")]
        [TestCase("jean")]
        public async Task SearchProduct_WithValidSearchTerm_ReturnsProducts(string searchTerm)
        {
            var request = new RestRequest(ApiConstants.SearchProductEndpoint);
            request.AddParameter("search_product", searchTerm);
            var response = await Client.ExecutePostAsync<GetProductsResponse>(request);

            AssertStatusCode(response, HttpStatusCode.OK);
            response.Data.Should().NotBeNull();
            response.Data!.Products.Should().NotBeEmpty();
            response.Data.Products.Should().OnlyContain(p =>
                p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                p.Category.Category.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }

        [Test]
        public async Task SearchProduct_WithoutSearchTerm_Returns400InResponseBody()
        {
            var request = new RestRequest(ApiConstants.SearchProductEndpoint);
            var response = await Client.ExecutePostAsync<ApiResponse>(request);

            AssertStatusCode(response, HttpStatusCode.OK);
            response.Data.Should().NotBeNull();
            response.Data!.ResponseCode.Should().Be(400);
            response.Data!.Message.Should().Be("Bad request, search_product parameter is missing in POST request.");
        }
    }
}
