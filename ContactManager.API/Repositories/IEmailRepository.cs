using ContactManager.API.Entities;

namespace ContactManager.API.Repositories
{
    public interface IEmailRepository
    {
        Task<IEnumerable<Email>> GetEmails(int contactId);
        Task<Email?> GetEmail(int contactId, int emailId);
        void CreateEmail(Contact contact, Email email);
        void DeleteEmail(Email email);
    }
}
