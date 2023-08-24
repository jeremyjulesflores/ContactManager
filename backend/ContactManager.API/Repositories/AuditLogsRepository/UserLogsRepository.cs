using ContactManager.API.DbContexts;
using ContactManager.API.Entities;
using ContactManager.API.Models;

namespace ContactManager.API.Repositories.AuditLogsRepository
{
    public interface IUserLogsRepository
    {
        void CreateLog(UserLogs log);
    }
    public class UserLogsRepository : IUserLogsRepository
    {
        private readonly ContactInfoContext _context;

        public UserLogsRepository(ContactInfoContext context)
        {
            this._context = context;
        }
        public void CreateLog(UserLogs log)
        {
            _context.UserLogs.Add(log);
        }
    }
}
