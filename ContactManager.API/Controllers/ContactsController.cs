using AutoMapper;
using ContactManager.API.Models;
using ContactManager.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactRepository _repository;
        private readonly IMapper _mapper;

        public ContactsController(IContactRepository repository,
                                  IMapper mapper)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this._mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactDto>>> GetContactsAsync()
        {
            var contacts = await _repository.GetContacts();
            if (contacts == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<ContactWithoutDetailsDto>>(contacts));
        }

        [HttpGet("{contactId}")]
        public async Task<IActionResult> GetContactAsync(int contactId, 
                                                   bool includeContactDetails = true)
        {
            var contact = await _repository.GetContact(contactId, includeContactDetails);
            if (contact == null)
            {
                return NotFound();
            }

            if (includeContactDetails)
            {
                return Ok(_mapper.Map<ContactDto>(contact));
            }
            return Ok(_mapper.Map<ContactWithoutDetailsDto>(contact));
        }
  
    }
}
