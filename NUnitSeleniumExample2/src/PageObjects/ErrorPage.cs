using OpenQA.Selenium;
using System;
using WS.Examples.Tests.Common;

/// <summary>
/// Test page classes.
/// 
/// Subject: Software Developer Portfolio
/// Author: Wesley Scott
/// </summary>
namespace WS.Examples.Tests.Selenium.PageObjects
{
    /// <summary>
    /// Class representing the CARFAX error page.
    /// </summary>
    public class ErrorPage : Page
    {
        public ErrorPage(IWebDriver driver, string baseUrl, string pageSegment, TimeSpan pageTimeout, TimeSpan elementTimeout) :
            base(driver, TestUtils.AppendUrl(baseUrl, pageSegment), pageTimeout, elementTimeout)
        {
        }
    }
}