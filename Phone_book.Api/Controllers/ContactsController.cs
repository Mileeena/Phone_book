using Microsoft.AspNetCore.Mvc;
using Phone_book.Data.Models;
using Phone_book.Data.Repository;

namespace Phone_book.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class ContactsController : ControllerBase
    {
        
        private readonly ILogger<ContactsController> _logger;
        public readonly IRepository<Contact> _contextContacts;

        public ContactsController(ILogger<ContactsController> logger, IRepository<Contact> contextContacts)
        {
            _logger = logger;
            _contextContacts = contextContacts;
        }

        [HttpGet]
        [Route("contacts")]
        public ActionResult<Contact> GetAvailableContacts()
        {
            return Ok(_contextContacts.All);
        }

        [HttpGet]
        //[Authorize (Roles = "")]
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

        [HttpPost]
        //[Authorize (Roles = "")]
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
        //[Authorize (Roles = "")]
        [Route("delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
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

        [HttpPost]
        //[Authorize (Roles = "")]
        [Route("update")]
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