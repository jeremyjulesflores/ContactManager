using ContactManager.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<ContactDto>> GetContacts()
        {
            return Ok(ContactsDataStore.Current.Contacts);
        }

        [HttpGet("{id}")]
        public ActionResult<ContactDto> GetContact(int id)
        {
            var contactToReturn = ContactsDataStore.Current.Contacts
                .FirstOrDefault(c => c.Id == id);

            if(contactToReturn == null)
            {
                return NotFound();
            }
            return Ok(contactToReturn);
        }
  
    }
}
