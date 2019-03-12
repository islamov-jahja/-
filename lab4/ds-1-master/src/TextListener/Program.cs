using System;
using StackExchange.Redis;

namespace TextListener
{
    class Program
    {
        private static ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("127.0.0.1:6379");
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
