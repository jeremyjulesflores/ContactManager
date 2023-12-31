﻿using ContactManager.API.Entities;
using ContactManager.API.Exceptions;
using ContactManager.API.Helper;
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
    [Route("api/contacts")]
    [Authorize]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IGetUser _getUser;

        public ContactsController(IContactService _contactService, IGetUser getUser)
        {
            this._contactService = _contactService;
            this._getUser = getUser ?? throw new ArgumentNullException(nameof(getUser));
        }
        /// <summary>
        /// Gets a Contact associated with the authenticated user
        /// </summary>
        /// <returns>An ActionResult containing ContactWithoutDetailsDto</returns>
        /// <remarks>
        /// 
        /// SAMPLE: Request:
        ///     GET /api/contacts/1
        /// 
        /// </remarks>
        /// <response code="200">Successfully Get a list of Contact</response>
        /// <response code="500">Server Error</response>
        /// <response code="404">User is not Found</response>
        /// <response code="404">Contact is not Found</response>
        [Produces("application/json")]
        [ProducesResponseType(typeof(ContactWithoutDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{contactId}")]
        public async Task<IActionResult> GetContactAsync(int contactId)
        {
            try
            {
                var user = _getUser.Get();
                var userId = user.Id;
                var contact = await _contactService.GetContact(userId, contactId);
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
        /// <summary>
        /// Gets all Contacts associated with the authenticated user.
        /// </summary>
        /// <returns>An ActionResult containing IEnumerable of ContactWithoutDetailsDto</returns>
        /// <remarks>
        /// 
        /// SAMPLE: Request:
        ///     GET /api/contacts
        /// 
        /// </remarks>
        /// <response code="200">Successfully Get a list of Contact</response>
        /// <response code="500">Server Error</response>
        /// <response code="404">User is not Found</response>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ContactWithoutDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ContactWithoutDetailsDto>>> GetContactsAsync()
        {
            var user = _getUser.Get();
            var userId = user.Id;
            try
            {
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
        

        [HttpPost]
        public async Task<IActionResult> CreateContact(ContactCreationDto contact)
        {
            try
            {
                var user = _getUser.Get();  
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
                var user = _getUser.Get();
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
                var user = _getUser.Get();
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
                return NotFound("Not Found");
            }
            catch (ContactNotFoundException ex)
            {
                return NotFound("Not Found");
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
                var user = _getUser.Get();
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
    }
}
