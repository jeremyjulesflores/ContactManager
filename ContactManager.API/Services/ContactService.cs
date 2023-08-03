using AutoMapper;
using ContactManager.API.Entities;
using ContactManager.API.Exceptions;
using ContactManager.API.Helper;
using ContactManager.API.Models;
using ContactManager.API.Models.CreationDtos;
using ContactManager.API.Models.UpdateDtos;
using ContactManager.API.Repositories;
using ContactManager.API.Repositories.Shared;
using ContactManager.API.Services.AuditLogsServices;
using System.Net;

namespace ContactManager.API.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _repository;
        private readonly ISharedRepository _sharedRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IContactLogsService _contactLogsService;

        public ContactService(IContactRepository repository,
                              ISharedRepository sharedRepository,
                              IUserRepository userRepository,
                              IMapper mapper,
                              IContactLogsService contactLogsService)
        {
            this._repository = repository;
            this._sharedRepository = sharedRepository;
            this._userRepository = userRepository;
            this._mapper = mapper;
            this._contactLogsService = contactLogsService;
        }
        public async Task CreateContact(int userId, ContactCreationDto contact)
        {
            var user = await _userRepository.GetUser(userId);
            if(user == null)
            {
                throw new UserNotFoundException("User not found");
            }

            var contactToCreate = _mapper.Map<Contact>(contact);
            this._repository.CreateContact(user, contactToCreate);

            if(!await this._sharedRepository.SaveChangesAsync())
            {
                throw new Exception("Creation Failed.");
            }
            _contactLogsService.CreateLog("Create", user.Username, $"{user.Id} : {contactToCreate.FirstName} {contactToCreate.LastName}", "Contact Created");
           
        }                            

        public async Task DeleteContact(int userId, int contactId)
        {
            var user = await _userRepository.GetUser(userId);
            if (user == null)
            {
                throw new UserNotFoundException("User not found");
            }
            var contactEntity = await this._repository.GetContact(userId, contactId);
            if(contactEntity == null) 
            {
                throw new ContactNotFoundException("Contact not found");
            }

            this._repository.DeleteContact(contactEntity);

            if(!await this._sharedRepository.SaveChangesAsync())
            {
                throw new Exception("Failed to Delete Contact");
            }

            
            _contactLogsService.CreateLog("Delete", user.Username, $"{contactEntity.Id} : {contactEntity.FirstName} {contactEntity.LastName}", "Contact Deleted");
        }
            
        public async Task<ContactDto?> GetContact(int userId, int contactId)
        {
            var user = await _userRepository.GetUser(userId);
            if (user == null)
            {
                throw new UserNotFoundException("User Not Found.");
            }

            var contact = await _repository.GetContact(userId, contactId);
            if (contact == null)
            {
                throw new ContactNotFoundException("Contact Not Found");
            }

            return this._mapper.Map<ContactDto>(contact);
        }

        public async Task<IEnumerable<ContactWithoutDetailsDto>> GetContacts(int userId)
        {
            var user = await _userRepository.GetUser(userId);
            if (user == null)
            {
                throw new UserNotFoundException("User Not Found.");
            }

            var contacts = await _repository.GetContacts(userId);
            return this._mapper.Map<IEnumerable<ContactWithoutDetailsDto>>(contacts);
        }

        public async Task UpdateContact(int userId, 
                                              int contactId, 
                                              ContactUpdateDto contact)
        {
            var user = await _userRepository.GetUser(userId);
            if (user == null)
            {
                throw new UserNotFoundException("User not found");
            }
            var contactEntity = await this._repository.GetContact(userId, contactId);
            if(contactEntity == null) 
            {
                throw new ContactNotFoundException("Contact not found"); 
            }
            string details = String.Empty;
            var difference = GetObjectDifference.GetObjectDifferences<Contact, ContactUpdateDto>(contactEntity, contact);
            foreach (var differenceEntity in difference)
            {
                details += $"{differenceEntity.PropertyName} : From: {differenceEntity.OriginalValue} -> {differenceEntity.ChangedValue};\n";
            }
            this._mapper.Map(contact, contactEntity);

            if(!await this._sharedRepository.SaveChangesAsync())
            {
                throw new Exception("Failed to Update contact");
            }
            _contactLogsService.CreateLog("Update", user.Username, $"{contactEntity.FirstName} {contactEntity.LastName}", "Contact Updated \n" + details);
      
        }

        async Task<ContactUpdateDto> IContactService.GetContactToPatch(int userId, int contactId)
        {
            var user = await _userRepository.GetUser(userId);
            if (user == null)
            {
                throw new UserNotFoundException("User Not Found.");
            }
            var contactEntity = await _repository.GetContact(userId, contactId);
            if (contactEntity == null)
            {
                throw new ContactNotFoundException("Contact Not Found");
            }

            return _mapper.Map<ContactUpdateDto>(contactEntity);
        }

        async Task IContactService.PatchContact(int userId, int contactId, ContactUpdateDto contact)
        {
            var user = await _userRepository.GetUser(userId);
            if (user == null)
            {
                throw new UserNotFoundException("User Not Found.");
            }
            var contactEntity = await _repository.GetContact(userId, contactId);
            if (contactEntity == null)
            {
                throw new ContactNotFoundException("Contact Not Found");
            }

            string details = String.Empty;
            var difference = GetObjectDifference.GetObjectDifferences<Contact, ContactUpdateDto>(contactEntity, contact);
            foreach (var differenceEntity in difference)
            {
                details += $"{differenceEntity.PropertyName} : From: {differenceEntity.OriginalValue} -> {differenceEntity.ChangedValue};\n";
            }
            _mapper.Map(contact, contactEntity);

            if (!await _sharedRepository.SaveChangesAsync())
            {
                throw new Exception("Failed Updating Address");
            };

            _contactLogsService.CreateLog("Update", user.Username, $"{contactEntity.Id} : {contactEntity.FirstName} {contactEntity.LastName}", $"Contact {contactEntity.Id} Patched \n" + details);
        }
    }
}
