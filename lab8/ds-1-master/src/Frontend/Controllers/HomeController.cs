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

namespace Frontend.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> TextDetails(string id)
        {
            string url = Consts.BACKEND_URL + $"/{id}";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);

            using(HttpContent responseContent = response.Content)
            {
                string jsonData = await responseContent.ReadAsStringAsync();
                ResultUserData resultData = JsonConvert.DeserializeObject<ResultUserData>(jsonData);
                ViewData["rate"] = resultData.rate;
                ViewData["id"] = id;
                ViewData["region"] = Consts.GetRegionToView(resultData.region);
                return View(); 
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadAsync(string data, string region)
        {
            HttpClient client = new HttpClient();
            UserData user = new UserData();
            user.data = data;
            user.region = Consts.GetRegionCode(region);
            HttpResponseMessage response = await client.PostAsJsonAsync(Consts.BACKEND_URL, user);
            using(HttpContent responseContent = response.Content)
            {
                return Redirect("TextDetails/" + await responseContent.ReadAsStringAsync());
            }
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
