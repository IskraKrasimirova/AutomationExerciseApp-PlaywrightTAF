using AutomationApp.UiTests.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace AutomationApp.UiTests.Hooks
{
    public class DependencyContainer
    {
        public static IServiceCollection RegisterDependencies()
        {
            var services = new ServiceCollection();
            services.AddSingleton(sp =>
            {
                return ConfigurationSettings.Instance.SettingsModel;
            });

            services.AddSingleton<PlaywrightFixture>();

            return services;
        }
    }
}
