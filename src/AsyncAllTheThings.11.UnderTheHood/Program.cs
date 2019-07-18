using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Threading.Tasks;

namespace AsyncAllTheThings._11.UnderTheHood
{
    public class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<AsyncBenchmark>();
        }

        [RankColumn, MemoryDiagnoser]
        public class AsyncBenchmark
        {
            [Benchmark]
            public async Task Await() => await DoSomethingAsync();

            [Benchmark]
            public Task NoAwait1() => DoSomethingAsync();

            [Benchmark]
            public Task NoAwait2() => DoSomethingWithNoAwaitAsync();

            private async Task DoSomethingAsync() => await Task.Delay(TimeSpan.FromMilliseconds(10));

            private Task DoSomethingWithNoAwaitAsync() => Task.Delay(TimeSpan.FromMilliseconds(10));

        }
    }
}
