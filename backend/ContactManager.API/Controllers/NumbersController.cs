using AutoMapper;
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
    public class NumbersController : ControllerBase
    {
        private readonly INumberService _numberService;
        private readonly ILogger<NumbersController> _logger;

        public NumbersController(INumberService numberService,
                                 ILogger<NumbersController> logger)
        {
            this._numberService = numberService;
            this._logger = logger;  
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NumberDto>>> GetNumbersAsync(int contactId)
        {
            var user = GetUser();
            var userId = user.Id;
            try
            {
                var numbers = await _numberService.GetNumbers(userId, contactId);
                return Ok(numbers);
            }
            catch(UserNotFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound("Not Found");
            }
            catch(ContactNotFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound("Not Found");
            }
            catch(Exception ex)
            {
                _logger.LogCritical(
                    $"Exception while getting Numbers for contact with id {contactId}.", ex);
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpGet("{numberId}")]
        public async Task<ActionResult<AddressDto>> GetNumberAsync(int contactId,
                                                                    int numberId)
        {
            var user = GetUser();
            var userId = user.Id;
            try
            {
                var number = await _numberService.GetNumber(userId, contactId, numberId);
                return Ok(number);
            }
            catch (UserNotFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound("Not Found");
            }
            catch (ContactNotFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound("Not Found");
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound("Not Found");
            }
            catch (Exception ex)
            {
                _logger.LogCritical(
                    $"Exception while getting Numbers for contact with id {contactId}.", ex);
                return StatusCode(500, "Something went wrong");
            }

        }

        [HttpPost]
        public async Task<ActionResult<NumberDto>> CreateNumberAsync(int contactId,
                                                      NumberCreationDto number)
        {
            var user = GetUser();
            var userId = user.Id;
            try
            {
                await this._numberService.CreateNumber(userId, contactId, number);

                return Ok("Number Successfully Created");
            }
            catch(UserNotFoundException ex)
            {
                _logger.LogCritical($"User {userId} was not found while creating a number", ex);
                return NotFound("Not Found");
            }
            catch(ContactNotFoundException ex)
            {
                _logger.LogCritical($"Contact {contactId} was not found while creating a number", ex);
                return NotFound("Not Found");
            }
            catch(Exception ex)
            {
                _logger.LogCritical("An Exception happened while creating a number", ex);
                return StatusCode(500, "Something went wrong");
            }
            
        }

        [HttpDelete("{numberId}")]
        //[ValidateAntiForgeryToken] //Idk autocompleted
        public async Task<ActionResult> DeleteNumberAsync(int contactId,
                                                      int numberId)
        {
            var user = GetUser();
            var userId = user.Id;
            try
            {
                await _numberService.DeleteNumber(userId, contactId, numberId);
                return Ok("Delete Successful");
            }
            catch (UserNotFoundException ex)
            {
                _logger.LogCritical($"User {userId} was not found while creating a number", ex);
                return NotFound("Not Found");
            }
            catch (ContactNotFoundException ex)
            {
                _logger.LogCritical($"Contact {contactId} was not found while creating a number", ex);
                return NotFound("Not Found");
            }
            catch(ArgumentNullException ex)
            {
                _logger.LogCritical(ex.Message, ex);
                return NotFound("Not Found");
            }
            catch (Exception ex)
            {
                _logger.LogCritical("An Exception happened while creating a number", ex);
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpPut("{numberId}")]
        public async Task<ActionResult> UpdateNumberAsync(int contactId,
                                          int numberId,
                                          NumberUpdateDto number)
        {
            var user = GetUser();
            var userId = user.Id;
            try
            {
                await _numberService.UpdateNumber(userId, contactId, numberId, number);
                return Ok("Address Updated");
            }
            catch (UserNotFoundException ex)
            {
                _logger.LogCritical($"User {userId} was not found while creating a number", ex);
                return NotFound("Not Found");
            }
            catch (ContactNotFoundException ex)
            {
                _logger.LogCritical($"Contact {contactId} was not found while creating a number", ex);
                return NotFound("Not Found");
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogCritical(ex.Message, ex);
                return NotFound("Not Found");
            }
            catch (Exception ex)
            {
                _logger.LogCritical("An Exception happened while creating a number", ex);
                return StatusCode(500, "Something went wrong");
            }



        }

        [HttpPatch("{numberId}")]
        public async Task<ActionResult> PartiallyUpdateAddress(int contactId,
                                                               int numberId,
                                                               JsonPatchDocument<NumberUpdateDto> patchDocument)

        {
            var user = GetUser();
            var userId = user.Id;
            try
            {
                var numberToPatch = await _numberService.GetNumberToPatch(userId, contactId, numberId);
                if (numberToPatch == null)
                {
                    return NotFound();
                }
                patchDocument.ApplyTo(numberToPatch, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                //Check if Model is correct
                //If Request is invalid it will return false and return a Bad Requet
                if (!TryValidateModel(numberToPatch))
                {
                    return BadRequest(ModelState);
                }

                await _numberService.PatchNumber(userId, contactId, numberId, numberToPatch);
                return Ok("Number Successfully Updated");
            }
            catch (UserNotFoundException ex)
            {
                _logger.LogCritical($"User {userId} was not found while creating a number", ex);
                return NotFound("Not Found");
            }
            catch (ContactNotFoundException ex)
            {
                _logger.LogCritical($"Contact {contactId} was not found while creating a number", ex);
                return NotFound("Not Found");
            }
            catch (Exception ex)
            {
                _logger.LogCritical("An Exception happened while creating a number", ex);
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
