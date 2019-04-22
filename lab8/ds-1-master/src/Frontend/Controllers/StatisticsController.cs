using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Frontend.Models;
using System.Net.Http;
using consts;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Frontend.Controllers
{
    public class StatisticsController : Controller
    {
        [HttpGet]
        public async Task<ActionResult> TextStatistics()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(Consts.BACKEND_URL);

            using(HttpContent responseContent = response.Content)
            {
                string jsonData = await responseContent.ReadAsStringAsync();
                TextStatisticData resultData = JsonConvert.DeserializeObject<TextStatisticData>(jsonData);
                ViewData["textCount"] = resultData.countOfText;
                ViewData["hightRankCount"] = resultData.countOfTextWithHightRank;
                ViewData["avarageRank"] = resultData.avarageRank;
                return View(); 
            }
        }
    }
}