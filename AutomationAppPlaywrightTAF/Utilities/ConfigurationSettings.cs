using AutomationApp.UiTests.Models;
using Microsoft.Extensions.Configuration;

namespace AutomationApp.UiTests.Utilities
{
    public class ConfigurationSettings
    {
        private static readonly Lazy<ConfigurationSettings> lazy = new(() => new ConfigurationSettings());

        public static ConfigurationSettings Instance => lazy.Value;

        public SettingsModel SettingsModel { get; private set; }

        private ConfigurationSettings()
        {
            var environment = Environment.GetEnvironmentVariable("environment", EnvironmentVariableTarget.User);
            var configurationFileName = string.IsNullOrEmpty(environment)
                ? "appsettings.json"
                : $"appsettings.{environment}.json";

            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile(configurationFileName, optional: true)
                .Build();

            SettingsModel = config.GetSection("Settings").Get<SettingsModel>()!;
        }
    }
}
