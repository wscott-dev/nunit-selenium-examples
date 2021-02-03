using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
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
    /// Class representing the CARFAX used car search results page.
    /// </summary>
    public class SearchResultsPage : Page
    {
        // Suppress 'field never assigned a value' warnings.
#pragma warning disable 0649
        [FindsBy(How = How.XPath, Using = "//h1")]
        private readonly IWebElement h1;
        [FindsBy(How = How.XPath, Using = "//select[@name='make']")]
        private readonly IWebElement makeSelector;
        [FindsBy(How = How.XPath, Using = "//select[@name='model']")]
        private readonly IWebElement modelSelector;
        [FindsBy(How = How.XPath, Using = "//input[@name='zip']")]
        private readonly IWebElement zipCodeInput;
#pragma warning restore 0649

        public SearchResultsPage(IWebDriver driver, string baseUrl, TimeSpan pageTimeout, TimeSpan elementTimeout) :
            base(driver, baseUrl, pageTimeout, elementTimeout)
        {
        }

        public string GetHeaderText()
        {
            SeleniumTestUtils.WaitForPresenceOfElement(driver, elementTimeout, By.XPath("//h1"));
            SeleniumTestUtils.HighlightElement(driver, h1);

            return h1.Text.Trim();
        }

        public string GetSelectedMake()
        {
            SeleniumTestUtils.WaitForPresenceOfElement(driver, elementTimeout, By.XPath("//select[@name='make']"));
            SeleniumTestUtils.HighlightElement(driver, makeSelector);

            var selector = new SelectElement(makeSelector);
            return selector.SelectedOption.Text.Trim();
        }

        public string GetSelectedModel()
        {
            SeleniumTestUtils.WaitForPresenceOfElement(driver, elementTimeout, By.XPath("//select[@name='model']"));
            SeleniumTestUtils.HighlightElement(driver, modelSelector);

            var selector = new SelectElement(modelSelector);
            return selector.SelectedOption.Text.Trim();
        }

        public string GetZipCode()
        {
            SeleniumTestUtils.WaitForPresenceOfElement(driver, elementTimeout, By.XPath("//input[@name='zip']"));
            SeleniumTestUtils.HighlightElement(driver, zipCodeInput);

            return zipCodeInput.GetAttribute("value").Trim();
        }
    }
}