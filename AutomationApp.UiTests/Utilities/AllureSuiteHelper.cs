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
            //var className = TestContext.CurrentContext.Test.ClassName?.Split('.').Last() ?? "UnknownClass";

            //AllureLifecycle.Instance.UpdateTestCase(x =>
            //{
            //    x.labels.RemoveAll(l =>
            //        l.name == "suite" ||
            //        l.name == "parentSuite" ||
            //        l.name == "subSuite"
            //    );

            //    x.labels.Add(Label.ParentSuite("UI Tests"));
            //    x.labels.Add(Label.Suite(browser));
            //    x.labels.Add(Label.SubSuite(className));
            //});
        }
    }
}
