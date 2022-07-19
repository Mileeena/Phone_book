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
        public IRepository<Contact> _contextContacts { get; private set; }

        private int UserId => int.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

        public ContactsController(ILogger<ContactsController> logger, IRepository<Contact> contextContacts)
        {
            _contextContacts = contextContacts;
        }

        [HttpGet]
        //[Authorize (Roles = "User")]
        [Route("contacts")]
        public IActionResult GetAvailableContacts()
        {
            if (UserId != 1) return Ok(Enumerable.Empty<Contact>());

            return Ok(_contextContacts.All);
        }


        [HttpGet]
        //[Authorize (Roles = "User")]
        [Route("contacts/{id}")]
        public IActionResult Details(int id)
        {
            Contact entity = _contextContacts.FindById(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpGet]
        //[Authorize (Roles = "User")]
        [Route("add")]
        public IActionResult Add([FromBody] Contact entity)
        {
            try
            {
                _contextContacts.Add(entity);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet]
        //[Authorize (Roles = "User")]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _contextContacts.Delete(_contextContacts.FindById(id));
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet]
        //[Authorize (Roles = "User")]
        [Route("update/{id}")]
        public IActionResult Update([FromBody] Contact entity)
        {
            try
            {
                _contextContacts.Update(entity);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
