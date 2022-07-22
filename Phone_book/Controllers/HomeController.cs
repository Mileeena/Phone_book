using Microsoft.AspNetCore.Mvc;
using System.Text;
using Newtonsoft.Json;
using Phone_book.Data.Models;

namespace Phone_book.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var url = @$"{AppConst.ApiPath}/api/contacts";

            string json = _httpClient.GetStringAsync(url).Result;

            ViewBag.Contacts = JsonConvert.DeserializeObject<List<Contact>>(json);
            return View();
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var url = @$"{AppConst.ApiPath}/api/contacts/{id}";

            string json = _httpClient.GetStringAsync(url).Result;

            Contact entity = JsonConvert.DeserializeObject<Contact>(json);
            if (entity == null)
            {
                return RedirectToAction("Index");
            }
            return View(entity);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var url = @$"{AppConst.ApiPath}/api/delete/{id}";

            using (_httpClient)
            {
                var content = new StringContent(JsonConvert.SerializeObject(id),
                    Encoding.UTF8, "application/json");
                HttpResponseMessage result = _httpClient.GetAsync(url).Result;
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult GetDataFromViewField(string _surname, string _name,
            string _patronymic, string _phoneNumber, string _address)
        {
            var entity = new Contact()
            {
                Surname = _surname,
                Name = _name,
                Patronymic = _patronymic,
                PhoneNumber = _phoneNumber,
                Address = _address
            };

            var url = @$"{AppConst.ApiPath}/api/add";


            using (_httpClient)
            {
                var content = new StringContent(JsonConvert.SerializeObject(entity),
                    Encoding.UTF8, "application/json");
                HttpResponseMessage result = _httpClient.PostAsync(url, content).Result;
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var url = @$"{AppConst.ApiPath}/api/contacts/{id}";

            string json = _httpClient.GetStringAsync(url).Result;

            Contact entity = JsonConvert.DeserializeObject<Contact>(json);

            return View(entity);
        }

        [HttpPost]
        public IActionResult Update(int _id, string _surname, string _name,
            string _patronymic, string _phoneNumber, string _address)
        {
            var url = @$"{AppConst.ApiPath}/api/contacts/{_id}";

            string json = _httpClient.GetStringAsync(url).Result;

            Contact entity = JsonConvert.DeserializeObject<Contact>(json);
            if (entity == null) entity = new Contact();

            entity.Surname = _surname;
            entity.Name = _name;
            entity.Patronymic = _patronymic;
            entity.PhoneNumber = _phoneNumber;
            entity.Address = _address;

            url = @$"{AppConst.ApiPath}/api/update";

            using (_httpClient)
            {
                var content = new StringContent(JsonConvert.SerializeObject(entity),
                    Encoding.UTF8, "application/json");
                HttpResponseMessage result = _httpClient.PostAsync(url, content).Result;
            }

            return RedirectToAction("Index");
        }
    }
}