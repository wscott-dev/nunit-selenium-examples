
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using WS.Examples.Tests.Selenium.Common;

//
// CARFAX element presence tests.
// 
// Subject: Software Developer Portfolio
// Author: Wesley Scott
//
namespace WS.Examples.Tests.Selenium.ElementPresenceTests
{
    /// <summary>
    /// Test class containing tests which verify the presence of Web elements with
    /// specified values on a Web page.
    /// </summary>
    [TestFixture]
    [NonParallelizable]
    public partial class ElementPresenceTests : SeleniumTestFixture
    {
        /// <summary>
        /// Initialize the element presence test suite fixture.
        /// </summary>
        [OneTimeSetUp]
        public void Init()
        {
            Initialize();
        }

        /// <summary>
        /// Verify that all toolbar menus are present.
        /// </summary>
        /// <param name="menuName">
        /// A toolbar menu name.
        /// </param>
        [Test, Order(1)]
        [TestCaseSource("MenuElementTestData")]
        public void MenuElementPresenceTest(string menuName)
        {
            try
            {
                // Only navigate to page if necessary.
                if (!String.Equals(WebDriver.Url, Args.AppURL))
                {
                    WebDriver.Navigate().GoToUrl(Args.AppURL);
                }

                SeleniumTestUtils.WaitForPageToLoad(WebDriver, Args.PageTimeout);

                string menuXPath = "//*[@class='cgh-link-tag' and descendant-or-self::text()[normalize-space(.)='" +
                    menuName + "']]";

                var wait = new WebDriverWait(WebDriver, Args.ElementTimeout);
                IWebElement menu = wait.Until(
                    SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(menuXPath)));

                Actions actions = new Actions(WebDriver);
                actions.MoveToElement(menu).Perform();

                SeleniumTestUtils.HighlightElement(WebDriver, menu);

                if (Args.DelayMillis > 0)
                {
                    Thread.Sleep(Args.DelayMillis);
                }
            }
            catch (Exception e)
            {
                Assert.Fail($"Could not find toolbar menu '{menuName}'; reason: '{e.Message}'");
            }
        }

        /// <summary>
        /// Verify that all toolbar sub menus are present.
        /// </summary>
        /// <param name="menuName">
        /// A toolbar menu name.
        /// </param>
        /// <param name="subMenuName">
        /// A toolbar sub menu name.
        /// </param>
        [Test, Order(2)]
        [TestCaseSource("SubMenuElementTestData")]
        public void SubMenuElementPresenceTest(string menuName, string subMenuName)
        {
            try
            {
                // Only navigate to page if necessary.
                if (!String.Equals(WebDriver.Url, Args.AppURL))
                {
                    WebDriver.Navigate().GoToUrl(Args.AppURL);
                }

                SeleniumTestUtils.WaitForPageToLoad(WebDriver, Args.PageTimeout);

                string menuXPath = "//*[@class='cgh-link-tag' and descendant-or-self::text()[normalize-space(.)='" +
                    menuName + "']]";

                var wait = new WebDriverWait(WebDriver, Args.ElementTimeout);
                IWebElement menu = wait.Until(
                    SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(menuXPath)));

                Actions actions = new Actions(WebDriver);
                actions.MoveToElement(menu).Perform();
            }
            catch (Exception e)
            {
                Assert.Fail($"Could not find toolbar menu '{menuName}'; reason: '{e.Message}'");
            }

            try
            {
                string subMenuXPath = "//*[@class='cgh-link-tag' and descendant-or-self::text()[normalize-space(.)='" +
                    menuName + "']]/..//a[descendant-or-self::text()[normalize-space(.)=\"" + subMenuName + "\"]]";

                var wait = new WebDriverWait(WebDriver, Args.ElementTimeout);
                IWebElement subMenu = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(subMenuXPath)));

                Actions actions = new Actions(WebDriver);
                actions.MoveToElement(subMenu).Perform();

                SeleniumTestUtils.HighlightElement(WebDriver, subMenu);

                if (Args.DelayMillis > 0)
                {
                    Thread.Sleep(Args.DelayMillis);
                }
            }
            catch (Exception e)
            {
                Assert.Fail($"Could not find sub menu '{subMenuName}' under toolbar menu '{menuName}'; reason: '{e.Message}'");
            }
        }

        /// <summary>
        /// Verify the existence of page hyperlink elements.
        /// </summary>
        /// <param name="pageSegment">
        /// A Web page segment/leaf name.
        /// </param>
        /// <param name="title">
        /// Optional hyperlink element title.
        /// </param>
        /// <param name="text">
        /// Optional hyperlink text.
        /// </param>
        /// <param name="position">
        /// Optional position of the hyperlink on the Web page.
        /// </param>
        [Test, Order(3)]
        [TestCaseSource("HyperlinkElementTestData")]
        public void HyperlinkPresenceTest(string pageSegment, string title, string text, int position)
        {
            try
            {
                string pageUrl = Args.AppURL;

                if (!pageSegment.Equals("home"))
                {
                    pageUrl = Tests.Common.TestUtils.AppendUrl(Args.AppURL, pageSegment);
                }

                // Only navigate to new page if necessary.
                if (!String.Equals(WebDriver.Url, pageUrl))
                {
                    WebDriver.Navigate().GoToUrl(pageUrl);
                }

                SeleniumTestUtils.WaitForPageToLoad(WebDriver, Args.PageTimeout);

                SeleniumVerificationUtils.VerifyHyperlinkElementExistence(WebDriver, Args.ElementTimeout, title, text, position);

                if (Args.DelayMillis > 0)
                {
                    Thread.Sleep(Args.DelayMillis);
                }
            }
            catch (Exception e)
            {
                Assert.Fail($"Could not find hyperlink element page='{pageSegment}', " +
                    $"title='{title}', text='{text}', position='{position}'; reason: '{e.Message}'");
            }
        }

        /// <summary>
        /// Clean up resources associated with the test fixture.
        /// </summary>
        [OneTimeTearDown]
        public void Cleanup()
        {
            Dispose();
        }
    }
}
