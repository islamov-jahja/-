using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using StackExchange.Redis;
using System.Threading;
using consts;
using Newtonsoft.Json;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        static readonly ConcurrentDictionary<string, string> _data = new ConcurrentDictionary<string, string>();

        // GET api/values/<id>
        [HttpGet("{id}")]
        public ResultUserData Get(string id)
        {
            string value = null;
            var db = DB.redis.GetDatabase();
            ResultUserData resultUserData = new ResultUserData();
            
            while(true)
            {
                if (db.KeyExists(id)){
                    Consts.Region region = Consts.GetRegionCode(db.StringGet(id));
                    if (Exists(id, region))
                    {
                        value = GetValueFromDb(id, Consts.GetRegionCode(Consts.GetRegionString(region)));
                        resultUserData.rate = value;
                        resultUserData.region = region;
                        break;
                    }
                    else
                    {
                        Thread.Sleep(500);
                    }
                }
            }

            return resultUserData;
        }

          [HttpGet]
        public string GetTextStatistic()
        {
            var db = DB.redis.GetDatabase();
            string value = null;
            
            while(true)
            {
                if (db.KeyExists("text_statistic"))
                {
                    value = db.StringGet("text_statistic");
                    break;
                }

                Thread.Sleep(500);
            }

            return value;
        }

        public bool Exists(string id, Consts.Region region)
        {
            if (region == Consts.Region.rus)
            {
                var db = DB.redis.GetDatabase(1);
                return db.KeyExists(id);
            }

            if (region == Consts.Region.eu){
                var db = DB.redis.GetDatabase(2);
                return db.KeyExists(id);
            }

            if (region == Consts.Region.usa){
                var db = DB.redis.GetDatabase(3);
                return db.KeyExists(id);
            }

            return false;
        }
        public string GetValueFromDb(string id, Consts.Region region)
        {
            Console.WriteLine($"region: {region}");
            if (region == Consts.Region.rus)
            {
                var db = DB.redis.GetDatabase(1);
                return db.StringGet(id);
            }

            if (region == Consts.Region.eu){
                var db = DB.redis.GetDatabase(2);
                return db.StringGet(id);
            }

            if (region == Consts.Region.usa){
                var db = DB.redis.GetDatabase(3);
                return db.StringGet(id);
            }

            return null;
        }

        // POST api/values
        [HttpPost]
        public string Post([FromBody]UserData value)
        {
            var id = Guid.NewGuid().ToString();
            _data[id] = value.data;
            Console.WriteLine("data: " + value.data + "; Region: " + value.region);
            IDatabase db = DB.redis.GetDatabase();
            db.StringSet(id, Consts.GetRegionString(value.region));
            db.StringSet($"text {id}", value.data);
            var subsc = DB.redis.GetSubscriber();
            subsc.Publish("events", id);
            return id;
        }
    }
}
