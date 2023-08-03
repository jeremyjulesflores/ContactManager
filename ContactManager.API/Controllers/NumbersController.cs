using AutoMapper;
using ContactManager.API.Entities;
using ContactManager.API.Models;
using ContactManager.API.Models.CreationDtos;
using ContactManager.API.Models.UpdateDtos;
using ContactManager.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ContactManager.API.Controllers
{
    [Route("api/users/{userId}/contacts/{contactId}/[controller]")]
    [ApiController]
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

                if(numbers == null)
                {
                    return NotFound();
                }

                return Ok(numbers);
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
            var number = await _numberService.GetNumber(userId, contactId, numberId);

            if (number == null)
            {
                return NotFound();
            }

            return Ok(number);
        }

        [HttpPost]
        public async Task<ActionResult<NumberDto>> CreateNumberAsync(int contactId,
                                                      NumberCreationDto number)
        {
            var user = GetUser();
            var userId = user.Id;
            var created = await this._numberService.CreateNumber(userId, contactId, number);

            if (!created)
            {
                return BadRequest();
            }

            return Ok("Number Successfully Created");
        }

        [HttpDelete("{numberId}")]
        //[ValidateAntiForgeryToken] //Idk autocompleted
        public async Task<ActionResult> DeleteNumberAsync(int contactId,
                                                      int numberId)
        {
            var user = GetUser();
            var userId = user.Id;
            var deleted = await _numberService.DeleteNumber(userId, contactId, numberId);

            if (!deleted)
            {
                return NotFound();
            }
            return Ok("Delete Successful");
        }

        [HttpPut("{numberId}")]
        public async Task<ActionResult> UpdateNumberAsync(int contactId,
                                          int numberId,
                                          NumberUpdateDto number)
        {
            var user = GetUser();
            var userId = user.Id;
            var updated = await _numberService.UpdateNumber(userId, contactId, numberId, number);
            if (!updated)
            {
                return NotFound();
            }

            return Ok("Address Updated");
        }

        [HttpPatch("{numberId}")]
        public async Task<ActionResult> PartiallyUpdateAddress(int contactId,
                                                               int numberId,
                                                               JsonPatchDocument<NumberUpdateDto> patchDocument)

        {
            var user = GetUser();
            var userId = user.Id;
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

            if(!await _numberService.PatchNumber(userId, contactId, numberId, numberToPatch))
            {
                return BadRequest(ModelState);
            }

            return Ok("Number Successfully Updated");
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
