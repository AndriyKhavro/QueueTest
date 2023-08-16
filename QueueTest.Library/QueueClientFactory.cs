using StackExchange.Redis;

namespace QueueTest.Library;

public class QueueClientFactory
{
    public IQueueClient Create(string code, string port)
    {
        return code == "Beanstalk"
            ? new BeanstalkClient("localhost", ushort.Parse(port))
            : new RedisClient(new ConfigurationOptions
            {
                EndPoints = { $"localhost:{port}" }
            }.ToString());
    }
}