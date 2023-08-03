using ContactManager.API.Entities;
using ContactManager.API.Exceptions;
using ContactManager.API.Models;
using ContactManager.API.Models.CreationDtos;
using ContactManager.API.Models.UpdateDtos;
using ContactManager.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;
using System.Security.Claims;

namespace ContactManager.API.Controllers
{
    [Route("api/contacts/{contactId}/[controller]")]
    [ApiController]
    [Authorize]
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
            var user = GetUser();
            var userId = user.Id;
            try
            {
                
                var emails = await _emailService.GetEmails(userId, contactId);

                if (emails == null)
                {
                    return NotFound();
                }

                return Ok(emails);
            }
            catch(UserNotFoundException ex)
            {
                _logger.LogCritical(
                    $"User {userId} Not found while getting emails for contact with id {contactId}.", ex);
                return NotFound("Not Found");
            }
            catch(ContactNotFoundException ex)
            {
                _logger.LogCritical(
                    $"Contact {contactId} Not found while getting emails", ex);
                return NotFound("Not Found");
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
            var user = GetUser();
            var userId = user.Id;
            try
            {
                var email = await _emailService.GetEmail(userId, emailId: emailId, contactId: contactId);
                return Ok(email);
            }
            catch(UserNotFoundException ex)
            {
                _logger.LogCritical(
                    $"User {userId} Not found while getting email for contact with id {contactId}.", ex);
                return NotFound("Not Found");
            }
            catch(ContactNotFoundException ex)
            {
                _logger.LogCritical($"Contact {contactId} was not found while getting email", ex);
                return NotFound("Not Found");
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"An Exception happened while getting email", ex);
                return StatusCode(500, "Something went wrong");
            }
            
        }

        [HttpPost]
        public async Task<ActionResult<EmailDto>> CreateEmailAsync(int contactId,
                                                      EmailCreationDto email)
        {
            var user = GetUser();
            var userId = user.Id;
            try
            {
                await this._emailService.CreateEmail(userId, contactId, email);

                return Ok("Email Successfully Created");
            }
            catch (UserNotFoundException ex)
            {
                _logger.LogCritical(
                    $"User {userId} Not found while Creating email for contact with id {contactId}.", ex);
                return NotFound("Not Found");
            }
            catch (ContactNotFoundException ex)
            {
                _logger.LogCritical(
                    $"Contact {contactId} Not found while Creating.", ex);
                return NotFound("Not Found");
            }
            catch(Exception ex)
            {
                _logger.LogCritical(
                    $"An Exception Happened while Creating email for contact with id {contactId}", ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{emailId}")]
        public async Task<ActionResult> UpdateEmailAsync(int contactId,
                                          int emailId,
                                          EmailUpdateDto email)
        {
            var user = GetUser();
            var userId = user.Id;
            try
            {
                await _emailService.UpdateEmail(userId, contactId, emailId, email);
                return Ok("Email Updated");
            }
            catch(UserNotFoundException ex)
            {
                _logger.LogCritical($"User {userId} was not found while updating email from contact with id" +
                    $" {contactId}.", ex);
                return NotFound("Not Found");
            }
            catch(ContactNotFoundException ex)
            {
                _logger.LogCritical($"Contact {contactId} was not found while updating email", ex);
                return NotFound("Not Found");
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"An Exception happened while updating email.", ex);
                return StatusCode(500, "Something went wrong");
            }
            
        }

        [HttpPatch("{emailId}")]
        public async Task<ActionResult> PartiallyUpdateEmail(int contactId,
                                                   int emailId,
                                                   JsonPatchDocument<EmailUpdateDto> patchDocument)
        {
            var user = GetUser();
            var userId = user.Id;
            try
            {
                var emailToPatch = await _emailService.GetEmailToPatch(userId, contactId, emailId);


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

                await _emailService.PatchNumber(userId, contactId, emailId, emailToPatch);
            

                return Ok("Email Successfully Updated");
            }
            catch(UserNotFoundException ex)
            {
                _logger.LogCritical($"User {userId} was not found when Patching Email from contact with Id {contactId} ", ex);
                return NotFound("Not Found");
            }
            catch(ContactNotFoundException ex)
            {
                _logger.LogCritical($"Contact {contactId} was not found while Patching email", ex);
                return NotFound("Not Found");
            }
            catch(Exception ex)
            {
                _logger.LogCritical("An Exception happened while Patching email", ex);
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpDelete("{emailId}")]
        //[ValidateAntiForgeryToken] //Idk autocompleted
        public async Task<ActionResult> DeleteEmail(int contactId,
                                          int emailId)
        {
            var user = GetUser();
            var userId = user.Id;
            try
            {
                await _emailService.DeleteEmail(userId, contactId, emailId);
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
