using System;
using StackExchange.Redis;
using System.Text.RegularExpressions;
using consts;

namespace VowelConsRater
{
    class Program
    {
        static void Main(string[] args)
        {
            var subsc = DB.redis.GetSubscriber();
            subsc.Subscribe(Consts.QUEUE_CHANNEL_RATER, delegate 
            {
                IDatabase db = DB.redis.GetDatabase();
                String message = db.ListRightPop(Consts.QUEUE_NAME_RATER);
                while(message != null)
                {
                    String[] results = message.Split(' ');
                    String id = results[0];
                    int countOfVowel = Convert.ToInt32(results[1]);
                    int countOfConsonant = Convert.ToInt32(results[2]);
                    string region = db.StringGet(id);
                    Double relation = 0;

                    if (countOfConsonant != 0)
                    {
                        relation = Convert.ToDouble(countOfVowel)/Convert.ToDouble(countOfConsonant);
                    }

                    Console.WriteLine($"Region: {Consts.GetRegionToView(Consts.GetRegionCode(region))}");
                    Console.WriteLine($"Id: {id}");
                    SetValueToDb(id, relation, Consts.GetRegionCode(region));
                    message = null;
                    message = db.ListRightPop(Consts.QUEUE_NAME_RATER);
                }
            });

            Console.ReadKey();
        }

        public static void SetValueToDb(string id, Double value, Consts.Region region)
        {
            Console.WriteLine($"region: {region}");
            if (region == Consts.Region.rus)
            {
                var db = DB.redis.GetDatabase(1);
                db.StringSet(id, value);
            }

            if (region == Consts.Region.eu){
                var db = DB.redis.GetDatabase(2);
                db.StringSet(id, value);
            }

            if (region == Consts.Region.usa){
                var db = DB.redis.GetDatabase(3);
                db.StringSet(id, value);
            }
        }
    }
}
