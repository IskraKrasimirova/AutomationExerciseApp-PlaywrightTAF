using Allure.Net.Commons;

namespace AutomationApp.UiTests.Utilities
{
    public class AllureSuiteHelper
    {
        public static void ApplySuiteLabels()
        {
            var browser = Environment.GetEnvironmentVariable("BROWSER") ?? UiConstants.BrowserChromium;
            var className = TestContext.CurrentContext.Test.ClassName?.Split('.').Last() ?? "Unknown";
            AllureApi.AddParentSuite("UI Tests");
            AllureApi.AddSuite(browser);
            AllureApi.AddSubSuite(className);
            AllureApi.AddTestParameter("browser", browser);
        }
    }
}
