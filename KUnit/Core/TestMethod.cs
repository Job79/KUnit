using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using KUnit.Core.Exceptions;
using KUnit.Core.Reflection;

namespace KUnit.Core
{
    public class TestMethod
    {
        public Test Attribute { get; }
        public Delegate Delegate { get; }
        public MethodInfo MethodInfo { get; }
        public Stopwatch Timer { get; private set; }
        
        /// <summary>
        /// Result of executed test, can only be set once
        /// returns PASS when not set
        /// </summary>
        public TestResult FinalResult
        {
            get => _result ?? new TestResult(Result.Pass, test: this);
            set => _result ??= value;
        }
        private TestResult _result;

        /// <param name="methodInfo">valid test method</param>
        /// <param name="classInstances">list with already created class instances</param>
        public TestMethod(MethodInfo methodInfo, Dictionary<Type, object> classInstances = null)
        {
            object classInstance = null;
            if (classInstances != null && !methodInfo.IsStatic)
            {
                var classType = methodInfo.DeclaringType;
                if (!classInstances.TryGetValue(
                    classType ?? throw new InvalidOperationException("Declaring class is null"), out classInstance))
                {
                    classInstance = Activator.CreateInstance(classType);
                    classInstances.Add(classType, classInstance);
                }
            }

            var methodType = methodInfo.GetTestDelegateType();
            Delegate = Delegate.CreateDelegate(methodType, classInstance, methodInfo);
            Attribute = methodInfo.GetCustomAttributes().OfType<Test>().First();
            MethodInfo = methodInfo;
        }
        
        /// <summary>
        /// Function that is executed when test is started 
        /// </summary>
        private void Start() => Timer = Stopwatch.StartNew();
        
        /// <summary>
        /// Function that is executed when test is finished
        /// </summary>
        private void Stop() => Timer?.Stop();
        
        /// <summary>
        /// Execute TestMethod
        /// </summary>
        public async Task<TestResult> Execute()
        {
            try
            {
                var type = Delegate.GetType();
                Start();

                if (type == typeof(Delegates.TestMethodDelegate))
                    await Task.Run(() => ((Delegates.TestMethodDelegate) Delegate)());
                else if (type == typeof(Delegates.AsyncTestMethodDelegate))
                    await ((Delegates.AsyncTestMethodDelegate) Delegate)();
            }
            catch (TestCanceledException ex)
            {
                FinalResult = ex.Result;
                FinalResult.Test ??= this;
            }
            catch (Exception ex)
            {
                FinalResult = new TestResult(Result.Error, $"Test trowed error: {ex.Message}\n{ex.StackTrace}", this);
            }

            Stop();
            return FinalResult;
        }
    }
}