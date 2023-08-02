using ContactManager.API.Entities;
using ContactManager.API.Models.CreationDtos;

namespace ContactManager.API.Repositories
{
    public interface IContactRepository
    {
        /// <summary>
        /// Gets all contacts
        /// </summary>
        /// <param name="userId">Id of user where contacts come from</param>
        /// <returns>IEnumerable of contacts</returns>
        Task<IEnumerable<Contact>> GetContacts(int userId);
        /// <summary>
        /// Get contact
        /// </summary>
        /// <param name="contactId">Id of contact to get</param>
        /// <returns></returns>
        Task<Contact?> GetContact(int contactId);
        /// <summary>
        /// Creates contact
        /// </summary>
        /// <param name="user">User object to where the contact will be created</param>
        /// <param name="contact">Contact to be created</param>
        void CreateContact(User user, Contact contact);
        /// <summary>
        /// Deletes contact
        /// </summary>
        /// <param name="contact">Contact object to delete</param>
        void DeleteContact(Contact contact);

    }
}
