using ContactManager.API.DbContexts;
using ContactManager.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace ContactManager.API.Repositories.AuditLogsRepository
{
    public interface IContactLogsRepository
    {
        void CreateLog(ContactLogs log);
        Task<IEnumerable<ContactLogs>> GetLog(string username);
    }
    public class ContactLogsRepository : IContactLogsRepository
    {
        private readonly ContactInfoContext _context;

        public ContactLogsRepository(ContactInfoContext context)
        {
            this._context = context;
        }

        public void CreateLog(ContactLogs log)
        {
            _context.ContactLogs.Add(log);
        }

        public async Task<IEnumerable<ContactLogs>> GetLog(string username)
        {
            return await _context.ContactLogs.Where(c => c.UserName == username).ToListAsync();
        }
    }
}
