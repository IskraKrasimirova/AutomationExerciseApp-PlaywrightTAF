using AutomationApp.ApiTests.Helpers;
using AutomationApp.Common.Models;
using AutomationApp.Common.Utilities;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace AutomationApp.ApiTests.Utilities
{
    public class DependencyContainer
    {
        public static IServiceCollection RegisterDependencies()
        {
            var services = new ServiceCollection();

            services.AddSingleton(sp => ConfigurationSettings.Instance.SettingsModel);

            services.AddSingleton(sp =>
            {
                var settings = sp.GetRequiredService<SettingsModel>();

                var options = new RestClientOptions(settings.ApiBaseUrl);
                var client = new RestClient(options, configureSerialization: s => s.UseNewtonsoftJson());
                client.AddDefaultHeader("Accept", "application/json");
                return client;
            });

            services.AddScoped<UserApiHelper>();

            return services;
        }
    }
}
