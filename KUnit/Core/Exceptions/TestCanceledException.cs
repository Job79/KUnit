using System;

namespace KUnit.Core.Exceptions
{
    /// <summary>
    /// Exception that is thrown when a test is canceled
    /// </summary>
    public class TestCanceledException : OperationCanceledException
    {
        /// <summary>
        /// Result of canceled test
        /// </summary>
        public TestResult Result { get; }
        
        public TestCanceledException(TestResult result) => Result = result;
    }
}