using OpenQA.Selenium;
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
    /// Class representing the CARFAX used car search second step page.
    /// </summary>
    public class CarsForSale2Page : Page
    {
        // Suppress 'field never assigned a value' warnings.
#pragma warning disable 0649
        [FindsBy(How = How.XPath,
            Using = "//button[descendant-or-self::text()[normalize-space(.)='Show Me']]")]
        private readonly IWebElement showMeButton;
#pragma warning restore 0649

        /// <summary>
        /// Create the CARFAX 'Cars For Sale' step 2 page object.
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
        /// The Maximum time to wait for the page to load.
        /// </param>
        /// <param name="elementTimeout">
        /// The Maximum time to wait for an element on the page to become available.
        /// </param>
        public CarsForSale2Page(IWebDriver driver, string baseUrl, string path, TimeSpan pageTimeout, TimeSpan elementTimeout) :
            base(driver, TestUtils.AppendUrl(baseUrl, path), pageTimeout, elementTimeout)
        {
        }

        /// <summary>
        /// Click the 'Show Me' button for the specified make and model.
        /// </summary>
        /// <param name="make">
        /// A car make name.
        /// </param>
        /// <param name="model">
        /// A car model name.
        /// </param>
        /// <returns>
        /// Reference to a search results page object.
        /// </returns>
        public Page ClickShowMeButton(string make, string model)
        {
            /*
            * Replace characters to match the application url for the search page.
            */
            make = make.Replace(' ', '-');
            make = make.Replace('/', '-');
            model = model.Replace(' ', '-');
            model = model.Replace('/', '-');

            /*
		    * Wait for the 'Show Me' button text to contain the number of matched vehicles using a regular expression;
		    * matching zero vehicles will cause a timeout exception.
		    */
            SeleniumTestUtils.WaitForElementTextToMatch(driver, elementTimeout, By.XPath("//span[@class='totalRecordsText']"), "^[1-9][0-9]+$");

            SeleniumTestUtils.Click(driver, elementTimeout, showMeButton);

            SeleniumTestUtils.WaitForUrlToMatch(driver, pageTimeout, $".*{make}.*{model}.*");
            return PageFactory.Instance.GetPage(driver, driver.Url, PageFactory.SEARCH_RESULTS_PAGE, pageTimeout, elementTimeout);
        }
    }
}