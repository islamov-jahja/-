using System;
using StackExchange.Redis;
using consts;

namespace TextListener
{
    class Program
    {
        private static ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(Consts.REDIS_HOST);
        static void Main(string[] args)
        {
            var subsc = redis.GetSubscriber();
            subsc.Subscribe("events", (channel, id) => {
                var db = redis.GetDatabase();
                var message = db.StringGet((string)id);
                Console.WriteLine("id: " + (string)id + "       message: " + (string)message);
            });
            Console.ReadKey();
        }
    }
}
