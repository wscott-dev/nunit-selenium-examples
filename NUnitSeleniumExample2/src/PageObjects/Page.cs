using OpenQA.Selenium;
using System;
using WS.Examples.Tests.Selenium.Common;

/// <summary>
/// Test page classes.
/// 
/// Subject: Software Developer Portfolio
/// Author: Wesley Scott
/// </summary>
namespace WS.Examples.Tests.Selenium.PageObjects
{
    /// <summary>
    /// Class representing a Web page or partial Web page.
    /// </summary>
    public abstract class Page
    {
        protected Uri uri;
        protected IWebDriver driver;
        protected TimeSpan pageTimeout;
        protected TimeSpan elementTimeout;

        public Page(IWebDriver driver, string url, TimeSpan pageTimeout, TimeSpan elementTimeout)
        {
            this.driver = driver;
            uri = new Uri(url);

            this.pageTimeout = pageTimeout;
            this.elementTimeout = elementTimeout;
        }

        public void Navigate()
        {
            driver.Navigate().GoToUrl(uri.AbsoluteUri);
            SeleniumTestUtils.WaitForUrl(driver, pageTimeout, uri.AbsoluteUri);
        }

        public string GetBaseUrl()
        {
            return uri.GetLeftPart(UriPartial.Authority);
        }
    }
}