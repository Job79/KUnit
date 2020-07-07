using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace KUnit.Performance
{
    class Program
    {
        /*
         * Determines average performance of KUnit,
         * all the test functions are empty. Real usage will (probably) be slower.
         */
        const int TestCount = 90;
        const int TaskCount = -1; // -1 = Environment.ProcessorCount
        const int Rounds = 1500;

        static async Task Main()
        {
            var stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < Rounds; i++) await KUnitTest.RunTestsAsync();

            const int tests = TestCount * Rounds;
            Console.WriteLine($"Executed {tests} in {stopwatch.ElapsedMilliseconds} milliseconds");
            Console.WriteLine($"Amount of test detection trips: {Rounds}");
            Console.WriteLine($"Amount of tests per trip: {TestCount}");
            Console.WriteLine($"Amount of parallel tasks: {TaskCount}");
            Console.WriteLine($"Tests/Second: {tests/stopwatch.Elapsed.TotalSeconds}");
        }

        /*
         * 90 empty test functions
         */
        [Test]
        public void Test1() => Assert.Pass();

        [Test]
        public void Test2() => Assert.Fail();

        [Test]
        public void Test3()
        {
        }

        [Test]
        public void Test4() => Assert.Pass();

        [Test]
        public void Test5() => Assert.Fail();

        [Test]
        public void Test6()
        {
        }

        [Test]
        public void Test7() => Assert.Pass();

        [Test]
        public void Test8() => Assert.Fail();

        [Test]
        public void Test9()
        {
        }

        [Test]
        public void Test10() => Assert.Pass();

        [Test]
        public void Test11() => Assert.Fail();

        [Test]
        public void Test12()
        {
        }

        [Test]
        public void Test13() => Assert.Pass();

        [Test]
        public void Test14() => Assert.Fail();

        [Test]
        public void Test15()
        {
        }
        
        [Test]
        public void Test16() => Assert.Pass();

        [Test]
        public void Test17() => Assert.Fail();

        [Test]
        public void Test18()
        {
        }

        [Test]
        public void Test19() => Assert.Pass();

        [Test]
        public void Test20() => Assert.Fail();

        [Test]
        public void Test21()
        {
        }

        [Test]
        public void Test22() => Assert.Pass();

        [Test]
        public void Test23() => Assert.Fail();

        [Test]
        public void Test24()
        {
        }

        [Test]
        public void Test25() => Assert.Pass();

        [Test]
        public void Test26() => Assert.Fail();

        [Test]
        public void Test27()
        {
        }

        [Test]
        public void Test28() => Assert.Pass();

        [Test]
        public void Test29() => Assert.Fail();

        [Test]
        public void Test30()
        {
        }
        
        [Test]
        public void Test31() => Assert.Pass();

        [Test]
        public void Test32() => Assert.Fail();

        [Test]
        public void Test33()
        {
        }

        [Test]
        public void Test34() => Assert.Pass();

        [Test]
        public void Test35() => Assert.Fail();

        [Test]
        public void Test36()
        {
        }

        [Test]
        public void Test37() => Assert.Pass();

        [Test]
        public void Test38() => Assert.Fail();

        [Test]
        public void Test39()
        {
        }

        [Test]
        public void Test40() => Assert.Pass();

        [Test]
        public void Test41() => Assert.Fail();

        [Test]
        public void Test42()
        {
        }

        [Test]
        public void Test43() => Assert.Pass();

        [Test]
        public void Test44() => Assert.Fail();

        [Test]
        public void Test45()
        {
        }
        
          [Test]
        public void Test46() => Assert.Pass();

        [Test]
        public void Test47() => Assert.Fail();

        [Test]
        public void Test48()
        {
        }

        [Test]
        public void Test49() => Assert.Pass();

        [Test]
        public void Test50() => Assert.Fail();

        [Test]
        public void Test51()
        {
        }

        [Test]
        public void Test52() => Assert.Pass();

        [Test]
        public void Test53() => Assert.Fail();

        [Test]
        public void Test54()
        {
        }

        [Test]
        public void Test55() => Assert.Pass();

        [Test]
        public void Test56() => Assert.Fail();

        [Test]
        public void Test57()
        {
        }

        [Test]
        public void Test58() => Assert.Pass();

        [Test]
        public void Test59() => Assert.Fail();

        [Test]
        public void Test60()
        {
        }
        
        [Test]
        public void Test61() => Assert.Pass();

        [Test]
        public void Test62() => Assert.Fail();

        [Test]
        public void Test63()
        {
        }

        [Test]
        public void Test64() => Assert.Pass();

        [Test]
        public void Test65() => Assert.Fail();

        [Test]
        public void Test66()
        {
        }

        [Test]
        public void Test67() => Assert.Pass();

        [Test]
        public void Test68() => Assert.Fail();

        [Test]
        public void Test69()
        {
        }

        [Test]
        public void Test70() => Assert.Pass();

        [Test]
        public void Test71() => Assert.Fail();

        [Test]
        public void Test72()
        {
        }

        [Test]
        public void Test73() => Assert.Pass();

        [Test]
        public void Test74() => Assert.Fail();

        [Test]
        public void Test75()
        {
        }
        
        [Test]
        public void Test76() => Assert.Pass();

        [Test]
        public void Test77() => Assert.Fail();

        [Test]
        public void Test78()
        {
        }

        [Test]
        public void Test79() => Assert.Pass();

        [Test]
        public void Test80() => Assert.Fail();

        [Test]
        public void Test81()
        {
        }

        [Test]
        public void Test82() => Assert.Pass();

        [Test]
        public void Test83() => Assert.Fail();

        [Test]
        public void Test84()
        {
        }

        [Test]
        public void Test85() => Assert.Pass();

        [Test]
        public void Test86() => Assert.Fail();

        [Test]
        public void Test87()
        {
        }

        [Test]
        public void Test88() => Assert.Pass();

        [Test]
        public void Test89() => Assert.Fail();

        [Test]
        public void Test90()
        {
        }
    }
}