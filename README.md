<p align="center">
  <strong>KUnit</strong>
  <br/>
  <img src="https://img.shields.io/badge/License-MIT-green.svg">
  <img src="https://img.shields.io/badge/version-1.0.0-green.svg">
  <img src="https://img.shields.io/badge/build-passing-green.svg">
  <br/>
  Very small and fast unit testing framework.
  <br/><br/><br/>
</p>

# Features
- Very small and lightweight
- Very fast (~28.000 tests/second)*
- Stand-alone

\* See KUnit.Performance

# Example

```cs
    static Task Main() => KUnitTest.RunTestsPrintOutputAsync();

    public Program()
    {
        Console.WriteLine("Setup...");
    }

    [Test]
    public void Pass()
    {
        Assert.Pass();
    }       
```
```
Detecting all tests...
Setup...
Running 1 test...
Starting Pass...
Completed Pass; Pass; 10ms
test results: 1 passed; 0 failed; 0 errors
```
