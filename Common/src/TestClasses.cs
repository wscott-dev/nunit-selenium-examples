using System;

//
// Common test classes.
// 
// Subject: Software Developer Portfolio
// Author: Wesley Scott
//
namespace WS.Examples.Tests.Common
{
    /// <summary>
    /// Test exception base class.
    /// </summary>
    [Serializable]
    public abstract class TestException : Exception
    {
        /// <summary>
        /// Create a test exception.
        /// </summary>
        /// <param name="message">
        /// An exception message.
        /// </param>
        public TestException(string message) : base(message)
        {
        }
    }
}