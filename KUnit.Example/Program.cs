using System;
using System.Threading.Tasks;

namespace KUnit.Example
{
    class Program
    {
        static Task Main(string[] args) => KUnitTest.RunTestsPrintOutputAsync();

        public Program()
        {
            Console.WriteLine("Setup...");
        }
        
        [Test]
        public void Pass()
        {
            Assert.Pass();
        }
        
        [Test]
        public void Fail()
        {
            Assert.Fail();
        }
        
        [Test]
        public void Pass2()
        {
        }
        
        [Test]
        public void Example()
        {
            int i1 = 1, i2 = 2;
            Assert.AreEqual(3,i1+i2);
        }
        
        [Test]
        public void Example2()
        {
            int i1 = 1, i2 = 2;
            Assert.AreEqual(4,i1+i2);
        }
    }
}