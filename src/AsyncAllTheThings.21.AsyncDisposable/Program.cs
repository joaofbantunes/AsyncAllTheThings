using System;
using System.IO;
using System.Threading.Tasks;

await using var writer = new FileWriter("test-file.txt");
await writer.WriteLineAsync("just some sample text");


class FileWriter : IAsyncDisposable
{
    private readonly StreamWriter _writer;

    public FileWriter(string filePath) => _writer = new StreamWriter(File.OpenWrite(filePath));

    public Task WriteLineAsync(string text) => _writer.WriteLineAsync(text);

    public async ValueTask DisposeAsync()
    {
        await _writer.DisposeAsync();
    }
}