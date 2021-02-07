using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WS.Examples.Tests.Common;

//
// Common Selenium test classes.
// 
// Subject: Software Developer Portfolio
// Author: Wesley Scott
//
namespace WS.Examples.Tests.Selenium.Common
{
    /// <summary>
    /// Type of Web browser.
    /// </summary>
    public enum BrowserType
    {
        /// <summary>
        /// Google Chrome browser.
        /// </summary>
        Chrome,

        /// <summary>
        /// Non-display Google Chrome browser.
        /// </summary>
        HeadlessChrome,

        /// <summary>
        /// Unknown browser type.
        /// </summary>
        Unknown
    }

    /// <summary>
    /// Class representing a Selenium test configuration.
    /// </summary>
    /// <remarks>
    /// This class is mapped from a JSON file.
    /// </remarks>
    public class SeleniumTestConfig
    {
        private string configDir;
        private string url;
        private int pageTimeoutSeconds;
        private int elementTimeoutSeconds;
        private int delayMillis;
        private string browserName;

        /// <summary>
        /// Configuration directory name.
        /// </summary>
        public string ConfigDir { get => configDir; set => configDir = value; }

        /// <summary>
        /// Application Web address.
        /// </summary>
        public string URL { get => url; set => url = value; }

        /// <summary>
        /// Maximum number of seconds to wait for page loads.
        /// </summary>
        public int PageTimeoutSeconds { get => pageTimeoutSeconds; set => pageTimeoutSeconds = value; }

        /// <summary>
        /// Maximum number of seconds to wait for Web elements to become available.
        /// </summary>
        public int ElementTimeoutSeconds { get => elementTimeoutSeconds; set => elementTimeoutSeconds = value; }

        /// <summary>
        /// Number of milliseconds to delay test execution.
        /// </summary>
        public int DelayMillis { get => delayMillis; set => delayMillis = value; }

        /// <summary>
        /// Name of the browser to use for testing.
        /// </summary>
        public string BrowserName { get => browserName; set => browserName = value; }
    }

    /// <summary>
    /// Selenium test arguments.
    /// </summary>
    public class SeleniumTestArgs
    {
        /// <summary>
        /// Name of the configuration.
        /// </summary>
        public string CfgName;

        /// <summary>
        /// Configuration directory name.
        /// </summary>
        public string ConfigDir;

        /// <summary>
        /// Web address of application under test.
        /// </summary>
        public string AppURL;

        /// <summary>
        /// Maximum time to wait for page loads.
        /// </summary>
        public TimeSpan PageTimeout;

        /// <summary>
        /// Maximum time to wait for browser element to become available.
        /// </summary>
        public TimeSpan ElementTimeout;

        /// <summary>
        /// Number of milliseconds to delay test execution.
        /// </summary>
        public int DelayMillis;

        /// <summary>
        /// Type of browser to launch.
        /// </summary>
        public BrowserType BrowserType;
    }

    /// <summary>
    /// Test fixture responsible for creating a Web driver and loading test
    /// arguments from an external configuration file.
    /// </summary>
    public abstract class SeleniumTestFixture : IDisposable
    {
        private SeleniumTestArgs args;
        private IWebDriver driver;

        /// <summary>
        /// Initialize the Selenium test fixture by loading the test configuration
        /// and creating a connection to the specified Web driver type.
        /// </summary>
        public void Initialize()
        {
            /*
            * Work around for the following:
            * The type initializer for 'System.IO.Compression.ZipStorer' throws an exception
            * Install System.Text.Encoding.CodePages
            */
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            string appDir = AppDomain.CurrentDomain.BaseDirectory;
            string jsonFilePath = Path.GetFullPath(Path.Combine(
                appDir, "cfg", TestUtils.GetEnvValue(EnvVars.TEST_CFG_FILENAME)));

            args = SeleniumTestUtils.GetTestArgs(jsonFilePath)[0];

            driver = SeleniumTestUtils.GetWebDriver(args.BrowserType);
            if (driver == null)
            {
                throw new WebDriverNotFoundException(BrowserType.Chrome.ToString());
            }
        }

        /// <summary>
        /// Dispose of the Selenium test fixture and associated Web driver.
        /// </summary>
        public void Dispose()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }
        }

        /// <summary>
        /// Get/Set Selenium test fixture arguments.
        /// </summary>
        public SeleniumTestArgs Args { get => args; set => args = value; }

        /// <summary>
        /// Get the Web driver associated with the Selenium test fixture.
        /// </summary>
        public IWebDriver WebDriver { get => driver; }
    }

    /// <summary>
    /// Class representing elements displayed on a Web page.
    /// </summary>
    public class PageElements
    {
        private List<HyperlinkElement> hyperLinks;

        /// <summary>
        /// List of input field names or values.
        /// </summary>
        public List<HyperlinkElement> Hyperlinks { get => hyperLinks; set => hyperLinks = value; }
    }

    /// <summary>
    /// Class representing an element on a Web page.
    /// </summary>
    public abstract class BaseElement
    {
        private string name;
        private string text;
        private int position;

        /// <summary>
        /// Get/set the Web element name.
        /// </summary>
        public string Name { get => name; set => name = value; }

        /// <summary>
        /// Get/set the Web element text value.
        /// </summary>
        public string Text { get => text; set => text = value; }

        /// <summary>
        ///  Get/set the Web element position.
        /// </summary>
        public int Position { get => position; set => position = value; }
    }

    /// <summary>
    /// Class representing a hyperlink element on a Web page.
    /// </summary>
    public class HyperlinkElement : BaseElement
    {
        private string title;

        /// <summary>
        /// Get/set the element title.
        /// </summary>
        public string Title { get => title; set => title = value; }
    }

    /// <summary>
    /// Class representing a missing Web driver exceptional condition.
    /// </summary>
    [Serializable]
    public class WebDriverNotFoundException : TestException
    {
        /// <summary>
        /// Create an exception indicating that the Web driver executable was not found.
        /// </summary>
        /// <param name="message">
        /// An exception message.
        /// </param>
        public WebDriverNotFoundException(string message) : base(message)
        {
        }

        /// <summary>
        /// Get the exception message with a specialized prefix.
        /// </summary>
        public override string Message
        {
            get
            {
                return ($"Could not find Web driver: {base.Message}");
            }
        }
    }

    /// <summary>
    /// Class representing a match of more than one element exceptional condition.
    /// </summary>
    [Serializable]
    public class MultipleElementsMatchedException : TestException
    {
        private readonly int count;

        /// <summary>
        /// Create an exception indicating that more than one Web element was matched.
        /// </summary>
        /// <param name="count">
        /// The number of Web elements matched.
        /// </param>
        /// <param name="message">
        /// An exception message.
        /// </param>
        public MultipleElementsMatchedException(int count, string message) : base(message)
        {
            this.count = count;
        }

        /// <summary>
        /// Get the exception message with a specialized prefix.
        /// </summary>
        public override string Message
        {
            get
            {
                return ($"Multiple elements ({count}) matched: {base.Message}");
            }
        }
    }
}