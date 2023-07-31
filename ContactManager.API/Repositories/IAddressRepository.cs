using ContactManager.API.Entities;

namespace ContactManager.API.Repositories
{
    public interface IAddressRepository
    {
        Task<IEnumerable<Address>> GetAddresses(int contactId);
        Task<Address?> GetAddress(int contactId, int addressId);
        void CreateAddress(Contact contact, Address address);
        void DeleteAddress(Address address);
    }
}
