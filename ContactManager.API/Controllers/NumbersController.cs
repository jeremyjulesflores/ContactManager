using ContactManager.API.Models;
using ContactManager.API.Models.CreationDtos;
using ContactManager.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.API.Controllers
{
    [Route("api/contacts/{contactId}/[controller]")]
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
            try
            {
                var numbers = await _numberService.GetNumbers(contactId);

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
            var number = await _numberService.GetNumber(contactId, numberId);

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
            var created = await this._numberService.CreateNumber(contactId, number);

            if (!created)
            {
                return BadRequest();
            }

            return Ok("Number Successfully Created");
        }
    }
}
