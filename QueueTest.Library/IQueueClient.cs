namespace QueueTest.Library;

public interface IQueueClient : IDisposable
{
    Task SendMessageToQueue(string message);
    Task<string> ReadFromQueue();
}