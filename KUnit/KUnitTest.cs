using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using KUnit.Core;
using KUnit.Core.Reflection;

namespace KUnit
{
    public class KUnitTest
    {
        /// <summary>
        /// fired when a test is completed
        /// </summary>
        public static event OnTestCompleteDelegate OnTestComplete;
        public delegate void OnTestCompleteDelegate(TestResult result);

        /// <summary>
        /// Execute all tests and return IEnumerable with test output
        /// </summary>
        /// <param name="assembly">assembly with test methods</param>
        /// <param name="tasksCount">amount of parallel tests (use processorCount when -1)</param>
        /// <returns>IEnumerable with test output</returns>
        public static IEnumerable<TestResult> RunTests(Assembly assembly = null, int tasksCount = -1) =>
            Task.Run(async () => await RunTestsAsync(assembly, tasksCount)).GetAwaiter().GetResult();

        /// <summary>
        /// Execute all tests and return IEnumerable with test output
        /// </summary>
        /// <param name="assembly">assembly with test methods</param>
        /// <param name="tasksCount">amount of parallel tests (use processorCount when -1)</param>
        /// <returns>IEnumerable with test output</returns>
        public static async Task<IEnumerable<TestResult>> RunTestsAsync(Assembly assembly = null,
            int tasksCount = -1)
        {
            var testMethods = (assembly ?? Assembly.GetEntryAssembly()).GetTestMethods();

            var testResults = new List<Task<TestResult>>();
            var maxTasks = new SemaphoreSlim(tasksCount == -1 ? Environment.ProcessorCount : tasksCount);
            foreach (var test in testMethods)
            {
                await maxTasks.WaitAsync();
                var task = test.Execute();
                testResults.Add(task);
                task.ContinueWith(ta =>
                {
                    OnTestComplete?.Invoke(ta.Result);
                    return maxTasks.Release();
                });
            }

            var results = await Task.WhenAll(testResults);
            return results;
        }
    }
}