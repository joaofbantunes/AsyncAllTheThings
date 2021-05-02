using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

Console.WriteLine("Making requests, press any key to cancel.");

using var cts = new CancellationTokenSource();

var cancellationTask = Task.Run(
    () =>
    {
        Console.ReadKey();
        cts.Cancel();
    },
    cts.Token);

var requesterTask = Task.Run(async () =>
{
    using var requester = new Requester();
    Console.WriteLine("Making requests and getting a traditional IEnumerable");

    foreach (var value in await requester.MakeRequestsEnumerable(5, cts.Token))
    {
        Console.WriteLine($"Handling request {value}");
    }

    Console.WriteLine("Making requests and getting an IAsyncEnumerable");

    await foreach (var value in requester.MakeRequestsAsyncEnumerable(5, cts.Token))
    {
        Console.WriteLine($"Handling request {value}");
    }
});


try
{
    await requesterTask;
}
catch (TaskCanceledException)
{
    Console.WriteLine("Task canceled");
}



class Requester : IDisposable
{
    private static readonly Uri uri = new("https://httpstat.us/200?sleep=1000");
    private readonly HttpClient _httpClient;

    public Requester()
    {
        _httpClient = new HttpClient();
    }

    public async Task<IEnumerable<int>> MakeRequestsEnumerable(int requestCount, CancellationToken ct)
    {
        var result = new int[requestCount];

        for (var i = 0; i < requestCount; ++i)
        {
            Console.WriteLine($"{nameof(MakeRequestsEnumerable)} - Making request {i}");
            await _httpClient.GetAsync(uri, ct);
            Console.WriteLine($"{nameof(MakeRequestsEnumerable)} - Got request {i} response");
            //yield return i;
            result[i] = i;
        }

        return result;
    }

    public async IAsyncEnumerable<int> MakeRequestsAsyncEnumerable(int requestCount, [EnumeratorCancellation] CancellationToken ct)
    {
        for (var i = 0; i < requestCount; ++i)
        {
            Console.WriteLine($"{nameof(MakeRequestsAsyncEnumerable)} - Making request {i}");
            await _httpClient.GetAsync(uri, ct);
            Console.WriteLine($"{nameof(MakeRequestsAsyncEnumerable)} - Got request {i} response");
            yield return i;
        }
    }

    public void Dispose() => _httpClient.Dispose();
}
