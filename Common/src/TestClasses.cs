using System;

/// <summary>
/// Common test classes.
/// 
/// Subject: Software Developer Portfolio
/// Author: Wesley Scott
/// </summary>
namespace WS.Examples.Tests.Common
{
    /// <summary>
    /// Test exception base class.
    /// </summary>
    [Serializable]
    public abstract class TestException : Exception
    {
        public TestException(string message) : base(message)
        {
        }
    }
}