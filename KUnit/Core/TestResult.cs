namespace KUnit.Core
{
    public class TestResult
    {
        public TestResult(Result result, string message = "", TestMethod test = null)
        {
            Result = result;
            Message = message;
            Test = test;
        }

        public TestMethod Test { get; set; }
        public Result Result { get; }
        public string Message { get; }

        /// <summary>
        /// Convert TestResult to string
        /// </summary>
        public override string ToString()
            => string.IsNullOrWhiteSpace(Message)?
                $"{Test.MethodInfo.Name}\t\t{Result}\t\t{Test?.Timer?.ElapsedMilliseconds}ms":
                $"{Test.MethodInfo.Name}\t\t{Result}: {Message}\t\t{Test?.Timer?.ElapsedMilliseconds}ms";
    }
    
    public enum Result
    {
       Pass, Fail, Error, Timeout
    }
}