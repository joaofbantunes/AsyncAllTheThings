using System;
using System.Threading.Tasks;

namespace AsyncAllTheThings._14.DuckTyping.Ducks
{
    public class RandomAfterDelayAwaitableSample
    {
        private static readonly Random RandomGenerator = new Random();
        private readonly Task<int> _task;

        public RandomAfterDelayAwaitableSample(TimeSpan delay)
        {
            _task = GetRandomAfterDelay(delay);
        }

        public SampleAwaiter<int> GetAwaiter() => new SampleAwaiter<int>(_task);

        private static async Task<int> GetRandomAfterDelay(TimeSpan delay)
        {
            await Task.Delay(delay);
            return RandomGenerator.Next();
        }
    }
}
