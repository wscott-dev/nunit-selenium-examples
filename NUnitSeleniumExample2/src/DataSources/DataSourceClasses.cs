using System.Collections.Generic;

/// <summary>
/// Test data source classes.
/// 
/// Subject: Software Developer Portfolio
/// Author: Wesley Scott
/// </summary>
namespace WS.Examples.Tests.Selenium.DataSourceClasses
{
    /// <summary>
    /// Class representing car data.
    /// </summary>
    public class CarData
    {
        private List<CarModel> models;

        /// <summary>
        /// List of input field names or values.
        /// </summary>
        public List<CarModel> Models { get => models; set => models = value; }
    }

    /// <summary>
    /// Class representing car model data.
    /// </summary>
    public class CarModel
    {
        private string name;

        /// <summary>
        /// </summary>
        public string Name { get => name; set => name = value; }
    }

    /// <summary>
    /// Class representing zip code data.
    /// </summary>
    public class ZipCodeData
    {
        private List<ZipCode> zipCodes;

        /// <summary>
        /// List of input field names or values.
        /// </summary>
        public List<ZipCode> ZipCodes { get => zipCodes; set => zipCodes = value; }
    }

    /// <summary>
    /// Class representing a zip code.
    /// </summary>
    public class ZipCode
    {
        private string city;
        private string state;

        public string City { get => city; set => city = value; }
        public string State { get => state; set => state = value; }
    }
}