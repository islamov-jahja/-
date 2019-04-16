using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using StackExchange.Redis;
using consts;

namespace TextStatistic
{
    class Program
    {
        private static Double rank;
        private static int allCountOfText;
        private static int countTextsWithHighRate;
        private static Double avgRank;
        static void Main(string[] args)
        {
            var subsc = DB.redis.GetSubscriber();
            subsc.Subscribe(Consts.TEXT_RANK_CALCULATED, (channel, message) => {
                DataToStatistic statisticMessage = JsonConvert.DeserializeObject<DataToStatistic>(message);
                Console.WriteLine(message);
                rank += statisticMessage.rate;
                allCountOfText++;

                if (statisticMessage.rate > 0.5)
                {
                    countTextsWithHighRate++;
                }

                avgRank = rank / allCountOfText;
                
                Console.WriteLine($"countOfText: {allCountOfText}  highRankPart: {countTextsWithHighRate}+ avgRank: {avgRank}");
                TextStatisticData textStatistic = new TextStatisticData();
                textStatistic.countOfText = allCountOfText;
                textStatistic.avarageRank = avgRank;
                textStatistic.countOfTextWithHightRank = countTextsWithHighRate;
                DB.redis.GetDatabase().StringSet("text_statistic", JsonConvert.SerializeObject(textStatistic));
            });

            Console.ReadKey();
        }
    }
}
