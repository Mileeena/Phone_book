using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Phone_book.WebClient.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string Email, string Password)
        {
            var url = @$"{AppConst.AuthApiPath}/api/login";


            using (_httpClient)
            {
                var content = new StringContent(JsonConvert.SerializeObject(new { Email, Password}),
                    Encoding.UTF8, "application/json");
                HttpResponseMessage result = _httpClient.PostAsync(url, content).Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return RedirectToAction("Add", "Home");
        }
    }
}
