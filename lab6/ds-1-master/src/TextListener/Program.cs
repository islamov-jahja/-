using System;
using StackExchange.Redis;
using consts;

namespace TextListener
{
    class Program
    {
        static void Main(string[] args)
        {
            var subsc = DB.redis.GetSubscriber();
            subsc.Subscribe("events", (channel, id) => {
                var db = DB.redis.GetDatabase();
                var text = db.StringGet($"text {id}");
                Console.WriteLine($"Text: {text}");
            });
            Console.ReadKey();
        }
    }
}
