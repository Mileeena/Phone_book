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

        /// <summary>
        /// Get All Contacts
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/contacts
        /// 
        /// </remarks>        
        /// <returns>A list of existed contacts</returns>
        /// <response code="200">Returns the list of existed contacts</response>
        /// <response code="400">If there was an error</response>
        [HttpGet]
        [Route("contacts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Contact> GetAvailableContacts()
        {
            List<Contact> result = null;
            try
            {
                result = _contextContacts.All.ToList();
            }
            catch (Exception e)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        /// <summary>
        /// Get Selected Contact
        /// </summary>
        /// <param name="id">Contact id</param>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/contacts/id
        /// 
        /// </remarks>        
        /// <returns>Selected contact</returns>
        /// <response code="200">Returns the selected contact</response>
        /// <response code="400">If there was an error</response>
        /// <response code="404">If the contact is not found</response>
        [HttpGet]
        //[Authorize (Roles = "")]
        [Route("contacts/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Details(int id)
        {
            Contact entity = null;
            try
            {
                entity = _contextContacts.FindById(id);
                if (entity == null)
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
            return Ok(entity);
        }


        /// <summary>
        /// Add New Contact
        /// </summary>
        /// <param name="entity">Contact</param>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/add
        ///     {
        ///         "id": 0,
        ///         "surname": "Belson",
        ///         "name": "Gevin",
        ///         "patronymic": "Stuart",
        ///         "phoneNumber": "+799999999999",
        ///         "address": "HERALD NEW YORK NY 10001-3057 USA"
        ///     }
        /// </remarks>        
        /// <returns>Add contact</returns>
        /// <response code="200">Add the contact</response>
        /// <response code="400">If there was an error</response>
        /// <response code="404">If the contact is null</response>
        [HttpPost]
        //[Authorize (Roles = "")]
        [Route("add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Add([FromBody] Contact entity)
        {
            try
            {
                if (entity == null)
                {
                    return NotFound();
                }
                _contextContacts.Add(entity);
            }
            catch (Exception e)
            {
                return BadRequest();
            }

            return Ok();
        }

        /// <summary>
        /// Delete Selected Contact
        /// </summary>
        /// <param name="id">Contact id</param>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Delete/id
        /// 
        /// </remarks>        
        /// <returns>Delete selected contact</returns>
        /// <response code="200">Delete the selected contact</response>
        /// <response code="400">If there was an error</response>
        /// <response code="404">If the contact is null</response>
        [HttpGet]
        //[Authorize (Roles = "")]
        [Route("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                var entity = _contextContacts.FindById(id);
                if (entity == null)
                {
                    return NotFound();
                }
                _contextContacts.Delete(entity);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            return Ok();
        }

        /// <summary>
        /// Update Selected Contact
        /// </summary>
        /// <param name="entity">Contact</param>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/update
        ///     {
        ///         "id": 0,
        ///         "surname": "Belson",
        ///         "name": "Gevin",
        ///         "patronymic": "Stuart",
        ///         "phoneNumber": "+799999999999",
        ///         "address": "HERALD NEW YORK NY 10001-3057 USA"
        ///     }
        /// </remarks>        
        /// <returns>Update contact</returns>
        /// <response code="200">Update the contact</response>
        /// <response code="400">If there was an error</response>
        /// <response code="404">If the contact is null</response>
        [HttpPost]
        //[Authorize (Roles = "")]
        [Route("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update([FromBody] Contact entity)
        {
            try
            {
                if (entity == null)
                {
                    return NotFound();
                }
                _contextContacts.Update(entity);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            return Ok();
        }
    }
}