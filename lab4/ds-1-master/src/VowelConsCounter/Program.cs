using System;
using StackExchange.Redis;
using System.Text.RegularExpressions;
using consts;

namespace VowelConsCounter
{
    class Program
    {
        private static ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(Consts.REDIS_HOST);
        static void Main(string[] args)
        {
            var subsc = redis.GetSubscriber();
            subsc.Subscribe(Consts.QUEUE_CHANNEL_COUNTER, delegate
            {
                IDatabase db = redis.GetDatabase();
                String message = db.ListRightPop(Consts.QUEUE_NAME_COUNTER);
                
                while(message != null)
                {
                    String[] resultOfSplit = message.Split(' ');
                    String text = resultOfSplit[0];
                    String id = resultOfSplit[1];
                    int countOfVowel = Regex.Matches(text, @"[aiueoy]", RegexOptions.IgnoreCase).Count;
                    int countOfConsonant = text.Length - countOfVowel;
                    
                    message = $"{id} {countOfVowel} {countOfConsonant}";
                    db.ListLeftPush(Consts.QUEUE_NAME_RATER, message, flags: CommandFlags.FireAndForget);
                    db.Multiplexer.GetSubscriber().Publish(Consts.QUEUE_CHANNEL_RATER, "");
                    Console.WriteLine(message);
                    message = null;
                    message = db.ListRightPop(Consts.QUEUE_NAME_COUNTER);
                }
                
            });

            Console.ReadKey();
        }
    }
}
