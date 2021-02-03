using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using WS.Examples.Tests.Common;

/// <summary>
/// Common Selenium test utilities.
/// 
/// Subject: Software Developer Portfolio
/// Author: Wesley Scott
/// </summary>
namespace WS.Examples.Tests.Selenium.Common
{
    /// <summary>
    /// Class containing test utility methods.
    /// </summary>
    public static class SeleniumTestUtils
    {
        private static string configDir;

        /// <summary>
        /// Get the configuration directory name from the test configuration file.
        /// </summary>
        /// <returns></returns>
        public static string GetConfigDir()
        {
            if (string.IsNullOrEmpty(configDir))
            {
                string jsonFilePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    "cfg", TestUtils.GetEnvValue(EnvVars.TEST_CFG_FILENAME)));

                configDir = SeleniumTestUtils.GetTestArgs(jsonFilePath)[0].ConfigDir;
            }

            return configDir;
        }

        /// <summary>
        /// Get a Web driver for the specified browser type.
        /// </summary>
        /// <param name="browserType">
        /// A Web browser type.</param>
        /// <returns>
        /// Reference to a new Web driver.
        /// </returns>
        public static IWebDriver GetWebDriver(BrowserType browserType)
        {
            string driverDir;
            IWebDriver driver = null;

            switch (browserType)
            {
                case BrowserType.Chrome:

                    driverDir = TestUtils.GetEnvValue(EnvVars.CHROME_WEBDRIVER_DIR);
                    if (!string.IsNullOrEmpty(driverDir))
                    {
                        driver = new ChromeDriver(driverDir);
                        driver.Manage().Window.Maximize();
                    }

                    break;

                case BrowserType.HeadlessChrome:

                    driverDir = TestUtils.GetEnvValue(EnvVars.CHROME_WEBDRIVER_DIR);
                    if (!string.IsNullOrEmpty(driverDir))
                    {
                        // TODO: Need explicit size for headless testing, can get screen size from somewhere?
                        Size size = new Size(1920, 1080);

                        ChromeOptions headless_options = new ChromeOptions();
                        headless_options.AddArgument("headless");

                        driver = new ChromeDriver(driverDir, headless_options);
                        driver.Manage().Window.Size = size;
                    }

                    break;

                default:

                    driver = null;

                    break;
            }

            return (driver);
        }

        /// <summary>
        /// Load test arguments from the specified configuration file.
        /// </summary>
        /// <param name="cfgFilePath">
        /// Absolute path to a JSON test configuration file.
        /// </param>
        /// <returns>
        /// A list of argument sets; one for each configuration/browser
        /// type combination.
        /// </returns>
        public static IList<SeleniumTestArgs> GetTestArgs(string cfgFilePath)
        {
            List<SeleniumTestArgs> argsList = new List<SeleniumTestArgs>();
            Dictionary<string, SeleniumTestConfig> TestConfigs;

            TestConfigs = JsonConvert.DeserializeObject<Dictionary<string, SeleniumTestConfig>>(File.ReadAllText(cfgFilePath));

            foreach (string cfgName in TestConfigs.Keys)
            {
                SeleniumTestConfig cfg = TestConfigs[cfgName];

                SeleniumTestArgs args = new SeleniumTestArgs
                {
                    CfgName = cfgName,
                    ConfigDir = cfg.ConfigDir,
                    AppURL = cfg.URL,
                    PageTimeout = TimeSpan.FromSeconds(cfg.PageTimeoutSeconds),
                    ElementTimeout = TimeSpan.FromSeconds(cfg.ElementTimeoutSeconds),
                    DelayMillis = cfg.DelayMillis
                };

                args.BrowserType = cfg.BrowserName switch
                {
                    "Chrome" => BrowserType.Chrome,
                    "HeadlessChrome" => BrowserType.HeadlessChrome,
                    _ => BrowserType.Unknown,
                };

                argsList.Add(args);
            }

            return (argsList);
        }

        /// <summary>
        /// Wait for a Web page to load.
        /// </summary>
        /// <param name="driver">
        /// Reference to a Web driver.
        /// </param>
        /// <param name="timeout">
        /// Maximum time to wait for page to load.
        /// </param>
        public static void WaitForPageToLoad(IWebDriver driver, TimeSpan timeout)
        {
            WebDriverWait wait = new WebDriverWait(driver, timeout);
            wait.Until(e => ((IJavaScriptExecutor)e).ExecuteScript("return (document.readyState == 'complete')"));
        }

        /// <summary>
        /// Wait for the current URL to match the specified URL.
        /// </summary>
        /// <param name="driver">
        /// Reference to a Web driver.
        /// </param>
        /// <param name="timeout">
        /// Maximum time to wait for the URL to match.
        /// </param>
        /// <param name="url">
        /// The URL to wait for.
        /// </param>
        public static void WaitForUrl(IWebDriver driver, TimeSpan timeout, string url)
        {
            string regex = $"^{url}\\/?$";

            WebDriverWait wait = new WebDriverWait(driver, timeout);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlMatches(regex));
        }

        /// <summary>
        /// Wait for the current URL to match to specified regular expression.
        /// </summary>
        /// <param name="driver">
        /// Reference to a Web driver.
        /// </param>
        /// <param name="timeout">
        /// Maximum time to wait for the URL to contain the specified pattern.
        /// </param>
        /// <param name="regex">
        /// A regular expression.
        /// </param>
        public static void WaitForUrlToMatch(IWebDriver driver, TimeSpan timeout, string regex)
        {
            WebDriverWait wait = new WebDriverWait(driver, timeout);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlMatches(regex));
        }

        /// <summary>
        /// Wait for the specified Web element to become clickable.
        /// </summary>
        /// <param name="driver">
        /// Reference to a Web driver.
        /// </param>
        /// <param name="timeout">
        /// Maximum time to wait for the specified element to become clickable.
        /// </param>
        /// <param name="element">
        /// Reference to a Web element.
        /// </param>
        public static void WaitForElementToBeClickable(IWebDriver driver, TimeSpan timeout, IWebElement element)
        {
            WebDriverWait wait = new WebDriverWait(driver, timeout);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
        }

        /// <summary>
        /// Wait for the specified Web element to come in to existence.
        /// </summary>
        /// <param name="driver">
        /// Reference to a Web driver.
        /// </param>
        /// <param name="timeout">
        /// Maximum time to wait for the specified element to come in to existence.
        /// </param>
        /// <param name="url">
        /// Reference to a Web element locator.
        /// </param>
        public static void WaitForPresenceOfElement(IWebDriver driver, TimeSpan timeout, By locator)
        {
            WebDriverWait wait = new WebDriverWait(driver, timeout);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));
        }

        /// <summary>
        /// Wait for element text to match the specified regular expression.
        /// </summary>
        /// <param name="driver">
        /// Reference to a Web driver.
        /// </param>
        /// <param name="timeout">
        ///  Maximum time to wait for the specified text to match.
        /// </param>
        /// <param name="locator">
        /// Locator used to find the Web element.
        /// </param>
        /// <param name="regex">
        /// The regular expression to match.
        /// </param>
        public static void WaitForElementTextToMatch(IWebDriver driver, TimeSpan timeout, By locator, string regex)
        {
            WebDriverWait wait = new WebDriverWait(driver, timeout);
            IWebElement element = driver.FindElement(locator);
            wait.Until(ElementTextMatches(driver, element, regex));
        }

        /// <summary>
        /// Wait for the specified Web element to become clickable.
        /// </summary>
        /// <param name="driver">
        /// Reference to a Web driver.
        /// </param>
        /// <param name="timeout">
        /// Maximum time to wait for the specified element to become clickable.
        /// </param>
        /// <param name="element">
        /// Reference to a Web element.
        /// </param>
        public static void Click(IWebDriver driver, TimeSpan timeout, IWebElement element)
        {
            WaitForElementToBeClickable(driver, timeout, element);

            SeleniumTestUtils.HighlightElement(driver, element);

            Actions actions = new Actions(driver);
            actions.MoveToElement(element);
            actions.Click(element);
            actions.Perform();
        }

        /// <summary>
        /// Highlight the border of the specified Web element for test demonstration purposes.
        /// </summary>
        /// <param name="driver">
        /// Reference to a Web driver.
        /// </param>
        /// <param name="element">
        /// Reference to the Web element to be highlighted.
        /// </param>
        public static void HighlightElement(IWebDriver driver, IWebElement element)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("arguments[0].setAttribute('style', 'border: 2px solid red;');", element);
            jse.ExecuteScript("arguments[0].scrollIntoView(false);", element);
        }

        /// <summary>
        /// Determine if the given element text matches the specified regular expression.
        /// Modeled on the <https://github.com/DotNetSeleniumTools/DotNetSeleniumExtras> library behavior,
        /// this library does not provide regex text matching capability.
        /// </summary>
        /// <param name="driver">
        /// Reference to a Web driver.
        /// </param>
        /// <param name="element">
        /// Reference to a Web element.
        /// </param>
        /// <param name="text">
        /// The regular expression to match.
        /// </param>
        /// <returns>
        /// <see langword="true"/>If the element text matches the specified regular expression, <see langword="false"/> otherwise.
        /// </returns>
        private static Func<IWebDriver, bool> ElementTextMatches(IWebDriver driver, IWebElement element, string regex)
        {
            return (driver) =>
            {
                try
                {
                    var pattern = new Regex(regex, RegexOptions.IgnoreCase);
                    var match = pattern.Match(element.Text);
                    return match.Success;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            };
        }

    }
}