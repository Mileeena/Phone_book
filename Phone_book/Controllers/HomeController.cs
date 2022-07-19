using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Phone_book.Data.Models;
using Phone_book.Data.Repository;

namespace Phone_book.Controllers
{
    public class HomeController : Controller
    {
        public IRepository<Contact> _contextContacts { get; private set; }

        public HomeController(ILogger<HomeController> logger, IRepository<Contact> contextContacts)
        {
            _contextContacts = contextContacts;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Contacts = _contextContacts.All;
            return View();
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Contact entity = _contextContacts.FindById(id);
            if (entity == null)
            {
                return Redirect("~/");
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
            return Redirect("~/");
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

                return Redirect("~/");
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
            return Redirect("~/");
        }
    }
}