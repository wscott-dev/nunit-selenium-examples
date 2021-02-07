
using NUnit.Framework;
using System;
using System.Threading;
using WS.Examples.Tests.Selenium.Common;
using WS.Examples.Tests.Selenium.PageObjects;

//
// CARFAX used car search tests.
// 
// Subject: Software Developer Portfolio
// Author: Wesley Scott
// </summary>
namespace WS.Examples.Tests.Selenium.CarSearchTests
{
    [TestFixture]
    [NonParallelizable]
    public partial class CarSearchTests : SeleniumTestFixture
    {
        HomePage homePage;
        CarsForSale1Page carsForSalePage;

        /// <summary>
        /// Initialize the CARFAX search test suite fixture.  Then navigate to the
        /// Web application home page and click the 'Used Cars for Sale' menu item.
        /// </summary>
        [OneTimeSetUp]
        public void Init()
        {
            Initialize();

            homePage = (HomePage)PageFactory.Instance.GetPage(
                WebDriver, Args.AppURL, PageFactory.HOME, Args.PageTimeout, Args.ElementTimeout);
            homePage.Navigate();

            carsForSalePage = homePage.ClickUsedCarsForSaleMenu();
        }

        /// <summary>
        /// Verify that a CARFAX basic used car search returns the appropriate results page.
        /// </summary>
        /// <param name="make">
        /// A car make.
        /// </param>
        /// <param name="model">
        /// A model of the specified car make.
        /// </param>
        /// <param name="zipCode">
        /// A search zip code.
        /// </param>
        [Test, Order(1)]
        [TestCaseSource("GetCarModelTestData")]
        public void BasicCarSearchTest(string make, string model, string zipCode)
        {
            try
            {
                CarsForSale2Page step2Page;
                SearchResultsPage searchResultsPage;

                carsForSalePage.Navigate();

                carsForSalePage.Select_Make(make);
                carsForSalePage.Select_Model(model);
                carsForSalePage.Type_ZipCode(zipCode);

                step2Page = (CarsForSale2Page)carsForSalePage.Click_Next_Button();

                searchResultsPage = (SearchResultsPage)step2Page.ClickShowMeButton(make, model);

                Assert.Multiple(() =>
                {
                    string expectedHeaderText = $"{make} {model}";
                    string headerText = searchResultsPage.GetHeaderText();
                    Assert.That(headerText.Contains(expectedHeaderText),
                        $"Search page header doesn't contain text: '{expectedHeaderText}'");

                    string searchMake = searchResultsPage.GetSelectedMake();
                    Assert.That(make.Equals(searchMake),
                        $"Search page make doesn't match; expected: '{make}', actual: '{searchMake}'");

                    string searchModel = searchResultsPage.GetSelectedModel();
                    Assert.That(model.Equals(searchModel),
                        $"Search page model doesn't match; expected: '{model}', actual: '{searchModel}'");

                    string searchZip = searchResultsPage.GetZipCode();
                    Assert.That(zipCode.Equals(searchZip),
                        $"Search page zip code doesn't match; expected: '{zipCode}', actual: '{searchZip}'");
                });

                if (Args.DelayMillis > 0)
                {
                    Thread.Sleep(Args.DelayMillis);
                }
            }
            catch (Exception e)
            {
                Assert.Fail($"Basic car search test failed; reason: '{e.Message}'");
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
