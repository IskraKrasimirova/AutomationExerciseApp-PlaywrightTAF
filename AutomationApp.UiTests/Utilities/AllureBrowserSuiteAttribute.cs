using Allure.Net.Commons;
using NUnit.Framework.Interfaces;

namespace AutomationApp.UiTests.Utilities
{
    public class AllureBrowserSuiteAttribute: TestActionAttribute
    {
        public override void BeforeTest(ITest test)
        {
            if (test.IsSuite)
                return;

            var browser = Environment.GetEnvironmentVariable("BROWSER") ?? UiConstants.BrowserChromium;
            var className = test.ClassName?.Split('.').Last() ?? "UnknownClass";

            AllureLifecycle.Instance.UpdateTestCase(x =>
            {
                x.labels.RemoveAll(l =>
                    l.name == "suite" ||
                    l.name == "parentSuite" ||
                    l.name == "subSuite"
                );

                x.labels.Add(Label.ParentSuite("UI Tests"));
                x.labels.Add(Label.Suite(browser));
                x.labels.Add(Label.SubSuite(className));
            });
        }

        public override ActionTargets Targets => ActionTargets.Test;
    }
}
