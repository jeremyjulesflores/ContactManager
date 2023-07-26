using ContactManager.API.Models;
using ContactManager.API.Models.CreationDtos;
using ContactManager.API.Models.UpdateDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.API.Controllers
{
    [Route("api/contacts/{contactId}/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<AddressDto>> GetAddresses(int contactId)
        {
            var contact = ContactsDataStore.Current.Contacts.FirstOrDefault(c => c.Id == contactId);

            if(contact == null)
            {
                return NotFound();
            }
            return Ok(contact.Addresses);
        }

        [HttpGet("{addressId}", Name = "GetAddress")]
        public ActionResult<AddressDto> GetAddress(int contactId,
                                                   int addressId)
        {
            var contact = ContactsDataStore.Current.Contacts.FirstOrDefault(c => c.Id == contactId);

            if(contact == null)
            {
                return NotFound();
            }

            var address = contact.Addresses.FirstOrDefault(c => c.Id == addressId);

            if(address == null)
            {
                return NotFound();
            }

            return Ok(address);
        }

        [HttpPost]
        public ActionResult<AddressDto> CreateAddress(int contactId,
                                                      AddressCreationDto address)
        {
            var contact = ContactsDataStore.Current.Contacts.FirstOrDefault(c => c.Id == contactId);
            if(contact == null)
            {
                return NotFound();
            }

            //to be improved
            var maxAddressId = ContactsDataStore.Current.Contacts.SelectMany( c=> c.Addresses)
                                                                            .Max(a=>a.Id);

            var finalAddress = new AddressDto()
            {
                Id = ++maxAddressId,
                Type = address.Type,
                AddressDetails = address.AddressDetails
            };

            contact.Addresses.Add(finalAddress);

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

            if (!TryValidateModel(addressToPatch))
            {
                return BadRequest(ModelState);
            }

            addressFromStore.Type = addressToPatch.Type;
            addressFromStore.AddressDetails = addressToPatch.AddressDetails;

            return NoContent();
        }
    }
}
