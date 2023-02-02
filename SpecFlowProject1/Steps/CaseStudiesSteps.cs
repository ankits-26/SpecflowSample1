using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SpecFlowProject1.Drivers;
using System;
using System.Configuration;
using System.Threading;
using TechTalk.SpecFlow;

namespace SpecFlowProject1.Steps
{
    [Binding]
    public class CaseStudiesSteps
    {
        public IWebDriver driver;
        ScenarioContext _scenarioContext;
        public CaseStudiesSteps(ScenarioContext scenarioContext){
            _scenarioContext = scenarioContext;
        }

        //public IWebDriver driver;
        [Given(@"I launch Cognizant home page")]
        public void GivenILaunchCognizantHomePage()
        {
            try
            {
                driver = _scenarioContext.Get<IWebDriver>("WebDriver");
                string url = "https://www.cognizant.com/uk/en";
                //launch browser and maximize
                driver.Navigate().GoToUrl(url);
                Utilities.ReportStep(Utilities.ReportStatus.PASS,$"-- Launched url: {url}");
            }
            catch (Exception e)
            {
                Utilities.ReportStep(Utilities.ReportStatus.FAIL, $"--Launch failed, getting exception as: {e.Message}");
                Assert.Fail();
            }
        }

        [When(@"I click on ""(.*)"" link")]
        public void WhenIClickOnLink(string linkName)
        {
            try
            {
                string xapthLink = "//*[@id='skipToFooter']//a[contains(text(),'" + linkName + "')]";
                Utilities.WaitForElementToBeVisible(driver, xapthLink);
                //click on the linkname provided through feature
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", driver.FindElement(By.XPath(xapthLink)));
                //driver.FindElement(By.XPath(xapthLink)).Click();
                Utilities.ReportStep(Utilities.ReportStatus.PASS, $"-- clicked on {linkName} link");
            }
            catch
            {
                Utilities.ReportStep(Utilities.ReportStatus.WARNING, $"-- clicked on {linkName} link but with exception");

            }
        }

        [When(@"Select Industry Type as ""(.*)""")]
        public void WhenSelectIndustryTypeAs(string indusType)
        {
            try
            {
                string xapthIndusType = "//*[@id='dropdownMenuButtonCaseStudy']/span";
                Utilities.WaitForElementToBeVisible(driver, xapthIndusType);
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", driver.FindElement(By.XPath("//*[@id='dropdownMenuButtonCaseStudy']/span")));

                //driver.FindElement(By.XPath(xapthIndusType)).Click();
                IWebElement we = driver.FindElement(By.XPath("//*[@id='dropdownMenuButtonCaseStudy']/../ul/li/a[contains(text(),'"+ indusType + "')]"));
                Utilities.WaitForElementToBeVisible(driver, "//*[@id='dropdownMenuButtonCaseStudy']/../ul/li/a[contains(text(),'"+ indusType + "')]");
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", we);
                Utilities.ReportStep(Utilities.ReportStatus.PASS, $"-- Industry type selected as {indusType}");
                //select item as per the input provided
                //SelectElement dropDown = new SelectElement(driver.FindElement(By.XPath(xapthIndusType)));
                //dropDown.SelectByValue(indusType);
            }
            catch (Exception e)
            {
                Utilities.ReportStep(Utilities.ReportStatus.FAIL, $"--Failed to select on {indusType} dropdown, getting exception as: {e.Message}");
                Assert.Fail();
            }

        }

        [Then(@"I verify all the cards are related to ""(.*)""")]
        public void ThenIVerifyAllTheResultsAreRelatedTo(string indusType)
        {
            try
            {
                string cardName = "";
                string failMsg = "";
                string passMsg = "";
                bool flagFail = false;
                string xapthPagination = "//li[contains(@class,'page-item pagination-item')]/a[@class='page-link text-regular']";
                Utilities.WaitForElementToBeVisible(driver, xapthPagination);
                //get total number of pages
                int pageCount = driver.FindElements(By.XPath(xapthPagination)).Count;
                Utilities.ReportStep(Utilities.ReportStatus.INFO, $"-- Total page count: {pageCount}");

                for (int i = 1; i <= pageCount; i++) //for pagination
                {
                    //get total count of cards available on a page
                    int cardCounts = driver.FindElements(By.XPath("//*[@id='dynamic-card-container']/ul[1]/li")).Count;
                    Utilities.ReportStep(Utilities.ReportStatus.INFO, $"-- Total card count is {cardCounts} on page {i}");
                    for (int j = 1; j <= cardCounts; j++) //for total cards present in a page
                    {
                        cardName = driver.FindElement(By.XPath("//*[@id='dynamic-card-container']/ul[1]/li[" + j + "]/a[contains(@aria-label,'" + indusType + "')]")).GetAttribute("aria-label");
                        if (!cardName.Contains(indusType))
                        {
                            Utilities.ReportStep(Utilities.ReportStatus.FAIL, $"-- '{cardName}' is not a part of '{indusType}' Industry type. <br>");
                            //failMsg = failMsg + $"-- '{cardName}' is not a part of '{indusType}' Industry type. <br>";
                            flagFail = true;
                        }
                        else 
                        {
                            Utilities.ReportStep(Utilities.ReportStatus.PASS, $" -- '{cardName}' is a part of '{indusType}' Industry type. <br>");
                            //passMsg += $" -- '{cardName}' is a part of '{indusType}' Industry type. <br>";
                        }
                    }
                    if (i == pageCount) { break; }
                    //click on next button for pagination
                    IWebElement we = driver.FindElement(By.XPath("//span[@class='arrow-right cmp-icon m-0']"));
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", we);
                    Utilities.ReportStep(Utilities.ReportStatus.INFO, $"-- Pagination performed, navigated to page {i+1}");
                    Utilities.WaitForElementToBeVisible(driver, "//*[@id='dynamic-card-container']/ul[1]/li");
                    //driver.FindElement(By.XPath("//span[@class='arrow-right cmp-icon m-0']")).Click();
                }

                if (flagFail)
                {
                    //Utilities.ReportStep(Utilities.ReportStatus.FAIL, failMsg);
                    Assert.Fail();
                }
                else 
                {
                    //Utilities.ReportStep(Utilities.ReportStatus.PASS, passMsg);
                }
            }
            catch (Exception e)
            {
                Utilities.ReportStep(Utilities.ReportStatus.FAIL, "Failed to validate the searched results, getting exception as: " + e.Message);
                Assert.Fail();
            }
        }

    }
}
