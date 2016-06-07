using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalesTech.Automation;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Tests
{
    [TestClass]
    public class WebDriverTest
    {
        IWebDriver driver;
        [TestMethod]
        public void demoTest()
        {
            driver = DriverExtention.openDriver();
            driver.Navigate().GoToUrl("https://www.google.co.il/");
            driver.FindElement(By.Id("lst-ib")).SendKeys("jenkins");
            driver.FindElement(By.Id("lst-ib")).Submit();

            driver.FindElement(By.LinkText("Jenkins")).Click();
            WaitForPageLoad();
            Assert.IsTrue(driver.PageSource.Contains("Build great things at any scale"));
            driver.Quit();
        }

        protected void WaitForPageLoad()
        {
            try
            {
                // setImplicitlyWait(implicitlyWait);
                var wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(500));
                driver.Manage().Timeouts().SetScriptTimeout(TimeSpan.FromSeconds(1500));
                wait.Until(x => ((IJavaScriptExecutor)this.driver).ExecuteScript("return document.readyState").Equals("complete"));
            }
            catch (Exception e)
            {
                //QuiteTest(e.Message);
            }
        }
    }



}
