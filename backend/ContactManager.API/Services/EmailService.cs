using AutoMapper;
using ContactManager.API.Entities;
using ContactManager.API.Models.CreationDtos;
using ContactManager.API.Models.UpdateDtos;
using ContactManager.API.Models;
using ContactManager.API.Repositories;
using ContactManager.API.Repositories.Shared;
using ContactManager.API.Exceptions;
using ContactManager.API.Repositories.AuditLogsRepository;
using ContactManager.API.Services.AuditLogsServices;
using System.Net;
using ContactManager.API.Helper;

namespace ContactManager.API.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEmailRepository _repository;
        private readonly ISharedRepository _sharedRepository;
        private readonly IContactRepository _contactRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IContactLogsService _contactLogsService;

        public EmailService(IEmailRepository repository,
                            ISharedRepository sharedRepository,
                            IContactRepository contactRepository,
                            IUserRepository userRepository,
                            IMapper mapper,
                            IContactLogsService contactLogsService)
        {
            this._repository = repository;
            this._sharedRepository = sharedRepository;
            this._contactRepository = contactRepository;
            this._userRepository = userRepository;
            this._mapper = mapper;
            this._contactLogsService = contactLogsService;
        }

        public async Task CreateEmail(int userId, int contactId, EmailCreationDto email)
        {
            var user = await _userRepository.GetUser(userId);
            if(user == null)
            {
                throw new UserNotFoundException("User not Found");
            }
            var contact = await _contactRepository.GetContact(userId, contactId);
            if (contact == null)
            {
                throw new ContactNotFoundException("Contact not Found");
            }
            var emailToCreate = _mapper.Map<Email>(email);

            this._repository.CreateEmail(contact, emailToCreate);
            if(! await this._sharedRepository.SaveChangesAsync())
            {
                throw new Exception("Failed to Create");
            }
            _contactLogsService.CreateLog("Create", user.Username,
                                           $"{contactId} : {contact.FirstName} {contact.LastName}",
            $"Created {email.Type} email {emailToCreate.Id} : {email.EmailAddress}");
        }

        public async Task DeleteEmail(int userId, int contactId, int emailId)
        {
            var user = await _userRepository.GetUser(userId);
            if(user == null)
            {
                throw new UserNotFoundException("User not Found");
            }
            var contact = await _contactRepository.GetContact(userId, contactId);
            if (contact == null )
            {
                throw new ContactNotFoundException("Contact not Found");
            }

            var emailEntity = await this._repository.GetEmail(contactId, emailId);
            if (emailEntity == null) { throw new ArgumentNullException("Email not Found"); }

            this._repository.DeleteEmail(emailEntity);

            if (!await this._sharedRepository.SaveChangesAsync())
            {
                throw new Exception("Failed Deleting Address");
            }
            _contactLogsService.CreateLog("Delete", user.Username,
                                         $"{contact.Id} : {contact.FirstName} {contact.LastName}",
                                         $"Deleted {emailEntity.Type} email {emailId} : {emailEntity.EmailAddress}");
        }

        public async Task<EmailDto?> GetEmail(int userId, int contactId, int emailId)
        {
            if (!await this._sharedRepository.UserExists(userId))
            {
                throw new UserNotFoundException("User Not Found");
            }
            if (!await this._sharedRepository.ContactExists(userId, contactId))
            {
                throw new ContactNotFoundException("Contact Not Found");
            }

            var email = await _repository.GetEmail(contactId, emailId);
            //may be exception here
            return _mapper.Map<EmailDto>(email);
        }

        public async Task<IEnumerable<EmailDto>> GetEmails(int userId, int contactId)
        {
            if(!await _sharedRepository.UserExists(userId))
            {
                throw new UserNotFoundException("User not found");
            }
            if (!await _sharedRepository.ContactExists(userId, contactId))
            {
                throw new ContactNotFoundException("Contact not Found");
            }

            var emails = await _repository.GetEmails(contactId);
            //may be exception here
            return this._mapper.Map<IEnumerable<EmailDto>>(emails);
        }

        async Task<EmailUpdateDto> IEmailService.GetEmailToPatch(int userId, int contactId, int emailId)
        {
            if(! await _sharedRepository.UserExists(userId))
            {
                throw new UserNotFoundException("User not Found");
            }
            if(! await _sharedRepository.ContactExists(userId, contactId))
            {
                throw new ContactNotFoundException("Contact not Found");
            }
            var numberEntity = await _repository.GetEmail(contactId, emailId);

            return _mapper.Map<EmailUpdateDto>(numberEntity);
        }

        async Task IEmailService.PatchNumber(int userId, int contactId, int emailId, EmailUpdateDto email)
        {
            var user = await _userRepository.GetUser(userId);
            if (user == null)
            {
                throw new UserNotFoundException("User not found");
            }
            var contact = await _contactRepository.GetContact(userId, contactId);
            if (contact == null)
            {
                throw new ContactNotFoundException("Contact not found");
            }
            var emailEntity = await _repository.GetEmail(contactId, emailId);
            string details = String.Empty;
            var difference = GetObjectDifference.GetObjectDifferences<Email, EmailUpdateDto>(emailEntity, email);
            foreach(var differenceEntity in difference)
            {
                details += $"{differenceEntity.PropertyName} : From : {differenceEntity.OriginalValue} -> {differenceEntity.ChangedValue};";
            }
            _mapper.Map(email, emailEntity);

            if(! await _sharedRepository.SaveChangesAsync())
            {
                throw new Exception("Failed to Patch Email");
            }

            _contactLogsService.CreateLog("Patch", user.Username,
                                        $"{contact.Id} : {contact.FirstName} {contact.LastName}",
                                        $"Email {emailEntity.Id} Patched " + details);
        }

        async Task IEmailService.UpdateEmail(int userId, int contactId, int emailId, EmailUpdateDto email)
        {

            var user = await _userRepository.GetUser(userId);
            if(user == null)
            {
                throw new UserNotFoundException("User not found");
            }
            var contact = await _contactRepository.GetContact(userId, contactId);
            if(contact == null)
            {
                throw new ContactNotFoundException("Contact not found"); 
            }
            var emailEntity = await _repository.GetEmail(contactId, emailId);

            string details = String.Empty;
            var difference = GetObjectDifference.GetObjectDifferences<Email, EmailUpdateDto>(emailEntity, email);
            foreach (var differenceEntity in difference)
            {
                details += $"[{differenceEntity.PropertyName} : From: {differenceEntity.OriginalValue} -> {differenceEntity.ChangedValue}];";
            }
            //automapper will override emailEntity with the emails
            _mapper.Map(email, emailEntity);
            _contactLogsService.CreateLog("Update", user.Username,
                                        $"{contact.Id} : {contact.FirstName} {contact.LastName}",
                                        $"Email {emailEntity.Id} Updated " + details);

            await _sharedRepository.SaveChangesAsync();
        }
    }
}
