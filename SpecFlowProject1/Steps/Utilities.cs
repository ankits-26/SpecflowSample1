using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using TechTalk.SpecFlow;

namespace SpecFlowProject1.Steps
{
    static class Utilities
    {
        static string msg = "";

        public static void WaitForElementToBeVisible(IWebDriver driver, string xpath)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(xpath)));
            }
            catch (Exception e)
            {
                Assert.Fail("Element not visible, Exception: " + e.StackTrace);
            }
        }

        public static void ReportStep(ReportStatus status, string message)
        {
            string color = "black";

            switch (status)
            {
                case ReportStatus.PASS:
                    color = "green";
                    break;
                case ReportStatus.FAIL:
                    color = "red";
                    break;
                case ReportStatus.INFO:
                    color = "blue";
                    break;
                case ReportStatus.WARNING:
                    color = "orange";
                    break;

            }
            msg = msg +$"<html><body><p style=\"color:{color}\"> {message} </p></body></html>";
        }

        public static string GetStepDetails()
        {
            return msg;
        }

        public static void ClearMessage()
        {
            msg = "";
        }

        public enum ReportStatus
        {
            PASS,
            FAIL,
            WARNING,
            INFO,
            SKIP
        }
    }


}
