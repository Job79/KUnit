using System;

namespace KUnit
{
    /// <summary>
    /// Test attribute that marks methods as tests
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class Test : Attribute
    {
    }
}