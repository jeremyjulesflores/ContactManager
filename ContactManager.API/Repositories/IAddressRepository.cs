using ContactManager.API.Entities;

namespace ContactManager.API.Repositories
{
    public interface IAddressRepository
    {
        Task<IEnumerable<Address>> GetAddresses(int contactId);
        Task<Address?> GetAddress(int addressId, int contactId);
        Task CreateAddress(int contactId, Address address);
        void DeleteAddress(Address address);
    }
}
