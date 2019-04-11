using System;
using StackExchange.Redis;
using System.Text.RegularExpressions;
using consts;
using Newtonsoft.Json;

namespace VowelConsCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            var subsc = DB.redis.GetSubscriber();
            subsc.Subscribe(Consts.QUEUE_CHANNEL_COUNTER, delegate
            {
                IDatabase db = DB.redis.GetDatabase();
                String id = db.ListRightPop(Consts.QUEUE_NAME_COUNTER);
                
                while(id != null)
                {
                    String text = db.StringGet($"text {id}");
                    
                    Console.WriteLine($"Region: {Consts.GetRegionToView(Consts.GetRegionCode(db.StringGet(id)))}");
                    Console.WriteLine($"Id: {id}");

                    int countOfVowel = Regex.Matches(text, @"[aiueoy]", RegexOptions.IgnoreCase).Count;
                    int countOfConsonant = text.Length - countOfVowel;
                    
                    string message = $"{id} {countOfVowel} {countOfConsonant}";
                    db.ListLeftPush(Consts.QUEUE_NAME_RATER, message, flags: CommandFlags.FireAndForget);
                    db.Multiplexer.GetSubscriber().Publish(Consts.QUEUE_CHANNEL_RATER, "");

                    id = db.ListRightPop(Consts.QUEUE_NAME_COUNTER);
                }
                
            });

            Console.ReadKey();
        }
    }
}
