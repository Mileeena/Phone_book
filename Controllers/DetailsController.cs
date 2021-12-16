using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Phone_book.Data;
using Phone_book.Models;

namespace Phone_book.Controllers
{
    public class DetailsController : Controller
    {
        public Сontact Contact { get; private set; }
        public IActionResult Details(int id)
        {
            Contact = Repository.GetContactById(id);
            return View(Contact);
        }
    }
}
