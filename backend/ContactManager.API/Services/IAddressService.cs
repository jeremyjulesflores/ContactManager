using ContactManager.API.Entities;
using ContactManager.API.Models;
using ContactManager.API.Models.CreationDtos;
using ContactManager.API.Models.UpdateDtos;

namespace ContactManager.API.Services
{
    public interface IAddressService
    {
        /// <summary>
        /// Gets all Address by contact Id
        /// </summary>
        /// <param name="contactId">Id of contact</param>
        /// <param name="userId">Id of user</param>
        /// <returns>An IEnumerable of AddressDto</returns>
        Task<IEnumerable<AddressDto>> GetAddresses(int userId, int contactId);
        /// <summary>
        /// Gets one Address
        /// </summary>
        /// <param name="userId">Id of user</param>
        /// <param name="addressId">Id of address</param>
        /// <param name="contactId"></param>
        /// <returns>An AdressDto Object</returns>
        Task<AddressDto?> GetAddress(int userId, int contactId, int addressId);
        /// <summary>
        /// Creates Address
        /// </summary>
        /// <param name="userId">Id of user</param>
        /// <param name="contactId">Id of contact</param>
        /// <param name="address">Creation Object of address</param>
        /// <returns>bool if create is successful or not</returns>
        Task CreateAddress(int userId, int contactId, AddressCreationDto address);
        /// <summary>
        /// Deletes an Address
        /// </summary>
        /// <param name="contactId">Id of contact</param>
        /// <param name="addressId">Id of address to delete</param>
        /// <param name="userId">Id of user to get contact from</param>
        /// <returns>bool if address is successful or not</returns>
        Task DeleteAddress(int userId, int contactId, int addressId);
        /// <summary>
        /// Updates and Address
        /// </summary>
        /// <param name="userId">Id of user</param>
        /// <param name="contactId">Id of contact</param>
        /// <param name="address">Creation Object of address</param>
        /// <returns>bool if address is successful or not</returns>
        Task UpdateAddress(int userId, int contactId, int addressId, AddressUpdateDto address);
        /// <summary>
        /// Get the Address to Patch
        /// </summary>
        /// <param name="userId">Id of user</param>
        /// <param name="contactId">Id of contact</param>
        /// <param name="addressId">Id of address to get from</param>
        /// <returns>Address Update Dto</returns>
        Task<AddressUpdateDto> GetAddressToPatch(int userId, int contactId, int addressId);
        /// <summary>
        /// Patch Address
        /// </summary>
        /// <param name="userId">Id of user</param>
        /// <param name="contactId">Id of contact</param>
        /// <param name="addressId">Id of address to patch</param>
        /// <param name="address">Updated Address Object</param>
        /// <returns>Bool if the address has been patch</returns>
        Task PatchAddress(int userId,int contactId, int addressId, AddressUpdateDto address);
                                
    }

}
