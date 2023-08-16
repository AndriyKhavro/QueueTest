using System.Diagnostics;
using QueueTest.Library;

var clientCode = args[0];
var port = args.ElementAtOrDefault(1) ?? "6379";

Console.WriteLine($"Connecting to {clientCode} on port {port}");

using var client = new QueueClientFactory().Create(clientCode, port);

Console.WriteLine("Started sending messages...");

int count = 0;

var stopwatch = new Stopwatch();
stopwatch.Start();

while (stopwatch.Elapsed < TimeSpan.FromMinutes(1))
{
    await client.SendMessageToQueue("Hello, World!");
    count++;
}

Console.WriteLine($"Performed {count} operations");
await File.WriteAllTextAsync($"{clientCode}_{port}_WRITE.txt", count.ToString());