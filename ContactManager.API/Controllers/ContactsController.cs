using ContactManager.API.Models;
using ContactManager.API.Models.CreationDtos;
using ContactManager.API.Models.UpdateDtos;
using ContactManager.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.API.Controllers
{
    [ApiController]
    [Route("api/users/{userId}/[controller]")]
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

        [HttpPut("{contactId}")]
        public async Task<ActionResult> UpdateAddressAsync(int userId,
                                          int contactId,
                                          ContactUpdateDto contact)
        {
            var updated = await _contactService.UpdateContact(userId, contactId, contact);
            if (!updated)
            {
                return NotFound();
            }

            return Ok("Contact Updated");
        }

        [HttpPatch("{contactId}")]
        public async Task<ActionResult> PartiallyUpdateAddress(int userId,
                                                   int contactId,
                                                   JsonPatchDocument<ContactUpdateDto> patchDocument)
        {
            var contactToPatch = await _contactService.GetContactToPatch(userId, contactId);
            if (contactToPatch == null)
            {
                return NotFound();
            }


            patchDocument.ApplyTo(contactToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Check if Model is correct
            //If Request is invalid it will return false and return a Bad Requet
            if (!TryValidateModel(contactToPatch))
            {
                return BadRequest(ModelState);
            }

            if (!await _contactService.PatchContact(userId, contactId, contactToPatch))
            {
                return BadRequest(ModelState);
            }

            return Ok("Contact Successfully Updated");
        }

        [HttpDelete("{contactId}")]
        //[ValidateAntiForgeryToken] //Idk autocompleted
        public async Task<ActionResult> DeleteAddress(int userId,
                                          int contactId)
        {
            var deleted = await _contactService.DeleteContact(userId, contactId);

            if (!deleted)
            {
                return NotFound();
            }
            return Ok("Delete Successful");
        }

    }
}
