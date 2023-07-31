using ContactManager.API.DbContexts;
using ContactManager.API.Entities;
using ContactManager.API.Models.CreationDtos;
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
        public async Task<Contact?> GetContact(int contactId)
        {
          
            
            return await this._context.Contacts.Include(c => c.Addresses)
                                               .Include(c => c.Numbers)
                                               .Include(c => c.Emails)
                                               .FirstOrDefaultAsync(c => c.Id == contactId);
        }

        public async Task<IEnumerable<Contact>> GetContacts(int userId)
        {
            return await _context.Contacts.Where(c=>c.UserId == userId).ToListAsync();
        }

        void IContactRepository.CreateContact(User user, Contact contact)
        {
            user.Contacts.Add(contact);
        }

        void IContactRepository.DeleteContact(Contact contact)
        {
            _context.Contacts.Remove(contact);
        }
    }
}
