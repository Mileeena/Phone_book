using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Phone_book.ContextFolder;
using Phone_book.Data;
using Phone_book.Models;

namespace Phone_book.Controllers
{
    public class DetailsController : Controller
    {
        public IActionResult Details(int id)
        {
            return View(new DataContext().GetContactById(id));
        }
    }
}
