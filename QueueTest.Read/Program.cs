using System.Diagnostics;
using QueueTest.Library;

var clientCode = args[0];
var port = args.ElementAtOrDefault(1) ?? "6379";

Console.WriteLine($"Connecting to {clientCode} on port {port}");

using var client = new QueueClientFactory().Create(clientCode, port);

int count = 0;

var stopwatch = new Stopwatch();
stopwatch.Start();

// it doesn't work
await client.ListenToQueue(message =>
{
    Console.WriteLine($"Received message: {message}");
    count++;
});

while (stopwatch.Elapsed < TimeSpan.FromMinutes(1))
{
    await Task.Delay(1000);
}

Console.WriteLine($"Performed {count} operations");