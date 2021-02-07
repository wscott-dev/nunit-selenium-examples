using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
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

        /// <summary>
        /// Create a CARFAX search results page object.
        /// </summary>
        /// <param name="driver">
        /// Reference to a Selenium Web driver.
        /// </param>
        /// <param name="baseUrl">
        /// The base URL of the Web application.
        /// </param>
        /// <param name="pageTimeout">
        /// The maximum time to wait for the page to load.
        /// </param>
        /// <param name="elementTimeout">
        /// The maximum time to wait for an element to become avialable.
        /// </param>
        public SearchResultsPage(IWebDriver driver, string baseUrl, TimeSpan pageTimeout, TimeSpan elementTimeout) :
            base(driver, baseUrl, pageTimeout, elementTimeout)
        {
        }

        /// <summary>
        /// Get the page header text.
        /// </summary>
        /// <returns>
        /// The page header text with leading and trailing whitespace removed.
        /// </returns>
        public string GetHeaderText()
        {
            SeleniumTestUtils.WaitForPresenceOfElement(driver, elementTimeout, By.XPath("//h1"));
            SeleniumTestUtils.HighlightElement(driver, h1);

            return h1.Text.Trim();
        }

        /// <summary>
        /// Get the selected car make.
        /// </summary>
        /// <returns>
        /// The name of a car make.
        /// </returns>
        public string GetSelectedMake()
        {
            SeleniumTestUtils.WaitForPresenceOfElement(driver, elementTimeout, By.XPath("//select[@name='make']"));
            SeleniumTestUtils.HighlightElement(driver, makeSelector);

            var selector = new SelectElement(makeSelector);
            return selector.SelectedOption.Text.Trim();
        }

        /// <summary>
        /// Get the selected car model.
        /// </summary>
        /// <returns>
        /// The name of a car model.
        /// </returns>
        public string GetSelectedModel()
        {
            SeleniumTestUtils.WaitForPresenceOfElement(driver, elementTimeout, By.XPath("//select[@name='model']"));
            SeleniumTestUtils.HighlightElement(driver, modelSelector);

            var selector = new SelectElement(modelSelector);
            return selector.SelectedOption.Text.Trim();
        }

        /// <summary>
        /// Get the zip code field value.
        /// </summary>
        /// <returns>
        /// A zip code.
        /// </returns>
        public string GetZipCode()
        {
            SeleniumTestUtils.WaitForPresenceOfElement(driver, elementTimeout, By.XPath("//input[@name='zip']"));
            SeleniumTestUtils.HighlightElement(driver, zipCodeInput);

            return zipCodeInput.GetAttribute("value").Trim();
        }
    }
}