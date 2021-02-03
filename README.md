# NUnit Selenium Examples

__Subject:__ Software Developer Portfolio  
__Author:__ Wesley Scott  

## CARFAX Selenium Example Tests

Self-contained NUnit Selenium examples utilizing the CARFAX <https://www.carfax.com> website as the application under test.

Note that default test execution is slightly delayed for demonstration purposes.  The delay is configured in the `cfg\DefaultTestConfig.json` file for each example test suite.

### CARFAXSeleniumExample1

This example is a data-driven test suite that verifies the presence of several Web elements (toolbar menus and hyperlinks) on CARFAX application Web pages.  Test data is loaded from the `NUnitSeleniumExample1\cfg\carfax\ToolbarMenus.json` and `NUnitSeleniumExample1\cfg\carfax\HyperlinkElements.json` files.  This type of test suite is useful for smoke/preliminary testing that would occur before more detailed testing is performed.

### CARFAXSeleniumExample2

This example is a data-driven test suite that verifies basic used car search functionality using the Page Object test design pattern (a nice explanation of the pattern may be found here: <https://martinfowler.com/bliki/PageObject.html>).  Test data is loaded from the `NUnitSeleniumExample2\cfg\carfax\CarModels.json` and `NUnitSeleniumExample2\cfg\carfax\ZipCodes.json` files.  A car search test is performed for each make, model, and zip code combination.  A search test passes if the used car search results page contains the expected make and model in the page header text, and the results page make, model and zip code search parameters match the expected values.

### Software Required

Install the software listed below to build and run the example solution.

1. __Microsoft Visual Studio 2019 Community__ (include the *.NET Core cross-platform development* Workload); it may be downloaded from: <https://visualstudio.microsoft.com/downloads>
2. __Google Chrome__, Chrome may be downloaded from: <https://www.google.com/chrome>
3. __Chrome Web driver__, the Chrome Web driver may be downloaded from: <https://chromedriver.chromium.org/downloads>
(__Select a Chrome Web driver version that matches the installed Google Chrome version.__)

### Setup

A system running Microsoft Windows 10 is recommended to run these examples.

#### Installation

1. Download and unzip the `nunit-selenium-examples-main.zip` file to a local folder.

#### Environment Variables

Create the environment variables listed below.

1. __TEST_CFG_FILENAME__: Set the value to `DefaultTestConfig.json`.
(Note that the default test configuration lets browser test activity be seen as it is performed.)
2. __CHROME_WEBDRIVER_DIR__: Set the value to the full pathname of the folder containing the Chrome Web driver executable (`D:\Software\WebDrivers` for example).

### Build

1. Open the `NUnitSeleniumExamples.sln` solution file using Microsoft Visual Studio Community.
2. Set the *Solution Configurations* dropdown to *Debug* if necessary.
3. Set the *Solution Platforms* dropdown to *Any CPU* if necessary.
4. Set the *Startup Projects* dropdown to *CARFAXSeleniumExample1* if necessary.
5. Select the *Build->Build Solution* option from the menu bar.

### Execution

1. Close any open Chrome browser instances.
2. Select the *Test->Run All Tests* option from the menu bar.  The Chrome browser with be launched and the tests will be executed.
(Note that the Chrome browser will be launched once for the *CARFAXSeleniumExample1* test suite, and once for the *CARFAXSeleniumExample2* test suite.)

### Results

1. Select the *View->Test Explorer* option from the menu bar to view test results.
2. Expand *CARFAXSeleniumExample1* and *CARFAXSeleniumExample2* using the right click menu *Expand* option to see test result details.
