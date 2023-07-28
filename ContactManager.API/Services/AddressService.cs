using AutoMapper;
using ContactManager.API.Entities;
using ContactManager.API.Models;
using ContactManager.API.Models.CreationDtos;
using ContactManager.API.Models.UpdateDtos;
using ContactManager.API.Repositories;
using ContactManager.API.Repositories.Shared;

namespace ContactManager.API.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _repository;
        private readonly ISharedRepository _sharedRepository;
        private readonly IMapper _mapper;

        public AddressService(IAddressRepository repository,
                              ISharedRepository sharedRepository,
                              IMapper mapper)
        {
            this._repository = repository;
            this._sharedRepository = sharedRepository;
            this._mapper = mapper;
        }
        public async Task<bool> CreateAddress(int contactId, AddressCreationDto address)
        {
            if (!await _sharedRepository.ContactExists(contactId))
            {
                return false;
            }

            var addressToCreate = _mapper.Map<Address>(address);

            await _repository.CreateAddress(contactId, addressToCreate);

            return await _sharedRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAddress(int contactId, int addressId)
        {
            if (!await _sharedRepository.ContactExists(contactId))
            {
                return false;
            }
            var addressEntity = await _repository.GetAddress(addressId, contactId);
            if (addressEntity == null) { return false; }
            _repository.DeleteAddress(addressEntity);

            return await _sharedRepository.SaveChangesAsync();
        }

        public async Task<AddressDto?> GetAddress(int addressId, int contactId)
        {
            if (!await _sharedRepository.ContactExists(contactId))
            {
                return null;
            }

            var address = await _repository.GetAddress(addressId, contactId);
            if (address == null) { return null; };

            return _mapper.Map<AddressDto>(address);
        }

        public async Task<IEnumerable<AddressDto>> GetAddresses(int contactId)
        {
            if (!await _sharedRepository.ContactExists(contactId))
            {
                return null;
            }

            var addresses = await _repository.GetAddresses(contactId);

            return _mapper.Map<IEnumerable<AddressDto>>(addresses);
        }
        async Task<bool> IAddressService.UpdateAddress(int contactId, int addressId, AddressUpdateDto address)
        {
            if (!await _sharedRepository.ContactExists(contactId))
            {
                return false;
            }
            var addressEntity = await _repository.GetAddress(addressId, contactId);
            if (addressEntity == null)
            {
                return false;
            }
            //automapper will override addresEntity with the addresss
            _mapper.Map(address, addressEntity);

            return await _sharedRepository.SaveChangesAsync();
        }
    }
}
