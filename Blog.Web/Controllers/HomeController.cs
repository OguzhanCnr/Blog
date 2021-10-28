using Blog.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blog.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
       
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(Users users)
        {
            HttpClient client = new HttpClient();
            var responseMessage = client.GetAsync("http://localhost:64985/api/Users").Result;
            List<Users> userList = null;
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                userList = JsonConvert.DeserializeObject<List<Users>>(responseMessage.Content.ReadAsStringAsync().Result);
            }
            var login = userList.FirstOrDefault(x => x.Username == users.Username && x.Password == users.Password);

            if (login !=null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,users.Username)
                };
                var useridentity = new ClaimsIdentity(claims, "Home");
                ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
                await HttpContext.SignInAsync(principal);
                return RedirectToAction("Index","Home");
            }
            return View();

        }
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            var responseMessage = client.GetAsync("http://localhost:64985/api/Users").Result;
            List<Users> admins = null;
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                admins = JsonConvert.DeserializeObject<List<Users>>(responseMessage.Content.ReadAsStringAsync().Result);
            }

            return View(admins);
          
        }
        public IActionResult Detail(int id)
        {
            HttpClient client = new HttpClient();
            var responseMessage = client.GetAsync($"http://localhost:64985/api/Users/{id}").Result;
            Users users = new Users();
            if (responseMessage.IsSuccessStatusCode)
            {
               users = JsonConvert.DeserializeObject<Users>(responseMessage.Content.ReadAsStringAsync().Result);
            }

            return View(users);



        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
