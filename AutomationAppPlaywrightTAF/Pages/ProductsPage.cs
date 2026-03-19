using Microsoft.Playwright;
using System.Text.RegularExpressions;
using static Microsoft.Playwright.Assertions;

namespace AutomationApp.UiTests.Pages
{
    public class ProductsPage : BasePage
    {
        private ILocator AllProductsHeader => _page.GetByRole(AriaRole.Heading, new() { Name = "All Products" });
        private ILocator ProductsList => _page.Locator(".features_items");
        
        private ILocator SearchProductInput => _page.GetByPlaceholder("Search Product");
        private ILocator SearchButton => _page.Locator("#submit_search");
        private ILocator CategoryHeader => _page.GetByRole(AriaRole.Heading, new() { Name = "Category" });
        private ILocator BrandsHeader => _page.GetByRole(AriaRole.Heading, new() { Name = "Brands" });
        private ILocator SaleImage => _page.Locator("#sale_image");

        private ILocator FirstProductViewButton => ProductsList.Locator(".product-image-wrapper").First.GetByRole(AriaRole.Link, new() { Name = "View Product" });

        private ILocator SearchedProductsHeader => _page.GetByRole(AriaRole.Heading, new() { Name = "Searched Products" });
        private ILocator ProductItems => ProductsList.Locator(".product-image-wrapper");
        private ILocator ProductItem(int index) => ProductItems.Nth(index);
        private ILocator AddToCartButtonForProduct(int index) => ProductItem(index).Locator(".product-overlay a.add-to-cart");
        private ILocator ProductInfo(int index) => ProductItem(index).Locator(".productinfo.text-center");

        
        public ProductsPage(IPage page) : base(page)
        {
        }

        public async Task<string> GetProductName(int index) => await ProductInfo(index).Locator("p").InnerTextAsync();
        public async Task<string> GetProductPrice(int index) => await ProductInfo(index).Locator("h2").InnerTextAsync();
        private async Task<string?> GetProductImage(int index) => await ProductInfo(index).Locator("img").GetAttributeAsync("src");

        public async Task<string> GetFirstProductName() => await GetProductName(0);
        public async Task<string> GetFirstProductPrice() => await GetProductPrice(0);
        public async Task<string?> GetFirstProductImageSrc() => await GetProductImage(0);

        public async Task ClickViewFirstProduct()
        {
            await FirstProductViewButton.ClickAsync();
        }

        public async Task SearchProduct(string searchTerm)
        {
            await SearchProductInput.FillAsync(searchTerm);
            await SearchButton.ClickAsync();
        }

        public async Task HoverAndAddToCart(int productIndex)
        {
            var product = ProductItem(productIndex);
            await product.HoverAsync();
            await AddToCartButtonForProduct(productIndex).ClickAsync();
        }

        public async Task VerifyIsAtProductsPage()
        {
            await Expect(_page).ToHaveURLAsync("/products");
            await Expect(AllProductsHeader).ToBeVisibleAsync();
            await Expect(ProductsList).ToBeVisibleAsync();
            await Expect(SearchProductInput).ToBeVisibleAsync();
            await Expect(SearchButton).ToBeVisibleAsync();
            await Expect(CategoryHeader).ToBeVisibleAsync();
            await Expect(BrandsHeader).ToBeVisibleAsync();
            await Expect(SaleImage).ToBeVisibleAsync();
        }

        public async Task VerifySearchResultsAreDisplayed(string searchTerm)
        {
            var encodedSearchTerm = Uri.EscapeDataString(searchTerm);
            await Expect(_page).ToHaveURLAsync(new Regex($"products\\?search={encodedSearchTerm}"));
            await Expect(SearchedProductsHeader).ToBeVisibleAsync();
            await Expect(ProductsList).ToBeVisibleAsync();

            var count = await ProductItems.CountAsync();
            Assert.That(count, Is.GreaterThan(0), "No products found for the search term.");
        }
    }
}
