using OpenQA.Selenium;
using System;

//
// Test page factory classes.
// 
// Subject: Software Developer Portfolio
// Author: Wesley Scott
//
namespace WS.Examples.Tests.Selenium.PageObjects
{
    /// <summary>
    /// Factory class used to construct all test Web proxy pages.
    /// </summary>
    public sealed class PageFactory
    {
        /// <summary>
        /// CARFAX home page.
        /// </summary>
        public const string HOME = "home";
        /// <summary>
        /// CARFAX initial 'Used Cars for Sale' page.
        /// </summary>
        public const string CARS_FOR_SALE1 = "cars-for-sale";
        /// <summary>
        /// CARFAX 'Used Cars for Sale' page step 2.
        /// </summary>
        public const string CARS_FOR_SALE2 = "cars-for-sale#step=2";
        /// <summary>
        /// CARFAX used car search results page.
        /// </summary>
        public const string SEARCH_RESULTS_PAGE = "search-results";
        /// <summary>
        /// CARFAX error page.
        /// </summary>
        public const string ERROR = "error";

        private PageFactory()
        {
        }

        private static readonly Lazy<PageFactory> lazy = new Lazy<PageFactory>(() => new PageFactory());

        /// <summary>
        /// Static reference to the page factory.
        /// </summary>
        public static PageFactory Instance
        {
            get
            {
                return lazy.Value;
            }
        }

        /// <summary>
        /// Return a CARFAX page object for the specified page parameters.
        /// </summary>
        /// <param name="driver">
        /// Reference to a Selenium Web driver.
        /// </param>
        /// <param name="baseUrl">
        /// The base URL of the Web application.
        /// </param>
        /// <param name="pageSegment">
        /// A page path segment.
        /// </param>
        /// <param name="pageTimeout">
        /// The maximum time to wait for the page to load.
        /// </param>
        /// <param name="elementTimeout">
        /// The maximum time to wait for a page element to become available.
        /// </param>
        /// <returns>
        /// Reference to page object or null if no matching page was found.
        /// </returns>
        public Page GetPage(IWebDriver driver, string baseUrl, string pageSegment,
            TimeSpan pageTimeout, TimeSpan elementTimeout)
        {
            Page page = null;

            switch (pageSegment)
            {
                case null:
                case "":
                case HOME:
                    page = new HomePage(driver, baseUrl, pageTimeout, elementTimeout);
                    break;

                case CARS_FOR_SALE1:
                    page = new CarsForSale1Page(driver, baseUrl, pageSegment, pageTimeout, elementTimeout);
                    break;

                case CARS_FOR_SALE2:
                    page = new CarsForSale2Page(driver, baseUrl, pageSegment, pageTimeout, elementTimeout);
                    break;

                case SEARCH_RESULTS_PAGE:
                    page = new SearchResultsPage(driver, baseUrl, pageTimeout, elementTimeout);
                    break;

                case ERROR:
                    page = new ErrorPage(driver, baseUrl, pageSegment, pageTimeout, elementTimeout);
                    break;

                default:
                    break;
            }

            if (page != null)
            {
                SeleniumExtras.PageObjects.PageFactory.InitElements(driver, page);
            }

            return page;
        }
    }
}