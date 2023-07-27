using ContactManager.API.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.API.Repositories.Shared
{
    public class SharedRepository : ISharedRepository
    {
        private readonly ContactInfoContext _context;

        public SharedRepository(ContactInfoContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<bool> ContactExists(int contactId)
        {
            return await _context.Contacts.AnyAsync(c => c.Id == contactId);
        }

        public Task<bool> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
