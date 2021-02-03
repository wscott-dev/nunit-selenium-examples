using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;

/// <summary>
/// Common Selenium test verification utilities.
/// 
/// Subject: Software Developer Portfolio
/// Author: Wesley Scott
/// </summary>
namespace WS.Examples.Tests.Selenium.Common
{
    /// <summary>
    /// Class containing Selenium Web element verification utilities.
    /// </summary>
    public static class SeleniumVerificationUtils
    {
        /// <summary>
        /// Verify the presence of a hyperlink element on a web page by matching text within the element.
        /// </summary>
        /// <param name="driver">
        /// Reference to a Web driver.
        /// </param>
        /// <param name="timeout">
        /// Maximum amount of time to wait for the specified element to be found.
        /// </param>
        /// <param name="title">
        /// A text string.
        /// </param>
        /// <param name="position">
        /// The position of the element on the page.
        /// </param>
        public static void VerifyHyperlinkElementExistence(IWebDriver driver, TimeSpan timeout,
            string title, string text, int position)
        {
            string xPath = $"//*[(self::a";

            if (!string.IsNullOrEmpty(title))
            {
                xPath += " and @title='{title}'";
            }

            if (!string.IsNullOrEmpty(text))
            {
                xPath += $" and (normalize-space(text())) = '{text}'";
            }

            xPath += ")]";

            if (position > 0)
            {
                xPath = "(" + xPath;
                xPath += $")[{position}]";
            }

            var wait = new WebDriverWait(driver, timeout);
            ReadOnlyCollection<IWebElement> elements = wait.Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath(xPath)));

            if (elements.Count > 1)
            {
                throw new MultipleElementsMatchedException(elements.Count,
                    $"XPath '{xPath}' matched more than one element.");
            }

            SeleniumTestUtils.HighlightElement(driver, elements[0]);
        }
    }
}