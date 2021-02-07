using System;

//
// Common test utilities.
// 
// Subject: Software Developer Portfolio
// Author: Wesley Scott
//
namespace WS.Examples.Tests.Common
{
    /// <summary>
    /// Class containing common test utility methods.
    /// </summary>
    public static class TestUtils
    {
        /// <summary>
        /// Get the value for the specified environment variable name.
        /// </summary>
        /// <param name="varName">
        /// An environment variable name.
        /// </param>
        /// <returns>
        /// A environment variable value.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when the specified environment variable has not been set.
        /// </exception>
        public static string GetEnvValue(string varName)
        {
            string varValue = Environment.GetEnvironmentVariable(varName);

            if (string.IsNullOrEmpty(varValue))
            {
                throw new ArgumentNullException($"{varName} environment variable");
            }

            return (varValue);
        }

        /// <summary>
        /// Append path segments to a base URL.
        /// </summary>
        /// <param name="baseUrl">
        /// A base URL string.
        /// </param>
        /// <param name="segments">
        /// List of path segments to append.
        /// </param>
        /// <returns>
        /// The appended URL string.
        /// </returns>
        public static string AppendUrl(string baseUrl, params string[] segments)
        {
            string newUrl = baseUrl;

            foreach (string segment in segments)
            {
                newUrl = newUrl.EndsWith("/") ? newUrl + segment : newUrl + '/' + segment;
            }

            return newUrl;
        }
    }
}