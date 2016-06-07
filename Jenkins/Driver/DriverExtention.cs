using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.Events;
using System;
using System.IO;
using System.Reflection;

namespace SalesTech.Automation
{
    public static class DriverExtention
    {
        public enum Browser
        {
            firefox, chrome, ie
        }

        private static Browser currentBrowser = Browser.chrome;
        public static IWebDriver WebDriver;
        public static bool isSiteIsUp;
        private static string currentFolder = Directory.GetCurrentDirectory();
        public static double ImplicitlyWait = 180;

        public static IWebDriver openDriver()
        {
            EventFiringWebDriver firingDriver = null;

            switch (currentBrowser)
            {
                case (Browser.firefox):
                    FirefoxProfile ff;
                    FirefoxProfileManager profile;
                    profile = new FirefoxProfileManager();
                    ff = profile.GetProfile("default");
                    IWebDriver ffDriver = new FirefoxDriver(new FirefoxBinary(), ff, new TimeSpan(0, 0, Convert.ToInt32(ImplicitlyWait)));
                    ffDriver = setTimeOuts(ffDriver, 1000, ImplicitlyWait, 1000);
                    firingDriver = new EventFiringWebDriver(ffDriver);
                    break;
                case (Browser.chrome):
                    IWebDriver chromedriver;

                    ChromeOptions options = new ChromeOptions();
                    chromedriver = new ChromeDriver(currentFolder, options, new TimeSpan(0, 0, Convert.ToInt32(ImplicitlyWait)));
                    chromedriver = setTimeOuts(chromedriver, 1000, ImplicitlyWait, 1000);
                    firingDriver = new EventFiringWebDriver(chromedriver);
                    break;
                case (Browser.ie):
                    IWebDriver ieDriver = new InternetExplorerDriver(currentFolder);
                    ieDriver = setTimeOuts(ieDriver, 1000, ImplicitlyWait, 1000);
                    firingDriver = new EventFiringWebDriver(ieDriver);
                    break;
            }
            firingDriver.ExceptionThrown += firingDriver_TakeScreenshotOnException;
            WebDriver = firingDriver;
            MaximizeWindow();
            isSiteIsUp = false;
            return WebDriver;
        }

        private static IWebDriver setTimeOuts(IWebDriver driver, double PageLoadTimeout, double implicitlyWait, double ScriptTimeout)
        {
            driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(PageLoadTimeout));
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(implicitlyWait));
            driver.Manage().Timeouts().SetScriptTimeout(TimeSpan.FromSeconds(ScriptTimeout));
            return driver;
        }

        internal static void MaximizeWindow()
        {
            WebDriver.Manage().Window.Maximize();
        }

        private static void firingDriver_TakeScreenshotOnException(object sender, WebDriverExceptionEventArgs e)
        {
            if (WebDriver != null)
            {
                try
                {

                    Quite();

                    throw new ApplicationException("Quitting");
                }
                catch (Exception ex)
                {

                }
            }
        }

        public static void Quite()
        {
            if (WebDriver != null)
            {
                try
                {

                    WebDriver.Close();
                    WebDriver.Quit();
                    WebDriver = null;
                }
                catch (Exception e)
                {
                }
            }
        }
    }
}
