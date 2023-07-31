using ContactManager.API.DbContexts;
using ContactManager.API.Entities;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace ContactManager.API.Repositories
{
    public class NumberRepository : INumberRepository
    {
        private readonly ContactInfoContext _context;

        public NumberRepository(ContactInfoContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }
        void INumberRepository.CreateNumber(Contact contact, Number number)
        {
            contact.Numbers.Add(number);
        }

        void INumberRepository.DeleteNumber(Number number)
        {
            _context.Number.Remove(number);
        }

        async Task<Number?> INumberRepository.GetNumber(int contactId, int numberId)
        {
            return await _context.Number.Where(n => n.Id == numberId && n.ContactId == contactId)
                                        .FirstOrDefaultAsync();
        }

        async Task<IEnumerable<Number>> INumberRepository.GetNumbers(int contactId)
        {
            return await _context.Number.Where(n => n.ContactId == contactId).ToListAsync();
        }
    }
}
