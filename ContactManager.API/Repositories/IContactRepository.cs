using ContactManager.API.Entities;
using ContactManager.API.Models.CreationDtos;

namespace ContactManager.API.Repositories
{
    public interface IContactRepository
    {
        /// <summary>
        /// Get All Contacts
        /// </summary>
        /// <returns>IEnumerable of Type Contact</returns>
        Task<IEnumerable<Contact>> GetContacts(int userId);
        /// <summary>
        /// Get a Contact with or without details
        /// </summary>
        /// <param name="contactId">Contact Id</param>
        /// <param name="includeContactDetails">Bool if details are included or not</param>
        /// <returns>A Contact with or without details</returns>
        Task<Contact?> GetContact(int contactId);

        void CreateContact(User user, Contact contact);
        void DeleteContact(Contact contact);

    }
}
