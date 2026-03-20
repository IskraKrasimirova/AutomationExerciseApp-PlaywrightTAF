using Microsoft.Playwright;
using System.Text.RegularExpressions;
using static Microsoft.Playwright.Assertions;

namespace AutomationApp.UiTests.Pages
{
    public class ProductDetailsPage : BasePage
    {
        private ILocator ProductDetails => _page.Locator(".product-details");
        private ILocator ProductImage => ProductDetails.Locator(".view-product").GetByRole(AriaRole.Img);
        private ILocator ProductName => ProductDetails.GetByRole(AriaRole.Heading);
        private ILocator Category => ProductDetails.GetByText("Category:");
        private ILocator Price => ProductDetails.GetByText("Rs.");
        private ILocator Availability => ProductDetails.GetByText("Availability:");
        private ILocator Quantity => ProductDetails.GetByText("Quantity:");
        private ILocator QuantityInput => ProductDetails.Locator("#quantity");
        private ILocator Condition => ProductDetails.GetByText("Condition:");
        private ILocator Brand => ProductDetails.GetByText("Brand:");
        private ILocator AddToCartButton => ProductDetails.GetByRole(AriaRole.Button, new() { Name = "Add to Cart" });

        private ILocator WriteYourReviewTab => _page.GetByRole(AriaRole.Link, new() { Name = "Write Your Review" });
        private ILocator ReviewForm => _page.Locator("#review-form");
        private ILocator ReviewNameInput => ReviewForm.GetByPlaceholder("Your Name");
        private ILocator ReviewEmailInput => ReviewForm.GetByPlaceholder("Email Address");
        private ILocator ReviewTextInput => ReviewForm.GetByPlaceholder("Add Review Here!");
        private ILocator SubmitReviewButton => ReviewForm.GetByRole(AriaRole.Button, new() { Name = "Submit" });

        public ProductDetailsPage(IPage page) : base(page)
        {
        }

        public async Task SetQuantity(int quantity)
        {
            await QuantityInput.ClearAsync();
            await QuantityInput.FillAsync(quantity.ToString());
        }

        public async Task AddToCart()
        {
            await AddToCartButton.ClickAsync();
        }

        public async Task VerifyIsAtProductDetailsPage()
        {
            await Expect(_page).ToHaveURLAsync(new Regex("/product_details/\\d+"));
            await VerifyProductDetailsAreVisible();
            await VerifyProductReviewFormIsVisible();
        }

        public async Task VerifyProductDetailsAreVisible()
        {
            await Expect(ProductImage).ToBeVisibleAsync();
            await Expect(ProductName).ToBeVisibleAsync();
            await Expect(Category).ToBeVisibleAsync();
            await Expect(Price).ToBeVisibleAsync();
            await Expect(Availability).ToBeVisibleAsync();
            await Expect(Quantity).ToBeVisibleAsync();
            await Expect(QuantityInput).ToBeVisibleAsync();
            await Expect(AddToCartButton).ToBeVisibleAsync();
            await Expect(Condition).ToBeVisibleAsync();
            await Expect(Brand).ToBeVisibleAsync();
        }

        public async Task VerifyProductReviewFormIsVisible()
        {
            await Expect(WriteYourReviewTab).ToBeVisibleAsync();
            await Expect(ReviewNameInput).ToBeVisibleAsync();
            await Expect(ReviewEmailInput).ToBeVisibleAsync();
            await Expect(ReviewTextInput).ToBeVisibleAsync();
            await Expect(SubmitReviewButton).ToBeVisibleAsync();
        }

        public async Task VerifyProductNameIs(string expectedName)
        {
            await Expect(ProductName).ToContainTextAsync(expectedName);
        }

        public async Task VerifyProductPriceIs(string expectedPrice)
        {
            await Expect(Price).ToContainTextAsync(expectedPrice);
        }

        public async Task VerifyProductImageIs(string expectedSrc)
        {
            await Expect(ProductImage).ToHaveAttributeAsync("src", expectedSrc);
        }
    }
}
