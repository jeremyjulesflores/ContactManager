using ContactManager.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.API.Controllers
{
    [Route("api/contacts/{contactId}/[controller]")]
    [ApiController]
    public class EmailsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<EmailDto>> GetEmails(int contactId)
        {
            var contact = ContactsDataStore.Current.Contacts.FirstOrDefault(c => c.Id == contactId);

            if(contact == null)
            {
                return NotFound();
            }
            return Ok(contact.Emails);
        }

        [HttpGet("{emailId}")]
        public ActionResult<EmailDto> GetAddress(int contactId,
                                                   int emailId)
        {
            var contact = ContactsDataStore.Current.Contacts.FirstOrDefault(c => c.Id == contactId);

            if(contact == null)
            {
                return NotFound();
            }

            var email = contact.Emails.FirstOrDefault(c => c.Id == emailId);

            if(email == null)
            {
                return NotFound();
            }

            return Ok(email);
        }
    }
}
