using ContactManager.API.Entities;

namespace ContactManager.API.Repositories
{
    public interface IEmailRepository
    {
        /// <summary>
        /// Get all Emails 
        /// </summary>
        /// <param name="contactId">Id of contact where the emails comes from</param>
        /// <returns>IEnumerable of Emails</returns>
        Task<IEnumerable<Email>> GetEmails(int contactId);
        /// <summary>
        /// Get email
        /// </summary>
        /// <param name="contactId">Id of contact to where the email is form</param>
        /// <param name="emailId">Email Object</param>
        /// <returns>Email Object</returns>
        Task<Email?> GetEmail(int contactId, int emailId);
        /// <summary>
        /// Creates email
        /// </summary>
        /// <param name="contact">Id of contact to where the mail will be created</param>
        /// <param name="email">Email object to add</param>
        void CreateEmail(Contact contact, Email email);
        /// <summary>
        /// Delete email
        /// </summary>
        /// <param name="email">Email object to delete</param>
        void DeleteEmail(Email email);
    }
}
