using System;
using StackExchange.Redis;
using System.Text.RegularExpressions;

namespace TextRankCalc
{
    class Program
    {
        private static ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("127.0.0.1:6379");
        static void Main(string[] args)
        {
            var subsc = redis.GetSubscriber();
            subsc.Subscribe("events", (channel, id) => {
                var db = redis.GetDatabase();
                String message = db.StringGet((string)id);
                int countOfVowel = Regex.Matches(message, @"[aiueoy]", RegexOptions.IgnoreCase).Count;
                int countOfConsonant = message.Length - countOfVowel;
                db.StringSet("calculate: " + id, countOfVowel/countOfConsonant);
                Console.WriteLine("Text: " + (string)message);
            });
            Console.ReadKey();
        }
    }
}
