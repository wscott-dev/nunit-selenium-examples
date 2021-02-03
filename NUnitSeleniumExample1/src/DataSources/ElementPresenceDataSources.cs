using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using WS.Examples.Tests.Selenium.Common;

/// <summary>
/// Element presence test data sources.
/// 
/// Subject: Software Developer Portfolio
/// Author: Wesley Scott
/// </summary>
namespace WS.Examples.Tests.Selenium.ElementPresenceTests
{
    /// <summary>
    /// Test data retrieval methods for element presence tests.
    /// </summary>
    public partial class ElementPresenceTests
    {
        /// <summary>
        /// Get toolbar menu test data from an external JSON file.
        /// </summary>
        public static IEnumerable<object[]> MenuElementTestData()
        {
            IDictionary<string, string[]> inputData;
            IList<object[]> outputData = new List<object[]>();

            string jsonFilePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                "cfg", SeleniumTestUtils.GetConfigDir(), "ToolbarMenus.json"));

            if (!File.Exists(jsonFilePath))
            {
                throw new FileNotFoundException($"Could not find file '{jsonFilePath}'.");
            }

            inputData = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(File.ReadAllText(jsonFilePath));

            foreach (string keyName in inputData.Keys)
            {
                outputData.Add(new object[] { keyName });
            }

            return (outputData);
        }

        /// <summary>
        /// Get toolbar sub menu test data from an external JSON file.
        /// </summary>
        public static IEnumerable<object[]> SubMenuElementTestData()
        {
            IDictionary<string, string[]> inputData;
            IList<object[]> outputData = new List<object[]>();

            string jsonFilePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                "cfg", SeleniumTestUtils.GetConfigDir(), "ToolbarMenus.json"));

            if (!File.Exists(jsonFilePath))
            {
                throw new FileNotFoundException($"Could not find file '{jsonFilePath}'.");
            }

            inputData = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(File.ReadAllText(jsonFilePath));

            foreach (string keyName in inputData.Keys)
            {
                foreach (string value in inputData[keyName])
                {
                    outputData.Add(new object[] { keyName, value });
                }
            }

            return (outputData);
        }

        /// <summary>
        /// Get hyperlink element test data from an external JSON file.
        /// </summary>
        public static IEnumerable<object[]> HyperlinkElementTestData()
        {
            IDictionary<string, PageElements> inputData;
            IList<object[]> outputData = new List<object[]>();

            string jsonFilePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                "cfg", SeleniumTestUtils.GetConfigDir(), "HyperlinkElements.json"));

            if (!File.Exists(jsonFilePath))
            {
                throw new FileNotFoundException($"Could not find file '{jsonFilePath}'.");
            }

            inputData = JsonConvert.DeserializeObject<Dictionary<string, PageElements>>(
            File.ReadAllText(jsonFilePath));

            foreach (string pageSegment in inputData.Keys)
            {
                foreach (HyperlinkElement element in inputData[pageSegment].Hyperlinks)
                {
                    outputData.Add(new object[] { pageSegment, element.Title, element.Text, element.Position });
                }
            }

            return (outputData);
        }
    }
}