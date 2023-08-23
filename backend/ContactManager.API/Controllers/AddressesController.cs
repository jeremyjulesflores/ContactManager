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
    [Route("api/contacts/{contactId}/[controller]")]
    [ApiController]
    [Authorize]
    public class AddressesController : ControllerBase
    {
        private readonly ILogger<AddressesController> _logger;
        private readonly IAddressService _addressService;

        public AddressesController(ILogger<AddressesController> logger,
                                   IAddressService addressService)
        {
            this._logger = logger ?? throw new ArgumentException(nameof(logger));
            this._addressService = addressService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressDto>>> GetAddressesAsync(int contactId)
        {
            var user = GetUser();
            var userId = user.Id;
            try
            {
                var addresses = await _addressService.GetAddresses(userId, contactId);
                return Ok(addresses);
            }
            catch(UserNotFoundException ex)
            {
                _logger.LogCritical(
                    $"User {userId} Not found while getting Addresses for contact with id {contactId}.", ex);
                return NotFound("Not Found");
            }
            catch(ContactNotFoundException ex)
            {
                _logger.LogCritical(
                    $"Contact {contactId} Not found while getting Addresses, ex", ex);
                return NotFound("Not Found");
            }
            catch(Exception ex)
            {
                _logger.LogCritical(
                    $"Exception while getting Addresses for contact with id {contactId}.", ex);
                return StatusCode(500, "Something went wrong");
            }
            
        }

        [HttpGet("{addressId}")]
        public async Task<ActionResult<AddressDto>> GetAddressAsync(int contactId,
                                                   int addressId)
        {
            var user = GetUser();
            var userId = user.Id;
            try
            {
                var address = await _addressService.GetAddress(userId, contactId, addressId);
                if (address == null)
                {
                    return NotFound();
                }
                return Ok(address);
            }
            catch(UserNotFoundException ex)
            {
                _logger.LogCritical(
                    $"User {userId} Not found while getting Address for contact with id {contactId}.", ex);
                return NotFound("Not Found");
            }
            catch(ContactNotFoundException ex)
            {
                _logger.LogCritical(
                   $"Contact {contactId} Not found while getting Addresses", ex);
                return NotFound("Not Found");
            }
            catch(Exception ex)
            {
                _logger.LogCritical(
                    $"Exception while getting Addresses for contact with id {contactId}.", ex);
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpPost]
        public async Task<ActionResult<AddressDto>> CreateAddressAsync(int contactId,
                                                      AddressCreationDto address)
        {
            var user = GetUser();
            var userId = user.Id;
            try
            {
                await this._addressService.CreateAddress(userId, contactId, address);

                return Ok("Address Successfully Created");
            }
            catch(UserNotFoundException ex)
            {
                _logger.LogCritical($"User {userId} was not found while creating address for contact {contactId}", ex);
                return NotFound("Not Found");
            }
            catch(ContactNotFoundException ex)
            {
                _logger.LogCritical($"Contact {contactId} was not found while creating address", ex);
                return NotFound("Not Found.");
            }
            catch(ArgumentNullException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound("Not Found");
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"An Exception happened while creating address", ex);
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpPut("{addressId}")]
        public async Task<ActionResult> UpdateAddressAsync(int contactId,
                                          int addressId,
                                          AddressUpdateDto address)
        {
            var user = GetUser();
            var userId = user.Id;
            try
            {
                await _addressService.UpdateAddress(userId, contactId, addressId, address);
                return Ok("Address Updated");
            }
            catch(UserNotFoundException ex)
            {
                _logger.LogCritical($"User {userId} was not found while Updating Address for contact {contactId}", ex);
                return NotFound("Not Found");
            }
            catch(ContactNotFoundException ex)
            {
                _logger.LogCritical($"Contact {contactId} was not found while Updating Address", ex);
                return NotFound("Not Found");
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"An Exception happened while updating address", ex);
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpPatch("{addressId}")]
        public async Task<ActionResult> PartiallyUpdateAddress(int contactId,
                                                   int addressId,
                                                   JsonPatchDocument<AddressUpdateDto> patchDocument)
        {
            var user = GetUser();
            var userId = user.Id;
            try
            {
                var addressToPatch = await _addressService.GetAddressToPatch(userId, contactId, addressId);
                patchDocument.ApplyTo(addressToPatch, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                //Check if Model is correct
                //If Request is invalid it will return false and return a Bad Requet
                if (!TryValidateModel(addressToPatch))
                {
                    return BadRequest(ModelState);
                }

                await _addressService.PatchAddress(userId, contactId, addressId, addressToPatch);


                return Ok("Address Successfully Updated");
            }
            catch(UserNotFoundException ex)
            {
                _logger.LogCritical($"User {userId} was not found while patching address for contact {contactId}", ex);
                return NotFound("Not Found");
            }
            catch(ContactNotFoundException ex)
            {
                _logger.LogCritical($"Contact {contactId} was not found while patching address",ex);
                return NotFound("Not Found");
            }
            catch(Exception ex)
            {
                _logger.LogCritical("An Exception happened while patching address", ex);
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpDelete("{addressId}")]
        //[ValidateAntiForgeryToken] //Idk autocompleted
        public async Task<ActionResult> DeleteAddress(int contactId,
                                          int addressId)
        {
            var user = GetUser();
            var userId = user.Id;
            try
            {
                await _addressService.DeleteAddress(userId, contactId, addressId);
                return Ok("Delete Successful");
            }
            catch(UserNotFoundException ex)
            {
                _logger.LogCritical($"User {userId} was not found while deleting address for contact {contactId}", ex);
                return NotFound("Not Found");
            }
            catch(ContactNotFoundException ex)
            {
                _logger.LogCritical($"Contact {contactId} was not found while deleting address", ex);
                return NotFound("Not Found");
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound("Not Found");
            }
            catch (Exception ex)
            {
                _logger.LogCritical("An Exception happened while deleting address", ex);
                return StatusCode(500, "Something went wrong");
            }
        }

        private User GetUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var user = identity.Claims;
                return new User
                {
                    Username = user.FirstOrDefault(u => u.Type == ClaimTypes.Name)?.Value,
                    Id = Convert.ToInt32(user.FirstOrDefault(u => u.Type == "Id")?.Value),

                };
            }
            return null;
        }
    }

}
