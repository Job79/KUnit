using System;
using System.Reflection;
using System.Threading.Tasks;

namespace KUnit.Core.Reflection
{
    internal static class Delegates
    {
        /* Different delegate types for test methods */
        internal delegate void TestMethodDelegate();
        internal delegate Task AsyncTestMethodDelegate();
        
        /// <summary>
        /// Get delegate type for specific method
        /// </summary>
        internal static Type GetTestDelegateType(this MethodInfo m)
        {
            if (m.GetParameters().Length != 0) return null;
            else if (m.ReturnType == typeof(void)) return typeof(TestMethodDelegate);
            else if (m.ReturnType == typeof(Task)) return typeof(AsyncTestMethodDelegate);
            else return null;
        }
    }
}