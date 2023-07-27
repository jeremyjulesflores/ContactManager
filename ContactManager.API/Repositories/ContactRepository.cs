using ContactManager.API.DbContexts;
using ContactManager.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.API.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactInfoContext _context;

        public ContactRepository(ContactInfoContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<Contact?> GetContact(int contactId, bool includeContactDetails)
        {
            if (includeContactDetails)
            {
                return await _context.Contacts.Include(c => c.Addresses)
                                              .Include(c => c.Numbers)
                                              .Include(c => c.Emails)
                                              .FirstOrDefaultAsync(c => c.Id == contactId);
            }

            return await _context.Contacts
                .Where(c => c.Id == contactId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Contact>> GetContacts()
        {
            return await _context.Contacts.ToListAsync();
        }

        public async Task<bool> ContactExists(int contactId)
        {
            return await _context.Contacts.AnyAsync(c=> c.Id == contactId);
        }
    }
}
