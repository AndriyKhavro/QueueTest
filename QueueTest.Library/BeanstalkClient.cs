using Beanstalk.Core;

namespace QueueTest.Library;

public class BeanstalkClient : IQueueClient
{
    private readonly BeanstalkConnection _connection;
    private const string TubeName = "test-tube";

    public BeanstalkClient(string host, ushort port)
    {
        _connection = new BeanstalkConnection(host, port);
    }

    public Task SendMessageToQueue(string message)
    {
        return _connection.Put(message);
    }

    public async Task<string> ReadFromQueue()
    {
        var job = await _connection.Reserve(TimeSpan.FromMinutes(1));
        await _connection.Delete(job.Id);
        return job.Data;
    }

    public async Task ListenToQueue(Action<string> messageHandler)
    {
        await _connection.Watch(TubeName);

#pragma warning disable CS4014
        Task.Run(async () =>
#pragma warning restore CS4014
        {
            while (true)
            {
                var job = await _connection.Reserve(TimeSpan.FromMinutes(1));
                messageHandler(job.Data);
                await _connection.Delete(job.Id);
            }
        });
    }

    public void Dispose()
    {
        _connection.Dispose();;
    }
}