using Microsoft.AspNetCore.Mvc;
using Phone_book.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Phone_book.ContextFolder;

namespace Phone_book.Controllers
{
    public class MyDefaultController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Contacts = new DataContext().Contacts;
            return View();
        }
    }
}
