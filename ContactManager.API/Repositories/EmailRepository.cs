using ContactManager.API.Entities;

namespace ContactManager.API.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        public Task<Email?> GetEmail(int emailId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Email>> GetEmails()
        {
            throw new NotImplementedException();
        }
    }
}
