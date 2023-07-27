using AutoMapper;
using ContactManager.API.Entities;
using ContactManager.API.Models;
using ContactManager.API.Models.CreationDtos;
using ContactManager.API.Models.UpdateDtos;
using ContactManager.API.Repositories;
using ContactManager.API.Repositories.Shared;
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
        private readonly IAddressRepository _repository;
        private readonly IMapper _mapper;
        private readonly ISharedRepository _sharedRepository;

        public AddressesController(ILogger<AddressesController> logger,
                                   IAddressRepository repository,
                                   IMapper mapper,
                                   ISharedRepository sharedRepository)
        {
            this._logger = logger ?? throw new ArgumentException(nameof(logger));
            this._repository = repository;
            this._mapper = mapper;
            this._sharedRepository = sharedRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressDto>>> GetAddressesAsync(int contactId)
        {
            try
            {
                if (!await _sharedRepository.ContactExists(contactId))
                {
                    _logger.LogInformation(
                        $"Contact with Id {contactId} was not found when accessing GetAddresses.");
                    return NotFound();
                }
                
                var addresses = await _repository.GetAddresses(contactId);
                return Ok(_mapper.Map<IEnumerable<AddressDto>>(addresses));
            }
            catch(Exception ex)
            {
                _logger.LogCritical(
                    $"Exception while getting Addresses for contact with id {contactId}.", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
            
        }

        [HttpGet("{addressId}", Name = "GetAddress")]
        public async Task<ActionResult<AddressDto>> GetAddressAsync(int contactId,
                                                   int addressId)
        {
            if(!await _sharedRepository.ContactExists(contactId))
            {
                return NotFound();
            }

            var address = await _repository.GetAddress(addressId);

            if(address == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AddressDto>(address));
        }

        [HttpPost]
        public async Task<ActionResult<AddressDto>> CreateAddressAsync(int contactId,
                                                      AddressCreationDto address)
        {
            if (!await _sharedRepository.ContactExists(contactId))
            {
                return NotFound();
            }

            var finalAddress = _mapper.Map<Address>(address);



            

            return CreatedAtRoute("GetAddress",
                new
                {
                    contactId,
                    addressId = finalAddress.Id 
                },
                finalAddress);
        }

        [HttpPut("{addressId}")]
        public ActionResult UpdateAddress(int contactId,
                                          int addressId,
                                          AddressUpdateDto address)
        {
            var contact = ContactsDataStore.Current.Contacts.FirstOrDefault(c=> c.Id ==contactId);
            if(contact == null)
            {
                return NotFound();
            }

            var addressFromStore = contact.Addresses.FirstOrDefault(c => c.Id == addressId);
            if(addressFromStore == null)
            {
                return NotFound();
            }

            addressFromStore.Type = address.Type;
            addressFromStore.AddressDetails = address.AddressDetails;

            return NoContent();
        }

        [HttpPatch("{addressId}")]
        public ActionResult PartiallyUpdateAddress(int contactId,
                                                   int addressId,
                                                   JsonPatchDocument<AddressUpdateDto> patchDocument)
        {
            var contact = ContactsDataStore.Current.Contacts.FirstOrDefault(c => c.Id == contactId);
            if (contact == null)
            {
                return NotFound();
            }

            var addressFromStore = contact.Addresses.FirstOrDefault(c => c.Id == addressId);
            if (addressFromStore == null)
            {
                return NotFound();
            }

            //Can use mapper here
            var addressToPatch =
                new AddressUpdateDto()
                {
                    Type = addressFromStore.Type,
                    AddressDetails = addressFromStore.AddressDetails
                };

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

            addressFromStore.Type = addressToPatch.Type;
            addressFromStore.AddressDetails = addressToPatch.AddressDetails;

            return NoContent();
        }

        [HttpDelete("{addressId}")]
        //[ValidateAntiForgeryToken] //Idk autocompleted
        public ActionResult DeleteAddress(int contactId,
                                          int addressId)
        {
            var contact = ContactsDataStore.Current.Contacts.FirstOrDefault(c => c.Id == contactId);
            if(contact == null)
            {
                return NotFound();
            }

            var addressFromStore = contact.Addresses.FirstOrDefault(a=>a.Id == addressId);

            if(addressFromStore == null)
            {
                return NotFound();
            }

            contact.Addresses.Remove(addressFromStore);
            return NoContent();
        }
    }
}
