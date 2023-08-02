using AutoMapper;
using ContactManager.API.Entities;
using ContactManager.API.Models.CreationDtos;
using ContactManager.API.Models.UpdateDtos;
using ContactManager.API.Models;
using ContactManager.API.Repositories;
using ContactManager.API.Repositories.Shared;

namespace ContactManager.API.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEmailRepository _repository;
        private readonly ISharedRepository _sharedRepository;
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public EmailService(IEmailRepository repository,
                            ISharedRepository sharedRepository,
                            IContactRepository contactRepository,
                            IMapper mapper)
        {
            this._repository = repository;
            this._sharedRepository = sharedRepository;
            this._contactRepository = contactRepository;
            this._mapper = mapper;
        }

        public async Task<bool> CreateEmail(int contactId, EmailCreationDto email)
        {

            var contact = await _contactRepository.GetContact(contactId);
            if (contact == null)
            {
                return false;
            }
            var emailToCreate = _mapper.Map<Email>(email);

            this._repository.CreateEmail(contact, emailToCreate);
            return await this._sharedRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteEmail(int contactId, int emailId)
        {
            if (!await this._sharedRepository.ContactExists(contactId))
            {
                return false;
            }
            var emailEntity = await this._repository.GetEmail(contactId, emailId);
            if (emailEntity == null) { return false; }
            this._repository.DeleteEmail(emailEntity);

            return await this._sharedRepository.SaveChangesAsync();
        }

        public async Task<EmailDto?> GetEmail(int emailId, int contactId)
        {
            if (!await this._sharedRepository.ContactExists(contactId))
            {
                return null;
            }

            var email = await _repository.GetEmail(emailId, contactId);
            if (email == null) { return null; };

            return _mapper.Map<EmailDto>(email);
        }

        public async Task<IEnumerable<EmailDto>> GetEmails(int contactId)
        {
            if (!await _sharedRepository.ContactExists(contactId))
            {
                return null;
            }

            var emails = await _repository.GetEmails(contactId);

            return this._mapper.Map<IEnumerable<EmailDto>>(emails);
        }

        async Task<EmailUpdateDto> IEmailService.GetEmailToPatch(int contactId, int emailId)
        {
            var numberEntity = await _repository.GetEmail(contactId, emailId);

            return _mapper.Map<EmailUpdateDto>(numberEntity);
        }

        async Task<bool> IEmailService.PatchNumber(int contactId, int emailId, EmailUpdateDto email)
        {
            var emailEntity = await _repository.GetEmail(contactId, emailId);
            _mapper.Map(email, emailEntity);

            return await _sharedRepository.SaveChangesAsync();
        }

        async Task<bool> IEmailService.UpdateEmail(int contactId, int emailId, EmailUpdateDto email)
        {
            if (!await _sharedRepository.ContactExists(contactId))
            {
                return false;
            }
            var emailEntity = await _repository.GetEmail(emailId, contactId);
            if (emailEntity == null)
            {
                return false;
            }
            //automapper will override emailEntity with the emails
            _mapper.Map(email, emailEntity);

            return await _sharedRepository.SaveChangesAsync();
        }
    }
}
