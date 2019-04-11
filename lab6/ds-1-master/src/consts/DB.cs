using System;
using StackExchange.Redis;

namespace consts
{
    public class DB
    {
        public static ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(Consts.DEFAULT_REDIS_HOST);
    }
}