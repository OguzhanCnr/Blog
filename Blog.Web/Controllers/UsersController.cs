using Blog.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Blog.Web.Controllers
{
    public class UsersController : Controller
    {
        HttpClient _httpClient = new HttpClient();
        public IActionResult Index()
        {
           
            var responseMessage = _httpClient.GetAsync("http://localhost:64985/api/Users").Result;
            List<Users> admins = null;
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                admins = JsonConvert.DeserializeObject<List<Users>>(responseMessage.Content.ReadAsStringAsync().Result);
            }
            
            return View(admins);
        }

        public IActionResult Add()
        {
            return View(new Users());
        }
        [HttpPost]
        public IActionResult Add(Users admin)
        {
           
            StringContent content = new StringContent(JsonConvert.SerializeObject(admin), System.Text.Encoding.UTF8, "application/json");
            var responseMessage = _httpClient.PostAsync("http://localhost:64985/api/Users", content).Result;
           
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {

         
            var responseMessage = _httpClient.GetAsync($"http://localhost:64985/api/Users/{id}").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var admin = JsonConvert.DeserializeObject<Users>(responseMessage.Content.ReadAsStringAsync().Result);
                return View(admin);
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Edit(Users admin)
        {
          
            StringContent content = new StringContent(JsonConvert.SerializeObject(admin), System.Text.Encoding.UTF8, "application/json");
            var responseMessage = _httpClient.PutAsync($"http://localhost:64985/api/Users/{admin.UserId}", content).Result;

            return RedirectToAction("Index");
        }

        //http://localhost:59233/api/Products
        public IActionResult Delete(int id)
        {
         
            var responseMessage = _httpClient.DeleteAsync($"http://localhost:64985/api/Users/{id}").Result;

            return RedirectToAction("Index");
        }
    }
}
