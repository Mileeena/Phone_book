using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;
using Phone_book.Data.Models;
using Phone_book.Data.Repository;

namespace Phone_book.Controllers
{
    public class HomeController : Controller
    {
        private IRepository<Contact> _contextContacts { get; }
        private readonly HttpClient _httpClient = new HttpClient();

        public HomeController(ILogger<HomeController> logger, IRepository<Contact> contextContacts)
        {
            _contextContacts = contextContacts;
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
            var entity = _contextContacts.FindById(id);
            _contextContacts.Delete(entity);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult GetDataFromViewField(string _surname, string _name,
            string _patronymic, string _phoneNumber, string _address)
        {
                _contextContacts.Add(
                    new Contact()
                    {
                        Surname = _surname,
                        Name = _name,
                        Patronymic = _patronymic,
                        PhoneNumber = _phoneNumber,
                        Address = _address
                    });

                return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Contact entity = _contextContacts.FindById(id);
            return View(entity);
        }

        [HttpPost]
        public IActionResult Update(int _id, string _surname, string _name,
            string _patronymic, string _phoneNumber, string _address)
        {
            var entity = _contextContacts.FindById(_id);
            if(entity == null) entity = new Contact();

            entity.Surname = _surname;
            entity.Name = _name;
            entity.Patronymic = _patronymic;
            entity.PhoneNumber = _phoneNumber;
            entity.Address = _address;
            _contextContacts.Update(entity);
            return RedirectToAction("Index");
        }
    }
}