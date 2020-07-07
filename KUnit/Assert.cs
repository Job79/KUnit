using System;
using System.Collections.Generic;
using KUnit.Core;
using KUnit.Core.Exceptions;

namespace KUnit
{
    public static class Assert
    {
        /// <summary>
        /// Throws an TestCanceledException and fails the test
        /// </summary>
        public static void Fail(string message = "")
            => throw new TestCanceledException(new TestResult(Result.Fail, message));

        /// <summary>
        /// Throws an TestCanceledException and passes the test
        /// </summary>
        public static void Pass(string message = "")
            => throw new TestCanceledException(new TestResult(Result.Pass, message));

        /// <summary>
        /// Tests whether a specific condition is true and fails the test when condition is false
        /// </summary>
        public static void IsTrue(bool boolean, string message = "")
        {
            if (!boolean) Fail(message);
        }

        /// <summary>
        /// Tests whether a specific function returns true and fails the test when function returns false
        /// </summary>
        public static void IsTrue(Func<bool> result, string message = "") => IsTrue(result(), message);

        /// <summary>
        /// Tests whether a specific condition is false and fails the test when condition is true 
        /// </summary>
        public static void IsFalse(bool boolean, string message = "") => IsTrue(!boolean, message);
        
        /// <summary>
        /// Tests whether a specific function returns false and fails the test when function returns true 
        /// </summary>
        public static void IsFalse(Func<bool> result, string message = "") => IsFalse(result(), message);

        /// <summary>
        /// Tests whether the specified parameters are equal and fails the test when they are different
        /// </summary>
        public static void AreEqual<T>(T expected, T actual, string message = "") => IsTrue(() => EqualityComparer<T>.Default.Equals(expected, actual), message);

        /// <summary>
        /// Tests whether the specified parameters are different and fails the test when they are equal 
        /// </summary>
        public static void AreNotEqual<T>(T expected, T actual, string message = "") =>
            IsFalse(() => EqualityComparer<T>.Default.Equals(expected, actual), message);

        /// <summary>
        /// Tests whether the specified parameter is null and fails the test when parameter is not-null 
        /// </summary>
        public static void IsNull<T>(T item, string message = "") => IsTrue(() => item == null, message);
        
        /// <summary>
        /// Tests whether the specified parameter is not-null and fails the test when parameter is null
        /// </summary>
        public static void IsNotNull<T>(T item, string message = "") => IsFalse(() => item == null, message);
    }
}