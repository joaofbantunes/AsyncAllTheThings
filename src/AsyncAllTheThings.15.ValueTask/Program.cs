using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Threading.Tasks;

namespace AsyncAllTheThings._15.ValueTask
{
    public class Program
    {
        // Note: this is just to show the key advantage of ValueTask versus a Task
        // The ValueTask however as some other very important drawbacks, so be sure to take a look at:
        // - https://devblogs.microsoft.com/dotnet/understanding-the-whys-whats-and-whens-of-valuetask/
        // - https://github.com/dotnet/corefx/issues/27445
        // The goal of this sample is more to say: "hey! this exists, knowing it may come in handy sometime, but when that time comes, you better google stuff well! 😛"

        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<ValueTaskBenchmark>();
        }

        [RankColumn, MemoryDiagnoser]
        public class ValueTaskBenchmark
        {
            private const string SampleString = nameof(SampleString);

            [Benchmark]
            public async Task AlreadyComputedTask() => await InnerAlreadyComputedTask();

            [Benchmark]
            public async Task AlreadyComputedValueTask() => await InnerAlreadyComputedValueTask();

            [Benchmark]
            public async Task NotComputedTask() => await InnerNotComputedTask();

            [Benchmark]
            public async Task NotComputedValueTask() => await InnerNotComputedValueTask();


            private static Task<string> InnerAlreadyComputedTask() => Task.FromResult(SampleString);

            private static ValueTask<string> InnerAlreadyComputedValueTask() => new ValueTask<string>(SampleString);

            private static async Task<string> InnerNotComputedTask()
            {
                await Task.Delay(5);
                return SampleString;
            }

            public static async ValueTask<string> InnerNotComputedValueTask()
            {
                await Task.Delay(5);
                return SampleString;
            }
        }
    }
}
