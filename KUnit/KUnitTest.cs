using System;
using System.Collections.Generic;
using System.Linq;
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
        /// fired when a test is started 
        /// </summary>
        public static event OnTestStartDelegate OnTestStart;

        public delegate void OnTestStartDelegate(TestMethod test);

        /// <summary>
        /// fired when tests are detected 
        /// </summary>
        public static event OnTestsLoadDelegate OnTestsLoad;

        public delegate void OnTestsLoadDelegate(List<TestMethod> tests);

        /// <summary>
        /// Load all tests from assembly
        /// </summary>
        /// <param name="assembly">assembly with test methods</param>
        /// <returns>list with detected tests</returns>
        public static List<TestMethod> LoadTests(Assembly assembly = null)
        {
            var tests = (assembly ?? Assembly.GetEntryAssembly()).GetTestMethods().ToList();
            OnTestsLoad?.Invoke(tests);
            return tests;
        }

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
            var testResults = new List<Task<TestResult>>();
            using var maxTasks = new SemaphoreSlim(tasksCount == -1 ? Environment.ProcessorCount : tasksCount);
            foreach (var test in LoadTests(assembly))
            {
                await maxTasks.WaitAsync();
                OnTestStart?.Invoke(test);

                var task = test.Execute();
                testResults.Add(task);
                task.ContinueWith(ta =>
                {
                    OnTestComplete?.Invoke(ta.Result);
                    return maxTasks.Release();
                });
            }

            return await Task.WhenAll(testResults);
        }

        /// <summary>
        /// Run all tests and print output
        /// </summary>
        /// <param name="assembly">assembly with test methods</param>
        /// <param name="tasksCount">amount of parallel tests (use processorCount when -1)</param>
        public static void RunTestsPrintOutput(Assembly assembly = null, int tasksCount = -1) =>
            Task.Run(async () => await RunTestsPrintOutputAsync(assembly, tasksCount)).GetAwaiter().GetResult();

        /// <summary>
        /// Run all tests and print output
        /// </summary>
        /// <param name="assembly">assembly with test methods</param>
        /// <param name="tasksCount">amount of parallel tests (use processorCount when -1)</param>
        public static async Task RunTestsPrintOutputAsync(Assembly assembly = null, int tasksCount = -1)
        {
            using var writeLock = new SemaphoreSlim(1);
            using var completed = new SemaphoreSlim(0);
            int counter = 0;

            void OnTestLogger(List<TestMethod> tests)
            {
                counter = tests.Count;
                Console.WriteLine($"Running {tests.Count} {{0}}...", tests.Count > 1 ? "tests" : "test"); 
            }
            void OnTestStartLogger(TestMethod test)
            {
                writeLock.Wait();
                Console.WriteLine($"Starting {test.MethodInfo.Name}...");
                writeLock.Release();  
            }
            void OnTestCompleteLogger(TestResult testResult)
            {
                writeLock.Wait();
                Console.ForegroundColor = testResult.Result == Result.Pass ? ConsoleColor.Green : ConsoleColor.Red;
                
                Console.WriteLine($"Completed {testResult.Test.MethodInfo.Name}; {testResult.Result}; {testResult.Test.Timer.ElapsedMilliseconds}ms");
                if(!string.IsNullOrWhiteSpace(testResult.Message)) Console.WriteLine($"\t{testResult.Message.Replace("\n","\n\t")}");
                
                Console.ResetColor();
                if(Interlocked.Decrement(ref counter) <= 0) completed.Release();
                writeLock.Release();
            }

            OnTestsLoad += OnTestLogger;
            OnTestStart += OnTestStartLogger;
            OnTestComplete += OnTestCompleteLogger;

            Console.WriteLine("Detecting all tests...");
            var results = (await RunTestsAsync(assembly, tasksCount)).ToList();
            await completed.WaitAsync();

            int passed = results.Count(x=>x.Result == Result.Pass),
                failed = results.Count(x=>x.Result == Result.Fail),
                errors = results.Count(x=>x.Result == Result.Error);
            Console.WriteLine($"test results: {passed} passed; {failed} failed; {errors} errors");
            
            OnTestsLoad -= OnTestLogger;
            OnTestStart -= OnTestStartLogger;
            OnTestComplete -= OnTestCompleteLogger;
        }
    }
}