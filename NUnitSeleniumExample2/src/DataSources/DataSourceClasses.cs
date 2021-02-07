using System.Collections.Generic;

//
// Test data source classes.
// 
// Subject: Software Developer Portfolio
// Author: Wesley Scott
//
namespace WS.Examples.Tests.Selenium.DataSourceClasses
{
    /// <summary>
    /// Class representing car data.
    /// </summary>
    public class CarData
    {
        private List<CarModel> models;

        /// <summary>
        /// Get/set the list of car models.
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
        /// Get/set the car model name.
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
        /// Get/set the list of zip codes.
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

        /// <summary>
        /// Get/set a city name.
        /// </summary>
        public string City { get => city; set => city = value; }

        /// <summary>
        /// Get/set a state name.
        /// </summary>
        public string State { get => state; set => state = value; }
    }
}