using AutoMapper;
using ContactManager.API.Entities;
using ContactManager.API.Exceptions;
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
        public async Task<bool> CreateContact(int userId, ContactCreationDto contact)
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
                return false;
            }

            _contactLogsService.CreateLog("Create", user.Username, $"{contactToCreate.FirstName} {contactToCreate.LastName}", "Contact Created");
            return true;
        }

        public async Task<bool> DeleteContact(int userId, int contactId)
        {
            var user = await _userRepository.GetUser(userId);
            if (user == null)
            {
                throw new UserNotFoundException("User not found");
            }
            var contactEntity = await this._repository.GetContact(contactId);
            if(contactEntity == null) { return false; }

            this._repository.DeleteContact(contactEntity);

            if(!await this._sharedRepository.SaveChangesAsync())
            {
                return false;
            }

            _contactLogsService.CreateLog("Delete", user.Username, $"{contactEntity.FirstName} {contactEntity.LastName}", "Contact Deleted");
            return true;
        }
            
         
        public async Task<ContactDto?> GetContact(int userId, int contactId)
        {
            if(!await this._sharedRepository.UserExists(userId))
            {
                return null;
            }

            var contact = await this._repository.GetContact(contactId);

            if(contact == null) { return null;}

            return this._mapper.Map<ContactDto>(contact);
        }

        public async Task<IEnumerable<ContactWithoutDetailsDto>> GetContacts(int userId)
        {
            if (!await this._sharedRepository.UserExists(userId))
            {
                return null;
            }

            var contacts = await _repository.GetContacts(userId);

            return this._mapper.Map<IEnumerable<ContactWithoutDetailsDto>>(contacts);
        }

        public async Task<bool> UpdateContact(int userId, 
                                              int contactId, 
                                              ContactUpdateDto contact)
        {
            var user = await _userRepository.GetUser(userId);
            if (user == null)
            {
                throw new UserNotFoundException("User not found");
            }
            var contactEntity = await this._repository.GetContact(contactId);
            if(contactEntity == null) { return false; }

            this._mapper.Map(contact, contactEntity);

            if(!await this._sharedRepository.SaveChangesAsync())
            {
                return false;
            }
            _contactLogsService.CreateLog("Update", user.Username, $"{contactEntity.FirstName} {contactEntity.LastName}", "Contact Updated");
            return true;
        }

        async Task<ContactUpdateDto> IContactService.GetContactToPatch(int userId, int contactId)
        {
            var contactEntity = await _repository.GetContact(contactId);

            return _mapper.Map<ContactUpdateDto>(contactEntity);
        }

        async Task<bool> IContactService.PatchContact(int userId, int contactId, ContactUpdateDto contact)
        {
            var contactEntity = await _repository.GetContact(contactId);
            _mapper.Map(contact, contactEntity);

            return await _sharedRepository.SaveChangesAsync();
        }
    }
}
