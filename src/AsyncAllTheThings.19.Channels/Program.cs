using System;
using System.Threading.Channels;
using System.Threading.Tasks;

var channel = Channel.CreateUnbounded<int>();

var backgroundTask = Task.Run(async () =>
{
    var reader = channel.Reader;

    while (await reader.WaitToReadAsync())
    {
        if (reader.TryRead(out var value))
        {
            Console.WriteLine($"Processing {value}");
            await Task.Delay(TimeSpan.FromSeconds(2));
            Console.WriteLine($"{value} processed");
        }
    }

    // alternative

    //await foreach(var value in channel.Reader.ReadAllAsync())
    //{
    //    Console.WriteLine($"Processing {value}");
    //    await Task.Delay(TimeSpan.FromSeconds(2));
    //    Console.WriteLine($"{value} processed");
    //}
});

Console.WriteLine("Type numbers and enter, type something other than a number to complete");

var writer = channel.Writer;

while( int.TryParse(Console.ReadLine(), out var input))
{
    await writer.WriteAsync(input);
}

writer.Complete();

Console.WriteLine("Waiting for background task to finish...");

await backgroundTask;

Console.WriteLine("End");