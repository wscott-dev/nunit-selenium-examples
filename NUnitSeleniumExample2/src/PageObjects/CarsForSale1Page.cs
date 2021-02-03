using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
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
        [FindsBy(How = How.XPath, Using = "//input[@type='checkbox' and @name='cpo']")]
        private readonly IWebElement certifiedCBox;
        [FindsBy(How = How.XPath, Using = "//button[descendant-or-self::text()[normalize-space(.)='Next']]")]
        private readonly IWebElement nextButton;
#pragma warning restore 0649

        public CarsForSale1Page(IWebDriver driver, string baseUrl, string path, TimeSpan pageTimeout, TimeSpan elementTimeout) :
            base(driver, TestUtils.AppendUrl(baseUrl, path), pageTimeout, elementTimeout)

        {
        }

        public void Select_Make(string make)
        {
            var selector = new SelectElement(makeSelector);

            SeleniumTestUtils.HighlightElement(driver, makeSelector);

            // work round for selection issues when selecting same make
            selector.SelectByIndex(1);
            selector.SelectByIndex(selector.Options.Count - 1);

            selector.SelectByValue(make);
        }

        public void Select_Model(string model)
        {
            var selector = new SelectElement(modelSelector);

            SeleniumTestUtils.HighlightElement(driver, modelSelector);

            selector.SelectByValue(model);
        }

        public void Type_ZipCode(string zip)
        {
            SeleniumTestUtils.HighlightElement(driver, zipCodeInput);

            zipCodeInput.Click();

            // clear any previous input
            zipCodeInput.SendKeys(Keys.Control + "a");
            zipCodeInput.SendKeys(Keys.Delete);

            zipCodeInput.SendKeys(zip);
        }

        public Page Click_Next_Button()
        {
            SeleniumTestUtils.Click(driver, elementTimeout, nextButton);
            SeleniumTestUtils.WaitForUrl(driver, pageTimeout, TestUtils.AppendUrl(GetBaseUrl(), PageFactory.CARS_FOR_SALE2));
            return (CarsForSale2Page)PageFactory.Instance.GetPage(driver, GetBaseUrl(), PageFactory.CARS_FOR_SALE2, pageTimeout, elementTimeout);
        }
    }
}