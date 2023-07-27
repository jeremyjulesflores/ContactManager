using ContactManager.API.Entities;

namespace ContactManager.API.Repositories
{
    public interface IEmailRepository
    {
        Task<IEnumerable<Email>> GetEmails();
        Task<Email?> GetEmail(int emailId);
    }
}
