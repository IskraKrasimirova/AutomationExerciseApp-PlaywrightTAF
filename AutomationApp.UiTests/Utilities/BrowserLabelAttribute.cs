using Allure.Net.Commons;
using NUnit.Framework.Interfaces;

namespace AutomationApp.UiTests.Utilities
{
    public class BrowserLabelAttribute : NUnitAttribute, ITestAction
    {
        public ActionTargets Targets => ActionTargets.Test;

        public void BeforeTest(ITest test)
        {
            var browser = Environment.GetEnvironmentVariable("BROWSER") ?? UiConstants.BrowserChromium;

            AllureLifecycle.Instance.UpdateTestCase(x =>
            {
                x.labels.Add(new Label { name = "browser", value = browser });
                x.labels.Add(new Label { name = "suite", value = $"UI Tests - {browser}" });
            });
        }

        public void AfterTest(ITest test)
        {
        }
    }
}
