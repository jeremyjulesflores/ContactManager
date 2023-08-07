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

namespace ContactManager.API.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _repository;
        private readonly ISharedRepository _sharedRepository;
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;
        private readonly IContactLogsService _contactLogsService;
        private readonly IUserRepository _userRepository;

        public AddressService(IAddressRepository repository,
                              ISharedRepository sharedRepository,
                              IContactRepository contactRepository,
                              IMapper mapper,
                              IContactLogsService contactLogsService,
                              IUserRepository userRepository)
        {
            this._repository = repository;
            this._sharedRepository = sharedRepository;
            this._mapper = mapper;
            this._contactLogsService = contactLogsService;
            this._userRepository = userRepository;
            this._contactRepository = contactRepository;
        }
        public async Task CreateAddress(int userId, int contactId, AddressCreationDto address)
        {
            var user = await _userRepository.GetUser(userId);
            if(user == null)
            {
                throw new UserNotFoundException("User Not Found");
            }
            
            var contact = await _contactRepository.GetContact(userId, contactId);
            if(contact == null)
            {
                throw new ContactNotFoundException("Contact Not Found");
            }
            var addressToCreate = _mapper.Map<Address>(address);

            this._repository.CreateAddress(contact, addressToCreate);
            if(!await this._sharedRepository.SaveChangesAsync())
            {
                throw new Exception("Failed Creating Address");
            }
            _contactLogsService.CreateLog("Create", user.Username,
                                            $"{contactId} : {contact.FirstName} {contact.LastName}",
                                            $"Created {address.Type} address : {address.AddressDetails}");
        }

        public async Task DeleteAddress(int userId, int contactId, int addressId)
        {
            var user = await _userRepository.GetUser(userId);
            if (user == null)
            {
                throw new UserNotFoundException("User Not Found");
            }
            var contact = await _contactRepository.GetContact(userId, contactId);
            if (contact == null)
            {
                throw new ContactNotFoundException("Contact Not Found");
            }
            var addressEntity = await this._repository.GetAddress(contactId, addressId);
            //to use in audit logger
            var address = _mapper.Map<AddressDto>(addressEntity);
            if (addressEntity == null)
            {
                throw new ArgumentNullException(nameof(addressEntity));
            }
            this._repository.DeleteAddress(addressEntity);

            if (!await this._sharedRepository.SaveChangesAsync())
            {
                throw new Exception("Failed Deleting Address");
            }
            _contactLogsService.CreateLog("Delete", user.Username,
                                          $"{contact.Id} : {contact.FirstName} {contact.LastName}",
                                          $"Deleted {addressEntity.Type} address {addressEntity.Id} : {address.AddressDetails}");
        }

        public async Task<AddressDto?> GetAddress(int userId, int contactId, int addressId)
        {
            if(!await this._sharedRepository.UserExists(userId))
            {
                throw new UserNotFoundException("User Not Found");
            }
            if (!await this._sharedRepository.ContactExists(userId, contactId))
            {
                throw new ContactNotFoundException("Contact Not Found");
            }

            var address = await _repository.GetAddress(contactId, addressId);
            return _mapper.Map<AddressDto>(address);
        }

        public async Task<IEnumerable<AddressDto>> GetAddresses(int userId, int contactId)
        {
            if (!await this._sharedRepository.UserExists(userId))
            {
                throw new UserNotFoundException("User Not Found");
            }
            if (!await _sharedRepository.ContactExists(userId, contactId))
            {
                throw new ContactNotFoundException("Contact Not Found");
            }

            var addresses = await _repository.GetAddresses(contactId);

            return this._mapper.Map<IEnumerable<AddressDto>>(addresses);
        }

        async Task<AddressUpdateDto> IAddressService.GetAddressToPatch(int userId, int contactId, int addressId)
        {
            if (!await this._sharedRepository.UserExists(userId))
            {
                throw new UserNotFoundException("User Not Found");
            }
            if (!await _sharedRepository.ContactExists(userId,contactId))
            {
                throw new ContactNotFoundException("Contact Not Found");
            }
            var addressEntity = await _repository.GetAddress(contactId, addressId);

            return _mapper.Map<AddressUpdateDto>(addressEntity);
        }

        async Task IAddressService.PatchAddress(int userId, int contactId, int addressId, AddressUpdateDto address)
        {
            var user = await _userRepository.GetUser(userId);
            if(user == null)
            {
                throw new UserNotFoundException("User Not Found.");
            }
            var contact = await _contactRepository.GetContact(userId, contactId);
            if (contact == null)
            {
                throw new ContactNotFoundException("Contact Not Found");
            }
            var addressEntity = await _repository.GetAddress(contactId, addressId);
            if (addressEntity == null)
            {
                throw new ArgumentNullException("Address not found.");
            }

            string details = String.Empty;
            var difference = GetObjectDifference.GetObjectDifferences<Address, AddressUpdateDto>(addressEntity, address);
            foreach (var differenceEntity in difference)
            {
                details += $"{differenceEntity.PropertyName} : From: {differenceEntity.OriginalValue} -> {differenceEntity.ChangedValue};\n";
            }
            _mapper.Map(address, addressEntity);

            if(!await _sharedRepository.SaveChangesAsync())
            {
                throw new Exception("Failed Updating Address");
            };

            _contactLogsService.CreateLog("Patch", user.Username, $"{contact.Id} : {contact.FirstName} {contact.LastName}", $"Address {addressEntity.Id} Patched \n" + details);
        }

        async Task IAddressService.UpdateAddress(int userId, int contactId, int addressId, AddressUpdateDto address)
        {
            var user = await _userRepository.GetUser(userId);
            if (user == null)
            {
                throw new UserNotFoundException("User not found.");
            }
            var contact = await _contactRepository.GetContact(userId, contactId);
            if (contact == null)
            {
                throw new ContactNotFoundException("Contact not found.");
            }
            var addressEntity = await _repository.GetAddress(contactId, addressId);
            if (addressEntity == null)
            {
                throw new ArgumentNullException("Address not found.");
            }


            string details = String.Empty;
            var difference = GetObjectDifference.GetObjectDifferences<Address, AddressUpdateDto>(addressEntity, address);
            foreach (var differenceEntity in difference)
            {
                details += $"{differenceEntity.PropertyName} : From: {differenceEntity.OriginalValue} -> {differenceEntity.ChangedValue};\n";
            }
            //automapper will override addresEntity with the addresss
            _mapper.Map(address, addressEntity);
            if (!await _sharedRepository.SaveChangesAsync())
            {
                throw new Exception("Failed Updating Address");
            }

            _contactLogsService.CreateLog("Update", user.Username, $"{contact.Id} : {contact.FirstName} {contact.LastName}", $"Address {addressEntity.Id} Updated \n" + details);
        }
    }
}
