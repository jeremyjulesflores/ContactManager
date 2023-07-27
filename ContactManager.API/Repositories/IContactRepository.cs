using ContactManager.API.Entities;

namespace ContactManager.API.Repositories
{
    public interface IContactRepository
    {
        /// <summary>
        /// Get All Contacts
        /// </summary>
        /// <returns>IEnumerable of Type Contact</returns>
        Task<IEnumerable<Contact>> GetContacts();
        /// <summary>
        /// Get a Contact with or without details
        /// </summary>
        /// <param name="contactId">Contact Id</param>
        /// <param name="includeContactDetails">Bool if details are included or not</param>
        /// <returns>A Contact with or without details</returns>
        Task<Contact?> GetContact(int contactId, bool includeContactDetails);
        /// <summary>
        /// Checks if Contact Exists
        /// </summary>
        /// <param name="contactId">Contact Id</param>
        /// <returns>Bool true or false whether or not Contact exists</returns>
        Task<bool> ContactExists(int contactId);
    }
}
