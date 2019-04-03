using System;
using StackExchange.Redis;
using System.Text.RegularExpressions;
using consts;

namespace VowelConsRater
{
    class Program
    {
        private static ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(Consts.REDIS_HOST);
        static void Main(string[] args)
        {
            var subsc = redis.GetSubscriber();
            subsc.Subscribe(Consts.QUEUE_CHANNEL_RATER, delegate 
            {
                IDatabase db = redis.GetDatabase();
                String message = db.ListRightPop(Consts.QUEUE_NAME_RATER);

                while(message != null)
                {
                    String[] results = message.Split(' ');
                    String id = results[0];
                    int countOfVowel = Convert.ToInt32(results[1]);
                    int countOfConsonant = Convert.ToInt32(results[2]);
                    Double relation = 0;

                    if (countOfConsonant != 0)
                    {
                        relation = countOfVowel/countOfConsonant;
                    }
                    db.StringSet("calculate: " + id, relation);
                    Console.WriteLine(message);
                    message = null;
                    message = db.ListRightPop(Consts.QUEUE_NAME_RATER);
                }
            });

            Console.ReadKey();
        }
    }
}
