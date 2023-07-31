using AutoMapper;
using ContactManager.API.Entities;
using ContactManager.API.Models;
using ContactManager.API.Models.CreationDtos;
using ContactManager.API.Models.UpdateDtos;
using ContactManager.API.Repositories;
using ContactManager.API.Repositories.Shared;
using ContactManager.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.API.Controllers
{
    [Route("api/contacts/{contactId}/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly ILogger<AddressesController> _logger;
        private readonly IMapper _mapper;
        private readonly ISharedRepository _sharedRepository;
        private readonly IAddressService _addressService;

        public AddressesController(ILogger<AddressesController> logger,
                                   IMapper mapper,
                                   ISharedRepository sharedRepository,
                                   IAddressService addressService)
        {
            this._logger = logger ?? throw new ArgumentException(nameof(logger));
            this._mapper = mapper;
            this._sharedRepository = sharedRepository;
            this._addressService = addressService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressDto>>> GetAddressesAsync(int contactId)
        {
            try
            {
                var addresses = await _addressService.GetAddresses(contactId);
                
                if(addresses == null)
                {
                    return NotFound();
                }

                return Ok(addresses);
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
            var address = await _addressService.GetAddress(addressId: addressId, contactId: contactId);

            if(address == null)
            {
                return NotFound();
            }

            return Ok(address);
        }

        [HttpPost]
        public async Task<ActionResult<AddressDto>> CreateAddressAsync(int contactId,
                                                      AddressCreationDto address)
        {
            var created = await this._addressService.CreateAddress(contactId, address);

            if (!created)
            {
                return BadRequest();
            }

            return Ok("Address Successfully Created");
        }

        [HttpPut("{addressId}")]
        public async Task<ActionResult> UpdateAddressAsync(int contactId,
                                          int addressId,
                                          AddressUpdateDto address)
        {
            var updated = await _addressService.UpdateAddress(contactId, addressId, address);
            if (!updated)
            {
                return NotFound();
            }

            return Ok("Address Updated");
        }

        [HttpPatch("{addressId}")]
        public async Task<ActionResult> PartiallyUpdateAddress(int contactId,
                                                   int addressId,
                                                   JsonPatchDocument<AddressUpdateDto> patchDocument)
        {
            var addressToPatch = await _addressService.GetAddressToPatch(contactId, addressId);   
            if (addressToPatch == null)
            {
                return NotFound();
            }


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

            if(!await _addressService.PatchNumber(contactId, addressId, addressToPatch))
            {
                return BadRequest(ModelState);
            }

            return Ok("Address Successfully Updated");
        }

        [HttpDelete("{addressId}")]
        //[ValidateAntiForgeryToken] //Idk autocompleted
        public async Task<ActionResult> DeleteAddress(int contactId,
                                          int addressId)
        {
            var deleted = await _addressService.DeleteAddress(contactId, addressId);

            if (!deleted)
            {
                return NotFound();
            }
            return Ok("Delete Successful");
        }
    }
}
