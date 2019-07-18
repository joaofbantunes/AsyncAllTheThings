using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncAllTheThings._14.DuckTyping.Ducks
{
    public static class AwaitableExtensions
    {
        public static SampleAwaiter<T[]> GetAwaiter<T>(this IEnumerable<Task<T>> tasks) => new SampleAwaiter<T[]>(Task.WhenAll(tasks));
    }
}
