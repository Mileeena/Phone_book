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
            Contact byId = _contextContacts.FindById(id);
            return View(byId);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetDataFromViewField(int _id, string _surname, string _name,
            string _patronymic, string _phoneNumber, string _address)
        {
                _contextContacts.Add(
                    new Contact()
                    {
                        Id = _id,
                        Surname = _surname,
                        Name = _name,
                        Patronymic = _patronymic,
                        PhoneNumber = _phoneNumber,
                        Address = _address

                    });

                return Redirect("~/");
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}