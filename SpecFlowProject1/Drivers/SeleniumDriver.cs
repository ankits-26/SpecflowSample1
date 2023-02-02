using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SpecFlowProject1.Hooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SpecFlowProject1.Drivers
{
    public class SeleniumDriver
    {
        public IWebDriver driver;
        ScenarioContext _scenarioContext;
        public SeleniumDriver(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }
        public IWebDriver Setup()
        {
            driver = new ChromeDriver();
            _scenarioContext.Set(driver, "WebDriver");
            driver.Manage().Window.Maximize();
            return driver;
        }
    }
}
