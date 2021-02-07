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
    /// Class representing the CARFAX home page.
    /// </summary>
    public class HomePage : Page
    {
        // Suppress 'field never assigned a value' warnings.
#pragma warning disable 0649
        [FindsBy(How = How.XPath,
            Using = "//*[@class='cgh-link-tag' and descendant-or-self::text()[normalize-space(.)='Used Cars for Sale']]")]
        private readonly IWebElement usedCarsForSaleMenu;
#pragma warning restore 0649

        /// <summary>
        /// Create a CARFAX home page object.
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
        /// The maximum time to wait for a page element to become available.
        /// </param>
        public HomePage(IWebDriver driver, string baseUrl, TimeSpan pageTimeout, TimeSpan elementTimeout) :
            base(driver, baseUrl, pageTimeout, elementTimeout)
        {
        }

        /// <summary>
        /// Click on the 'Used Cars for Sale' menu item.
        /// </summary>
        /// <returns>
        /// A reference to the initial 'Cars For Sale' page object.
        /// </returns>
        public CarsForSale1Page ClickUsedCarsForSaleMenu()
        {
            SeleniumTestUtils.Click(driver, elementTimeout, usedCarsForSaleMenu);
            SeleniumTestUtils.WaitForUrl(driver, pageTimeout, TestUtils.AppendUrl(GetBaseUrl(), PageFactory.CARS_FOR_SALE1));
            return (CarsForSale1Page)PageFactory.Instance.GetPage(driver, GetBaseUrl(), PageFactory.CARS_FOR_SALE1, pageTimeout, elementTimeout);
        }
    }
}