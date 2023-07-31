using ContactManager.API.Entities;
using ContactManager.API.Models;
using ContactManager.API.Models.CreationDtos;
using ContactManager.API.Models.UpdateDtos;
using Microsoft.AspNetCore.JsonPatch;

namespace ContactManager.API.Services
{
    public interface IAddressService
    {
        /// <summary>
        /// Gets all Address by contact Id
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns>An IEnumerable of AddressDto</returns>
        Task<IEnumerable<AddressDto>> GetAddresses(int contactId);
        /// <summary>
        /// Gets one Address
        /// </summary>
        /// <param name="addressId"></param>
        /// <param name="contactId"></param>
        /// <returns>An AdressDto Object</returns>
        Task<AddressDto?> GetAddress(int contactId, int addressId);
        /// <summary>
        /// Creates Address
        /// </summary>
        /// <param name="contactId"></param>
        /// <param name="address"></param>
        /// <returns>bool if create is successful or not</returns>
        Task<bool> CreateAddress(int contactId, AddressCreationDto address);
        /// <summary>
        /// Deletes an Address
        /// </summary>
        /// <param name="contactId"></param>
        /// <param name="addressId"></param>
        /// <returns>bool if address is successful or not</returns>
        Task <bool> DeleteAddress(int contactId, int addressId);
        /// <summary>
        /// Updates and Address
        /// </summary>
        /// <param name="contactId"></param>
        /// <param name="address"></param>
        /// <returns>bool if address is successful or not</returns>
        Task <bool> UpdateAddress(int contactId, int addressId, AddressUpdateDto address);
        /// <summary>
        /// Get the Address to Patch
        /// </summary>
        /// <param name="contactId"></param>
        /// <param name="addressId"></param>
        /// <returns>Address Update Dto</returns>
        Task<AddressUpdateDto> GetAddressToPatch(int contactId, int addressId);
        /// <summary>
        /// Patch Address
        /// </summary>
        /// <param name="contactId"></param>
        /// <param name="addressId"></param>
        /// <param name="address"></param>
        /// <returns>Bool if the address has been patch</returns>
        Task<bool> PatchNumber(int contactId, int addressId, AddressUpdateDto address);
        //Try to implement
        //Task<bool> PartiallyUpdateAddress(int contactId,
        //                                  int addressId,
        //                                  JsonPatchDocument<AddressUpdateDto> patchDocument);
    }

}
