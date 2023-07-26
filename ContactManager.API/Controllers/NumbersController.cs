using ContactManager.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.API.Controllers
{
    [Route("api/contacts/{contactId}/[controller]")]
    [ApiController]
    public class NumbersController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<NumberDto>> GetNumbers(int contactId)
        {
            var contact = ContactsDataStore.Current.Contacts.FirstOrDefault(c => c.Id == contactId);

            if(contact == null)
            {
                return NotFound();
            }
            return Ok(contact.Numbers);
        }

        [HttpGet("{numberId}")]
        public ActionResult<NumberDto> GetAddress(int contactId,
                                                   int numberId)
        {
            var contact = ContactsDataStore.Current.Contacts.FirstOrDefault(c => c.Id == contactId);

            if(contact == null)
            {
                return NotFound();
            }

            var number = contact.Numbers.FirstOrDefault(c => c.Id == numberId);

            if(number == null)
            {
                return NotFound();
            }

            return Ok(number);
        }
    }
}
