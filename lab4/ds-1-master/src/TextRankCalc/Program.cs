using System;
using StackExchange.Redis;
using System.Text.RegularExpressions;
using consts;

namespace TextRankCalc
{
    class Program
    {
        private static ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(Consts.REDIS_HOST);
        static void Main(string[] args)
        {
            var subsc = redis.GetSubscriber();
            subsc.Subscribe("events", (channel, id) => {
                var db = redis.GetDatabase();
                String message = db.StringGet((string)id);
                Console.WriteLine("Text: " + (string)message + "  id: "+ id);
                message += " "  + id;
                db.ListLeftPush( Consts.QUEUE_NAME_COUNTER, message, flags: CommandFlags.FireAndForget );
                db.Multiplexer.GetSubscriber().Publish( Consts.QUEUE_CHANNEL_COUNTER, "" );
            });
            Console.ReadKey();
        }
    }
}
