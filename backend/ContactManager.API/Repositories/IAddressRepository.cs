using ContactManager.API.Entities;

namespace ContactManager.API.Repositories
{
    public interface IAddressRepository
    {
        /// <summary>
        /// Gets all Addresses
        /// </summary>
        /// <param name="contactId">Contact Id where addresses is from</param>
        /// <returns>IEnumberable of Addresses</returns>
        Task<IEnumerable<Address>> GetAddresses(int contactId);
        /// <summary>
        /// Get Address
        /// </summary>
        /// <param name="contactId">Contact Id where addresses is from</param>
        /// <param name="addressId">Id of address to get</param>
        /// <returns>Address Object</returns>
        Task<Address?> GetAddress(int contactId, int addressId);
        /// <summary>
        /// Creates Address
        /// </summary>
        /// <param name="contact">Contact object where to add the address</param>
        /// <param name="address">Address Object to add</param>
        void CreateAddress(Contact contact, Address address);
        /// <summary>
        /// Delete Address
        /// </summary>
        /// <param name="address">address object to delete</param>
        void DeleteAddress(Address address);
    }
}
