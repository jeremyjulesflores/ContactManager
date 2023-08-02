using AutoMapper;
using ContactManager.API.Entities;
using ContactManager.API.Models;
using ContactManager.API.Models.CreationDtos;
using ContactManager.API.Models.UpdateDtos;
using ContactManager.API.Repositories;
using ContactManager.API.Repositories.Shared;
using System.Net;

namespace ContactManager.API.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _repository;
        private readonly ISharedRepository _sharedRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ContactService(IContactRepository repository,
                              ISharedRepository sharedRepository,
                              IUserRepository userRepository,
                              IMapper mapper)
        {
            this._repository = repository;
            this._sharedRepository = sharedRepository;
            this._userRepository = userRepository;
            this._mapper = mapper;
        }
        public async Task<bool> CreateContact(int userId, ContactCreationDto contact)
        {
            var user = await _userRepository.GetUser(userId);
            if(user == null)
            {
                return false;
            }

            var contactToCreate = _mapper.Map<Contact>(contact);
            this._repository.CreateContact(user, contactToCreate);

            return await this._sharedRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteContact(int userId, int contactId)
        {
            if (!await this._sharedRepository.UserExists(userId))
            {
                return false;
            }
            var contactEntity = await this._repository.GetContact(contactId);
            if(contactEntity == null) { return false; }

            this._repository.DeleteContact(contactEntity);

            return await this._sharedRepository.SaveChangesAsync();
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
            if (!await this._sharedRepository.UserExists(userId))
            {
                return false;
            }
            var contactEntity = await this._repository.GetContact(contactId);
            if(contactEntity == null) { return false; }

            this._mapper.Map(contact, contactEntity);

            return await this._sharedRepository.SaveChangesAsync();
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
