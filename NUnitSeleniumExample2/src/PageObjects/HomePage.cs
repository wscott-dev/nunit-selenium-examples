using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using WS.Examples.Tests.Common;
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
    /// Class representing the CARFAX home page.
    /// </summary>
    public class HomePage : Page
    {
        // Suppress 'field never assigned a value' warnings.
#pragma warning disable 0649
        [FindsBy(How = How.XPath,
            Using = "//*[@class='cgh-link-tag' and descendant-or-self::text()[normalize-space(.)='Used Cars for Sale']]")]
        private readonly IWebElement usedCarsForSaleMenu;

        [FindsBy(How = How.XPath,
            Using = "//*[@class='cgh-link-tag' and descendant-or-self::text()[normalize-space(.)='CARFAX Reports']]")]
        private readonly IWebElement CARFAXReportsMenu;

        [FindsBy(How = How.XPath,
            Using = "//*[@class='cgh-link-tag' and descendant-or-self::text()[normalize-space(.)='My Car Maintenance']]")]
        private readonly IWebElement myCarMaintenanceMenu;
#pragma warning restore 0649

        public HomePage(IWebDriver driver, string baseUrl, TimeSpan pageTimeout, TimeSpan elementTimeout) :
            base(driver, baseUrl, pageTimeout, elementTimeout)
        {
        }

        public CarsForSale1Page ClickUsedCarsForSaleMenu()
        {
            SeleniumTestUtils.Click(driver, elementTimeout, usedCarsForSaleMenu);
            SeleniumTestUtils.WaitForUrl(driver, pageTimeout, TestUtils.AppendUrl(GetBaseUrl(), PageFactory.CARS_FOR_SALE1));
            return (CarsForSale1Page)PageFactory.Instance.GetPage(driver, GetBaseUrl(), PageFactory.CARS_FOR_SALE1, pageTimeout, elementTimeout);
        }
    }
}