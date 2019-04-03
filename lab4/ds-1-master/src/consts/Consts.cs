using System;

namespace consts
{
    public class Consts
    {
        public const String BACKEND_URL = "http://127.0.0.1:5000/api/values";
        public const String REDIS_HOST = "127.0.0.1:6379";
        public const String QUEUE_NAME_COUNTER = "vowel-cons-counter-jobs";
        public const String QUEUE_CHANNEL_COUNTER = "CalculateVowelConsJob";
        public const String QUEUE_NAME_RATER = "vowel-cons-rater-jobs"; 
        public const String QUEUE_CHANNEL_RATER = "RateVowelConsJob";  
    }
}