using AutomationApp.Common.Utilities;
using AutomationApp.UiTests.Pages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Playwright;

namespace AutomationApp.UiTests.Utilities
{
    public class DependencyContainer
    {
        public static IServiceCollection CreateServices(IPage page)
        {
            var services = new ServiceCollection();
            // Settings
            services.AddSingleton(sp => ConfigurationSettings.Instance.SettingsModel);

            // Page
            services.AddSingleton<IPage>(page);

            // Pages
            RegisterPages(services);

            return services;
        }

        private static void RegisterPages(ServiceCollection services)
        {
            services.AddScoped(sp =>
            {
                var page = sp.GetRequiredService<IPage>();
                return new HomePage(page);
            });

            services.AddScoped<LoginPage>();
            services.AddScoped<SignupPage>();
            services.AddScoped<AccountCreatedPage>();
            services.AddScoped<AccountDeletedPage>();
            services.AddScoped<BrandProductsPage>();
            services.AddScoped<CartModal>();
            services.AddScoped<CartPage>();
            services.AddScoped<CategoryProductsPage>();
            services.AddScoped<CheckoutModal>();
            services.AddScoped<CheckoutPage>();
            services.AddScoped<ContactUsPage>();
            services.AddScoped<OrderConfirmationPage>();
            services.AddScoped<PaymentPage>();
            services.AddScoped<ProductDetailsPage>();
            services.AddScoped<ProductsPage>();
        }
    }
}
