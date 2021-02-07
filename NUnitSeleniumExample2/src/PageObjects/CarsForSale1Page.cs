using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using WS.Examples.Tests.Common;
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
    /// Class representing the CARFAX used car search initial page.
    /// </summary>
    public class CarsForSale1Page : Page
    {
        // Suppress 'field never assigned a value' warnings.
#pragma warning disable 0649
        [FindsBy(How = How.XPath, Using = "//select[@name='make']")]
        private readonly IWebElement makeSelector;
        [FindsBy(How = How.XPath, Using = "//select[@name='model']")]
        private readonly IWebElement modelSelector;
        [FindsBy(How = How.XPath, Using = "//input[@name='zip']")]
        private readonly IWebElement zipCodeInput;
        [FindsBy(How = How.XPath, Using = "//button[descendant-or-self::text()[normalize-space(.)='Next']]")]
        private readonly IWebElement nextButton;
#pragma warning restore 0649

        /// <summary>
        /// Create the initial CARFAX 'Cars For Sale' page object.
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
        /// The maximum time to wait for an element on the page to become available.
        /// </param>
        public CarsForSale1Page(IWebDriver driver, string baseUrl, string path, TimeSpan pageTimeout, TimeSpan elementTimeout) :
            base(driver, TestUtils.AppendUrl(baseUrl, path), pageTimeout, elementTimeout)

        {
        }

        /// <summary>
        /// Select a car make.
        /// </summary>
        /// <param name="make">
        /// The name of the make to select.
        /// </param>
        public void Select_Make(string make)
        {
            var selector = new SelectElement(makeSelector);

            SeleniumTestUtils.HighlightElement(driver, makeSelector);

            // work round for selection issues when selecting same make
            selector.SelectByIndex(1);
            selector.SelectByIndex(selector.Options.Count - 1);

            selector.SelectByValue(make);
        }

        /// <summary>
        /// Select a car model.
        /// </summary>
        /// <param name="model">
        /// The name of the model to select.
        /// </param>
        public void Select_Model(string model)
        {
            var selector = new SelectElement(modelSelector);

            SeleniumTestUtils.HighlightElement(driver, modelSelector);

            selector.SelectByValue(model);
        }

        /// <summary>
        /// Type a zip code into the zip code input field.
        /// </summary>
        /// <param name="zip">
        /// The zip code to type.
        /// </param>
        public void Type_ZipCode(string zip)
        {
            SeleniumTestUtils.HighlightElement(driver, zipCodeInput);

            zipCodeInput.Click();

            // clear any previous input
            zipCodeInput.SendKeys(Keys.Control + "a");
            zipCodeInput.SendKeys(Keys.Delete);

            zipCodeInput.SendKeys(zip);
        }

        /// <summary>
        /// Click the page next button.
        /// </summary>
        /// <returns>
        /// A reference to the next page object.
        /// </returns>
        public Page Click_Next_Button()
        {
            SeleniumTestUtils.Click(driver, elementTimeout, nextButton);
            SeleniumTestUtils.WaitForUrl(driver, pageTimeout, TestUtils.AppendUrl(GetBaseUrl(), PageFactory.CARS_FOR_SALE2));
            return (CarsForSale2Page)PageFactory.Instance.GetPage(driver, GetBaseUrl(), PageFactory.CARS_FOR_SALE2, pageTimeout, elementTimeout);
        }
    }
}