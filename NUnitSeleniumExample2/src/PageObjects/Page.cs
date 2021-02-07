using OpenQA.Selenium;
using System;
using WS.Examples.Tests.Selenium.Common;

//
// Test page classes.
// 
// Subject: Software Developer Portfolio
// Author: Wesley Scott
//
namespace WS.Examples.Tests.Selenium.PageObjects
{
    /// <summary>
    /// Class representing a Web page or partial Web page.
    /// </summary>
    public abstract class Page
    {
        private Uri uri;
        /// <summary>
        /// Reference to a Selenium Web driver.
        /// </summary>
        protected IWebDriver driver;
        /// <summary>
        /// The maximum time to wait for the page to load.
        /// </summary>
        protected TimeSpan pageTimeout;
        /// <summary>
        /// The maximum time to wait for a page element to become available.
        /// </summary>
        protected TimeSpan elementTimeout;

        /// <summary>
        /// Create a page object.
        /// </summary>
        /// <param name="driver">
        /// Reference to a Selenium Web driver.
        /// </param>
        /// <param name="url">
        /// The base URL of the Web application.
        /// </param>
        /// <param name="pageTimeout">
        /// The maximum time to wait for the page to load.
        /// </param>
        /// <param name="elementTimeout">
        /// The maximum time to wait for a page element to become available.
        /// </param>
        public Page(IWebDriver driver, string url, TimeSpan pageTimeout, TimeSpan elementTimeout)
        {
            this.driver = driver;
            uri = new Uri(url);

            this.pageTimeout = pageTimeout;
            this.elementTimeout = elementTimeout;
        }

        /// <summary>
        /// Navigate to the page.
        /// </summary>
        public void Navigate()
        {
            driver.Navigate().GoToUrl(uri.AbsoluteUri);
            SeleniumTestUtils.WaitForUrl(driver, pageTimeout, uri.AbsoluteUri);
        }

        /// <summary>
        /// Get the base URL of the page.
        /// </summary>
        /// <returns>
        /// The base URL of the page as a string.
        /// </returns>
        public string GetBaseUrl()
        {
            return uri.GetLeftPart(UriPartial.Authority);
        }
    }
}