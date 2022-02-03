using Microsoft.AspNetCore.Mvc;
using Phone_book.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Phone_book.ContextFolder;
using Phone_book.Data;

namespace Phone_book.Controllers
{
    public class AddController : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetDataFromViewField(int _id, string _surname, string _name,
            string _patronymic, string _phoneNumber, string _address)
        {
            using (var db = new DataContext())
            {
                db.Contacts.Add(
                    new Сontact()
                    {
                        Id = _id,
                        Surname = _surname,
                        Name = _name,
                        Patronymic = _patronymic,
                        PhoneNumber = _phoneNumber,
                        Address = _address

                    });
                db.SaveChanges();
            }

            return Redirect("~/");
        }
    }
}
