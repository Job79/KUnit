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
    }
    
    public enum Result
    {
       Pass, Fail, Error
    }
}