using ContactManager.API.Entities;
using ContactManager.API.Exceptions;
using ContactManager.API.Models;
using ContactManager.API.Models.CreationDtos;
using ContactManager.API.Models.UpdateDtos;
using ContactManager.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ContactManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;


        public ContactsController(IContactService _contactService)
        {
            this._contactService = _contactService;

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactWithoutDetailsDto>>> GetContactsAsync()
        {
            try
            {
                var user = GetUser();
                var userId = user.Id;
                var contacts = await _contactService.GetContacts(userId);
                return Ok(contacts);
            }
            catch(UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong.");
            }
            
        }

        [HttpGet("{contactId}")]
        public async Task<IActionResult> GetContactAsync(int contactId)
        {
            try
            {
                var user = GetUser();
                var userId = user.Id;
                var contact = await _contactService.GetContact(contactId, userId);
                return Ok(contact);
            }
            catch (UserNotFoundException ex) 
            { 
                return NotFound(ex.Message);
            }
            catch (ContactNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }   
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact(ContactCreationDto contact)
        {
            try
            {
                var user = GetUser();
                var userId = user.Id;
                await _contactService.CreateContact(userId, contact);
                return Ok("Contact Successfully Created");
            }
            catch(UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong.");
            }
        }

        [HttpPut("{contactId}")]
        public async Task<ActionResult> UpdateAddressAsync(int contactId,
                                          ContactUpdateDto contact)
        {
            try
            {
                var user = GetUser();
                var userId = user.Id;
                await _contactService.UpdateContact(userId, contactId, contact);
                return Ok("Contact Updated");
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ContactNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpPatch("{contactId}")]
        public async Task<ActionResult> PartiallyUpdateAddress(int contactId,
                                                   JsonPatchDocument<ContactUpdateDto> patchDocument)
        {
            try
            {
                var user = GetUser();
                var userId = user.Id;
                var contactToPatch = await _contactService.GetContactToPatch(userId, contactId);
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

                await _contactService.PatchContact(userId, contactId, contactToPatch);
                return Ok("Contact Successfully Updated");
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ContactNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
           
        }

        [HttpDelete("{contactId}")]
        //[ValidateAntiForgeryToken] //Idk autocompleted
        public async Task<ActionResult> DeleteAddress(int contactId)
        {
            try
            {
                var user = GetUser();
                var userId = user.Id;
                await _contactService.DeleteContact(userId, contactId);
                return Ok("Delete Successful");
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ContactNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        private User GetUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if(identity!= null)
            {
                var user = identity.Claims;
                return new User
                {
                    Username = user.FirstOrDefault(u => u.Type == ClaimTypes.Name)?.Value,
                    Id = Convert.ToInt32(user.FirstOrDefault(u=> u.Type == "Id")?.Value),

                };
            }
            return null;
        }

    }
}
