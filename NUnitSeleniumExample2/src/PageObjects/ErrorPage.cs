using OpenQA.Selenium;
using System;
using WS.Examples.Tests.Common;

//
// Test page classes.
// 
// Subject: Software Developer Portfolio
// Author: Wesley Scott
//
namespace WS.Examples.Tests.Selenium.PageObjects
{
    /// <summary>
    /// Class representing the CARFAX error page.
    /// </summary>
    public class ErrorPage : Page
    {
        /// <summary>
        /// Create a CARFAX error page object.
        /// </summary>
        /// <param name="driver">
        /// Reference to a Selenium Web driver.
        /// </param>
        /// <param name="baseUrl">
        /// The base URL of the Web application.
        /// </param>
        /// <param name="path">
        /// The page path segment.
        /// </param>
        /// <param name="pageTimeout">
        /// The maximum time to wait for the page to load.
        /// </param>
        /// <param name="elementTimeout">
        /// The maximum time to wait for a page element to become available.
        /// </param>
        public ErrorPage(IWebDriver driver, string baseUrl, string path, TimeSpan pageTimeout, TimeSpan elementTimeout) :
            base(driver, TestUtils.AppendUrl(baseUrl, path), pageTimeout, elementTimeout)
        {
        }
    }
}