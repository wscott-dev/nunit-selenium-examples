using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using WS.Examples.Tests.Selenium.Common;
using WS.Examples.Tests.Selenium.DataSourceClasses;

//
// Car search test data sources.
// 
// Subject: Software Developer Portfolio
// Author: Wesley Scott
//
namespace WS.Examples.Tests.Selenium.CarSearchTests
{
    /// <summary>
    /// CARFAX car search test data providers.
    /// </summary>
    public partial class CarSearchTests
    {
        /// <summary>
        /// Get car model test data from external JSON files.
        /// </summary>
        public static IEnumerable<object[]> GetCarModelTestData()
        {
            IDictionary<string, CarData> carData;
            IDictionary<string, ZipCode> zipCodes;
            IList<object[]> outputData = new List<object[]>();

            string jsonFilePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                "cfg", SeleniumTestUtils.GetConfigDir(), "CarModels.json"));

            if (!File.Exists(jsonFilePath))
            {
                throw new FileNotFoundException($"Could not find file '{jsonFilePath}'.");
            }

            carData = JsonConvert.DeserializeObject<Dictionary<string, CarData>>(
                File.ReadAllText(jsonFilePath));

            jsonFilePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                "cfg", SeleniumTestUtils.GetConfigDir(), "ZipCodes.json"));

            if (!File.Exists(jsonFilePath))
            {
                throw new FileNotFoundException($"Could not find file '{jsonFilePath}'.");
            }

            zipCodes = JsonConvert.DeserializeObject<Dictionary<string, ZipCode>>(
              File.ReadAllText(jsonFilePath));

            foreach (string zipCode in zipCodes.Keys)
            {
                foreach (string make in carData.Keys)
                {
                    foreach (CarModel model in carData[make].Models)
                    {
                        outputData.Add(new object[] { make, model.Name, zipCode });
                    }
                }
            }

            return (outputData);
        }
    }
}