namespace QueueTest.Library;

public interface IQueueClient : IDisposable
{
    Task SendMessageToQueue(string message);
    Task ListenToQueue(Action<string> messageHandler);
}