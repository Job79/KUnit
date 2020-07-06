using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace KUnit.Core.Reflection
{
    internal static class ReflectionCore
    {
        /// <summary>
        /// Get test methods from assembly
        /// </summary>
        /// <returns>IEnumerable with test methods</returns>
        internal static IEnumerable<TestMethod> GetTestMethods(this Assembly assembly)
        {
            var classInstances = new Dictionary<Type, object>();
            return (assembly ?? throw new Exception("Could not load methods from assembly: Assembly is null"))
                .GetTypes()
                .SelectMany(x => x.GetMethods())
                .Where(IsValidTestMethod)
                .Select(x => new TestMethod(x, classInstances));
        }
        
        /// <summary>
        /// Determines whether methodInfo contains a valid test method
        /// </summary>
        internal static bool IsValidTestMethod(this MethodInfo m) =>
            m.GetCustomAttributes(typeof(Test), false).Any() && m.GetTestDelegateType() != null;
    }
}