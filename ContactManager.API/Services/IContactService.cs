using ContactManager.API.Models;
using ContactManager.API.Models.CreationDtos;
using ContactManager.API.Models.UpdateDtos;
using System.Runtime.CompilerServices;

namespace ContactManager.API.Services
{
    public interface IContactService
    {
        /// <summary>
        /// Get all contacts
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>IEnumerable of Contact without details</returns>
        Task<IEnumerable<ContactWithoutDetailsDto>> GetContacts(int userId);
        /// <summary>
        /// Get a Contact
        /// </summary>
        /// <param name="contactId">Id of Contact to Get/param>
        /// <param name="userId">Id of User</param>
        /// <returns>Contact Dto</returns>
        Task<ContactDto?> GetContact(int userId, int contactId);
        /// <summary>
        /// Creates a contact
        /// </summary>
        /// <param name="userId">Id of User</param>
        /// <param name="contact">Contact object for creation</param>
        /// <returns></returns>
        Task CreateContact(int userId, ContactCreationDto contact);
        /// <summary>
        /// Deletes a contact
        /// </summary>
        /// <param name="contactId">Id of contact to delete</param>
        /// <param name="userId">Id of user</param>
        /// <returns></returns>
        Task DeleteContact(int userId, int contactId);
        /// <summary>
        /// Updates a contact
        /// </summary>
        /// <param name="userId">Id of user</param>
        /// <param name="contactId">Id of contact to update</param>
        /// <param name="contact">Updated Contact object</param>
        /// <returns></returns>
        Task UpdateContact(int userId, int contactId, ContactUpdateDto contact);
        /// <summary>
        /// Get the Contact to Patch
        /// </summary>
        /// <param name="userId">Id of user</param>
        /// <param name="contactId">Id of contact to process</param>
        /// <returns></returns>
        Task<ContactUpdateDto> GetContactToPatch(int userId, int contactId);
        /// <summary>
        /// Patch Contact
        /// </summary>
        /// <param name="userId">Id of user</param>
        /// <param name="contactId">Id of contact to patch</param>
        /// <param name="contact">Updated Contact object</param>
        /// <returns></returns>
        Task PatchContact(int userId, int contactId, ContactUpdateDto contact);
    }
}
