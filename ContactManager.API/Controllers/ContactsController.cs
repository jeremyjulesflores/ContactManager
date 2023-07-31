using AutoMapper;
using ContactManager.API.Models;
using ContactManager.API.Models.CreationDtos;
using ContactManager.API.Repositories;
using ContactManager.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.API.Controllers
{
    [ApiController]
    [Route("api/user/{userId}/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;


        public ContactsController(IContactService _contactService)
        {
            this._contactService = _contactService;

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactWithoutDetailsDto>>> GetContactsAsync(int userId)
        {
            var contacts = await _contactService.GetContacts(userId);
            if (contacts == null)
            {
                return NotFound();
            }
            
            return Ok(contacts);
        }

        [HttpGet("{contactId}")]
        public async Task<IActionResult> GetContactAsync(int contactId,
                                                         int userId)
        {
            var contact = await _contactService.GetContact(contactId, userId);
            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact(int userId, ContactCreationDto contact)
        {
            var created = await _contactService.CreateContact(userId, contact);

            if (!created)
            {
                return BadRequest();
            }

            return Ok("Contact Successfully Created");
        }
  
    }
}
