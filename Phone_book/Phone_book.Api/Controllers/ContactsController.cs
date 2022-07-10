using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Phone_book.Data.Models;
using Phone_book.Data.Repository;

namespace Phone_book.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private IRepository<Contact> _contextContacts;

        private int UserId => int.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

        public ContactsController(ILogger<ContactsController> logger, IRepository<Contact> contextContacts)
        {
            _contextContacts = contextContacts;
        }

        [HttpGet]
        [Authorize (Roles = "User")]
        [Route("all")]
        public IActionResult GetAvailableContacts()
        {
            if (UserId != 1) return Ok(Enumerable.Empty<Contact>());

            return Ok(_contextContacts.All);
        }
    }
}
