using Allure.NUnit;
using AutomationApp.UiTests.Models.Factories;
using AutomationApp.UiTests.Pages;

namespace AutomationApp.UiTests.Tests
{
    [AllureNUnit]
    [Category("Order")]
    public class OrderTests : BaseTest
    {
        private HomePage _homePage;
        private ProductsPage _productsPage;
        private CartModal _cartModal;
        private CartPage _cartPage;
        private CheckoutModal _checkoutModal;
        private LoginPage _loginPage;
        private SignupPage _signupPage;
        private AccountCreatedPage _accountCreatedPage;
        private AccountDeletedPage _accountDeletedPage;
        private CheckoutPage _checkoutPage;
        private PaymentPage _paymentPage;
        private OrderConfirmationPage _orderConfirmationPage;

        [SetUp]
        public async Task TestSetUp()
        {
            _homePage = new HomePage(Page);
            _productsPage = new ProductsPage(Page);
            _cartModal = new CartModal(Page);
            _cartPage = new CartPage(Page);
            _checkoutModal = new CheckoutModal(Page);
            _loginPage = new LoginPage(Page);
            _signupPage = new SignupPage(Page);
            _accountCreatedPage = new AccountCreatedPage(Page);
            _accountDeletedPage = new AccountDeletedPage(Page);
            _checkoutPage = new CheckoutPage(Page);
            _paymentPage = new PaymentPage(Page);
            _orderConfirmationPage = new OrderConfirmationPage(Page);

            await Page.GotoAsync("/");
            await _homePage.AcceptCookiesIfPresent();
        }

        [Test]
        [Category("E2E")]
        public async Task RegisterDuringCheckout_PlacesOrderSuccessfully()
        {
            // Add product to cart
            await _homePage.VerifyIsAtHomePage();
            await _homePage.NavBar.GoToProductsPage();
            await _productsPage.VerifyIsAtProductsPage();

            var productName = await _productsPage.GetFirstProductName();
            var productPrice = await _productsPage.GetFirstProductPrice();
            await _productsPage.HoverAndAddToCart(0);
            await _cartModal.VerifyIsVisible();
            await _cartModal.ViewCart();

            // Proceed to checkout
            await _cartPage.VerifyIsAtCartPage();
            await _cartPage.VerifyProductsCount(1);
            await _cartPage.VerifyProductInCart(0, productName, productPrice, "1", productPrice);
            await _cartPage.ProceedToCheckout();
            await _checkoutModal.VerifyIsVisible();
            await _checkoutModal.ClickRegisterLogin();

            // Register
            var newUser = UserFactory.CreateDefault();
            await _loginPage.VerifyIsAtLoginPage();
            await _loginPage.Signup(newUser.Name, newUser.Email);
            await _signupPage.VerifyIsAtSignupPage(newUser.Name, newUser.Email);
            await _signupPage.CreateAccount(newUser);
            await _accountCreatedPage.VerifyAccountCreated();
            await _accountCreatedPage.ClickContinue();
            await _homePage.VerifyIsAtHomePage();
            await _homePage.NavBar.VerifyUserIsLoggedIn(newUser.Name);

            // Go back to cart and checkout
            await _homePage.NavBar.GoToCartPage();
            await _cartPage.VerifyIsAtCartPage();
            await _cartPage.VerifyProductsCount(1);
            await _cartPage.ProceedToCheckout();
            await _checkoutPage.VerifyIsAtCheckoutPage();
            await _checkoutPage.EnterComment("Test order comment");
            await _checkoutPage.PlaceOrder();

            // Payment
            var paymentDetails = PaymentFactory.CreateDefault();
            await _paymentPage.VerifyIsAtPaymentPage();
            await _paymentPage.EnterPaymentDetailsAndConfirm(paymentDetails);

            // Confirm order
            await _orderConfirmationPage.VerifyIsAtOrderConfirmationPage();
            await _orderConfirmationPage.ClickContinue();

            // Delete account
            await _homePage.VerifyIsAtHomePage();
            await _homePage.NavBar.DeleteAccount();
            await _accountDeletedPage.VerifyAccountDeleted();
            await _accountDeletedPage.ClickContinue();
            await _homePage.VerifyIsAtHomePage();
        }

        [Test]
        [Category("E2E")]
        public async Task RegisterBeforeCheckout_PlacesOrderSuccessfully()
        {
            // Register
            var newUser = UserFactory.CreateDefault();
            await _homePage.VerifyIsAtHomePage();
            await _homePage.NavBar.GoToLoginPage();
            await _loginPage.VerifyIsAtLoginPage();
            await _loginPage.Signup(newUser.Name, newUser.Email);
            await _signupPage.VerifyIsAtSignupPage(newUser.Name, newUser.Email);
            await _signupPage.CreateAccount(newUser);
            await _accountCreatedPage.VerifyAccountCreated();
            await _accountCreatedPage.ClickContinue();
            await _homePage.VerifyIsAtHomePage();
            await _homePage.NavBar.VerifyUserIsLoggedIn(newUser.Name);

            // Add product to cart
            await _homePage.NavBar.GoToProductsPage();
            await _productsPage.VerifyIsAtProductsPage();

            var productName = await _productsPage.GetFirstProductName();
            var productPrice = await _productsPage.GetFirstProductPrice();
            await _productsPage.HoverAndAddToCart(0);
            await _cartModal.VerifyIsVisible();
            await _cartModal.ViewCart();

            // Checkout
            await _cartPage.VerifyIsAtCartPage();
            await _cartPage.VerifyProductsCount(1);
            await _cartPage.VerifyProductInCart(0, productName, productPrice, "1", productPrice);
            await _cartPage.ProceedToCheckout();
            await _checkoutPage.VerifyIsAtCheckoutPage();
            await _checkoutPage.EnterComment("Test order comment");
            await _checkoutPage.PlaceOrder();

            // Payment
            var paymentDetails = PaymentFactory.CreateDefault();
            await _paymentPage.VerifyIsAtPaymentPage();
            await _paymentPage.EnterPaymentDetailsAndConfirm(paymentDetails);

            // Confirm order
            await _orderConfirmationPage.VerifyIsAtOrderConfirmationPage();
            await _orderConfirmationPage.ClickContinue();

            // Delete account
            await _homePage.VerifyIsAtHomePage();
            await _homePage.NavBar.DeleteAccount();
            await _accountDeletedPage.VerifyAccountDeleted();
            await _accountDeletedPage.ClickContinue();
            await _homePage.VerifyIsAtHomePage();
        }
    }
}
