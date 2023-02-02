using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SpecFlowProject1.Drivers;
using SpecFlowProject1.Steps;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace SpecFlowProject1.Hooks
{
    [Binding]
    public sealed class DependencyHooks
    {
        ScenarioContext _scenarioContext;
        public DependencyHooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }
        IWebDriver driver;
        static AventStack.ExtentReports.ExtentReports extent;
        static AventStack.ExtentReports.ExtentTest feature;
        AventStack.ExtentReports.ExtentTest scenario;
        AventStack.ExtentReports.ExtentTest step;

        static string htmlReportPath = Directory.GetParent(@"../../../").FullName
            + Path.DirectorySeparatorChar + "Result"
            + Path.DirectorySeparatorChar + "Result_" + DateTime.Now.ToString("ddMMyyyy HHmmss");
        
        [BeforeTestRun]
        public static void BeforeTestRun() {
            ExtentHtmlReporter htmlReport = new ExtentHtmlReporter(htmlReportPath);
            extent = new AventStack.ExtentReports.ExtentReports();
            extent.AddSystemInfo("Environment","QA");
            extent.AddSystemInfo("Machine", Environment.MachineName);
            extent.AddSystemInfo("OS", Environment.OSVersion.VersionString);
            extent.AttachReporter(htmlReport);
            htmlReport.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
            htmlReport.Config.ReportName = "Regression Testing";
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext context)
        {
            feature = extent.CreateTest(context.FeatureInfo.Title);
        }

        [BeforeScenario]
        public void BeforeScenario1(ScenarioContext context) {
            scenario = feature.CreateNode(context.ScenarioInfo.Title);
        }
        [BeforeScenario]
        public void BeforeScenario()
        {
            SeleniumDriver sd = new SeleniumDriver(_scenarioContext);
            sd.Setup();
        }
        [BeforeStep]
        public void BeforeStep() {
            step = scenario;
        }

        [AfterStep]
        public void AfterStep(ScenarioContext context) {
            string msg = Utilities.GetStepDetails();
            if (context.TestError == null)
            {
                step.Log(Status.Pass, context.StepContext.StepInfo.Text + msg);
            }else if (context.TestError != null)
            {
                step.Log(Status.Fail, context.StepContext.StepInfo.Text + msg);
            }
            Utilities.ClearMessage();
        }


        [AfterScenario]
        public void AfterScenario()
        {
            driver = _scenarioContext.Get<IWebDriver>("WebDriver");
            driver.Quit();
        }
        [AfterScenario]
        public void AfterScenario1()
        {
            extent.Flush();
        }
    }
}
