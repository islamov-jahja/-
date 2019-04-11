using System;
using StackExchange.Redis;
using System.Text.RegularExpressions;
using consts;



namespace TextRankCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            var subsc = DB.redis.GetSubscriber();
            subsc.Subscribe("events", (channel, id) => {
                string m_id = id;
                IDatabase db = DB.redis.GetDatabase();
                Console.WriteLine($"Region: {Consts.GetRegionToView(Consts.GetRegionCode(db.StringGet(m_id)))}");
                Console.WriteLine($"Id: {id}");
                db.ListLeftPush( Consts.QUEUE_NAME_COUNTER, m_id, flags: CommandFlags.FireAndForget );
                db.Multiplexer.GetSubscriber().Publish( Consts.QUEUE_CHANNEL_COUNTER, "" );
            });
            Console.ReadKey();
        }
    }
}
