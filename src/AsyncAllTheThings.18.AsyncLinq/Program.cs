using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

// https://github.com/dotnet/reactive

var doer = new StuffDoer();

// typical operations, like sum and count
// these require iterating the whole enumerable before producing results
Console.WriteLine($"Sum: {await doer.DoStuffAsync(5).SumAsync()}");
Console.WriteLine($"Count: {await doer.DoStuffAsync(5).CountAsync()}");

// zip works like the typical IEnumerable one
// and doesn't require the whole thing to be iterated before producing results
await doer
    .DoStuffAsync(5)
    .Zip(doer.DoStuffReversedAsync(5))
    .ForEachAsync((tuple, i) => Console.WriteLine($"Zip - index: {i}, values: {tuple} "));


// where vs skip and take while

await doer.DoStuffAsync(10).Where(v => v >= 2 && v < 6).ForEachAsync(v => Console.WriteLine($"Skip and Take while: {v}"));

await doer.DoStuffAsync(10).SkipWhile(v => v < 2).TakeWhile(v => v < 6).ForEachAsync(v => Console.WriteLine($"Skip and Take while: {v}"));


class StuffDoer
{

    public async IAsyncEnumerable<int> DoStuffAsync(int amountOfStuff, [EnumeratorCancellation] CancellationToken ct = default)
    {
        for (var i = 0; i < amountOfStuff; ++i)
        {
            Console.WriteLine($"Doing {i}");
            await Task.Delay(TimeSpan.FromMilliseconds(250), ct);
            yield return i;
        }
    }

    public async IAsyncEnumerable<int> DoStuffReversedAsync(int amountOfStuff, [EnumeratorCancellation] CancellationToken ct = default)
    {
        for (var i = amountOfStuff - 1; i >= 0; --i)
        {
            Console.WriteLine($"Doing reversed {i}");
            await Task.Delay(TimeSpan.FromMilliseconds(250), ct);
            yield return i;
        }
    }
}
