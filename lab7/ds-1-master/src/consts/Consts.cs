using System;

namespace consts
{
    public class Consts
    {
        public const String BACKEND_URL = "http://127.0.0.1:5000/api/values";
        public const String DEFAULT_REDIS_HOST = "127.0.0.1:6379";
        public const String RUS_REDIS_HOST = "127.0.0.1:8001";
        public const String EU_REDIS_HOST = "127.0.0.1:8002";
        public const String USA_REDIS_HOST = "127.0.0.1:8003";
        public const String QUEUE_NAME_COUNTER = "vowel-cons-counter-jobs";
        public const String QUEUE_CHANNEL_COUNTER = "CalculateVowelConsJob";
        public const String QUEUE_NAME_RATER = "vowel-cons-rater-jobs"; 
        public const String QUEUE_CHANNEL_RATER = "RateVowelConsJob";

        public static Region GetRegionCode(String region)
        {
            region.ToLower();
            if (region.Contains("rus") || region.Contains('1'))
            {
                return Region.rus;
            }else if (region.Contains("eu") || region.Contains('2'))
            {
                return Region.eu;
            }else if (region.Contains("usa") || region.Contains('3'))
            {
                return Region.usa;
            }
            return Region.rus;
        }

        public static String GetRegionToView(Region region)
        {
            if (region == Region.rus)
            {
                return "rus";
            }else if (region == Region.eu)
            {
                return "eu";
            }else if (region == Region.usa)
            {
                return "usa";
            }

            return "rus";
        }

        public static string GetRegionString(Region region)
        {
            if (region == Region.rus)
            {
                return "1";
            }else if (region == Region.eu)
            {
                return "2";
            }else if (region == Region.usa)
            {
                return "3";
            }

            return "1";
        }  

        public enum Region{
            rus, eu, usa
        };
    }
}