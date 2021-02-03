using OpenQA.Selenium;
using System;

/// <summary>
/// Test page factory classes.
/// 
/// Subject: Software Developer Portfolio
/// Author: Wesley Scott
/// </summary>
namespace WS.Examples.Tests.Selenium.PageObjects
{
    /// <summary>
    /// Factory class used to construct all test Web proxy pages.
    /// </summary>
    public sealed class PageFactory
    {
        public const string HOME = "home";
        public const string CARS_FOR_SALE1 = "cars-for-sale";
        public const string CARS_FOR_SALE2 = "cars-for-sale#step=2";
        public const string SEARCH_RESULTS_PAGE = "search-results";
        public const string ERROR = "error";

        private PageFactory()
        {
        }

        private static readonly Lazy<PageFactory> lazy = new Lazy<PageFactory>(() => new PageFactory());
        public static PageFactory Instance
        {
            get
            {
                return lazy.Value;
            }
        }

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