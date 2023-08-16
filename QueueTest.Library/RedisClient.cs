using StackExchange.Redis;

namespace QueueTest.Library;

public class RedisClient : IQueueClient
{
    private readonly ConnectionMultiplexer _redis;
    private readonly ISubscriber _subscriber;
    private readonly IDatabase _database;
    private readonly RedisChannel _queueName = RedisChannel.Literal("test-queue");
    private const string KeyName = "test-queue";

    public RedisClient(string redisConnectionString)
    {
        _redis = ConnectionMultiplexer.Connect(redisConnectionString);
        _subscriber = _redis.GetSubscriber();
        _database = _redis.GetDatabase();
    }

    public Task SendMessageToQueue(string message)
    {
        await _database.ListLeftPushAsync(KeyName, message);
        return _subscriber.PublishAsync(_queueName, message);
    }

    public Task ListenToQueue(Action<string> messageHandler)
    {
        return _subscriber.SubscribeAsync(_queueName, (channel, message) =>
        {
            messageHandler(message.ToString());
        });
    }

    public void Dispose()
    {
        _redis.Close();
    }
}
