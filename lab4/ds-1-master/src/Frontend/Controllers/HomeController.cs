﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Frontend.Models;
using System.Net.Http;
using consts;

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
                ViewData["rate"] = await responseContent.ReadAsStringAsync();
                ViewData["id"] = id;
                return View(); 
            }

            
        }

        [HttpPost]
        public async Task<IActionResult> UploadAsync(string data)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsJsonAsync(Consts.BACKEND_URL, data);
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
