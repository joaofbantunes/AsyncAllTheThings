using Nito.AsyncEx;
using System;
using System.Threading;
using System.Threading.Tasks;

// https://github.com/StephenCleary/AsyncEx

using var cts = new CancellationTokenSource();
var autoResetEvent = new AsyncAutoResetEvent();

var backgroundTask = Task.Run(async () =>
{
    while (!cts.Token.IsCancellationRequested)
    {
        Console.WriteLine("Waiting...");
        await autoResetEvent.WaitAsync(cts.Token);
        Console.WriteLine("Running!");
    }
});

while(Console.ReadKey().KeyChar != 'q')
{
    autoResetEvent.Set();
}

cts.Cancel();