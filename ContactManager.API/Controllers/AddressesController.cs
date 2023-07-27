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

            var address = await _repository.GetAddress(addressId, contactId);

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

            var addressToCreate = _mapper.Map<Address>(address);

            await _repository.CreateAddress(contactId, addressToCreate);

            await _sharedRepository.SaveChangesAsync();

            var createdAddressToReturn = _mapper.Map<AddressDto>(addressToCreate);

            return CreatedAtRoute("GetAddress",
                new
                {
                    contactId,
                    addressId = createdAddressToReturn.Id 
                },
                createdAddressToReturn);
        }

        [HttpPut("{addressId}")]
        public async Task<ActionResult> UpdateAddressAsync(int contactId,
                                          int addressId,
                                          AddressUpdateDto address)
        {
            if (!await _sharedRepository.ContactExists(contactId))
            {
                return NotFound();
            }

            var addressEntity = await _repository.GetAddress(addressId, contactId);

            if(addressEntity == null)
            {
                return NotFound();
            }

            //automapper will override addresEntity with the addresss
            _mapper.Map(address, addressEntity);

            await _sharedRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{addressId}")]
        public async Task<ActionResult> PartiallyUpdateAddress(int contactId,
                                                   int addressId,
                                                   JsonPatchDocument<AddressUpdateDto> patchDocument)
        {
              

            if(!await _sharedRepository.ContactExists(contactId))
            {
                return NotFound();
            }

            var addressEntity = await _repository.GetAddress(addressId, contactId);
            if(addressEntity == null)
            {
                return NotFound();
            }

            var addressToPatch = _mapper.Map<AddressUpdateDto>(addressEntity);


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

            _mapper.Map(addressToPatch, addressEntity);
            await _sharedRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{addressId}")]
        //[ValidateAntiForgeryToken] //Idk autocompleted
        public async Task<ActionResult> DeleteAddress(int contactId,
                                          int addressId)
        {
            if(!await _sharedRepository.ContactExists(contactId))
            {
                return NotFound();
            }

            var addressEntity = await _repository.GetAddress(addressId, contactId);
            if (addressEntity == null)
            {
                return NotFound();
            }

            _repository.DeleteAddress(addressEntity);

            await _sharedRepository.SaveChangesAsync();

            
            return NoContent();
        }
    }
}
