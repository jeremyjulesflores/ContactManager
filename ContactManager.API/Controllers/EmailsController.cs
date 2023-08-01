using ContactManager.API.Models;
using ContactManager.API.Models.CreationDtos;
using ContactManager.API.Models.UpdateDtos;
using ContactManager.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.API.Controllers
{
    [Route("api/contacts/{contactId}/[controller]")]
    [ApiController]
    public class EmailsController : ControllerBase
    {
        private readonly ILogger<EmailsController> _logger;
        private readonly IEmailService _emailService;

        public EmailsController(ILogger<EmailsController> logger,
                                IEmailService emailService)
        {
            this._logger = logger;
            this._emailService = emailService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmailDto>>> GetEmailsAsync(int contactId)
        {
            try
            {
                var emails = await _emailService.GetEmails(contactId);

                if (emails == null)
                {
                    return NotFound();
                }

                return Ok(emails);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(
                    $"Exception while getting Emails for contact with id {contactId}.", ex);
                return StatusCode(500, "Something went wrong");
            }

        }

        [HttpGet("{emailId}")]
        public async Task<ActionResult<EmailDto>> GetEmailAsync(int contactId,
                                                   int emailId)
        {
            var email = await _emailService.GetEmail(emailId: emailId, contactId: contactId);

            if (email == null)
            {
                return NotFound();
            }

            return Ok(email);
        }

        [HttpPost]
        public async Task<ActionResult<EmailDto>> CreateEmailAsync(int contactId,
                                                      EmailCreationDto email)
        {
            var created = await this._emailService.CreateEmail(contactId, email);

            if (!created)
            {
                return BadRequest();
            }

            return Ok("Email Successfully Created");
        }

        [HttpPut("{emailId}")]
        public async Task<ActionResult> UpdateEmailAsync(int contactId,
                                          int emailId,
                                          EmailUpdateDto email)
        {
            var updated = await _emailService.UpdateEmail(contactId, emailId, email);
            if (!updated)
            {
                return NotFound();
            }

            return Ok("Email Updated");
        }

        [HttpPatch("{emailId}")]
        public async Task<ActionResult> PartiallyUpdateEmail(int contactId,
                                                   int emailId,
                                                   JsonPatchDocument<EmailUpdateDto> patchDocument)
        {
            var emailToPatch = await _emailService.GetEmailToPatch(contactId, emailId);
            if (emailToPatch == null)
            {
                return NotFound();
            }


            patchDocument.ApplyTo(emailToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Check if Model is correct
            //If Request is invalid it will return false and return a Bad Requet
            if (!TryValidateModel(emailToPatch))
            {
                return BadRequest(ModelState);
            }

            if (!await _emailService.PatchNumber(contactId, emailId, emailToPatch))
            {
                return BadRequest(ModelState);
            }

            return Ok("Email Successfully Updated");
        }

        [HttpDelete("{emailId}")]
        //[ValidateAntiForgeryToken] //Idk autocompleted
        public async Task<ActionResult> DeleteEmail(int contactId,
                                          int emailId)
        {
            var deleted = await _emailService.DeleteEmail(contactId, emailId);

            if (!deleted)
            {
                return NotFound();
            }
            return Ok("Delete Successful");
        }
    }
}
