using ContactManager.API.DbContexts;
using ContactManager.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.API.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        private readonly ContactInfoContext _context;
        public EmailRepository(ContactInfoContext context)
        {
               this._context = context?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Email?> GetEmail(int contactId, int emailId)
        {
            return await _context.Email.Where(a => a.Id == emailId && a.ContactId == contactId)
                                         .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Email>> GetEmails(int contactId)
        {
            return await _context.Email.Where(a => a.ContactId == contactId).ToListAsync();
        }

        void IEmailRepository.CreateEmail(Contact contact, Email Email)
        {
            contact.Emails.Add(Email);
        }

        void IEmailRepository.DeleteEmail(Email Email)
        {
            _context.Email.Remove(Email);
        }
    }
}
