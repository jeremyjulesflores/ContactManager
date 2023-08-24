using ContactManager.API.DbContexts;
using ContactManager.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.API.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ContactInfoContext _context;

        public AddressRepository(ContactInfoContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<Address?> GetAddress(int contactId, int addressId)
        {
            return await _context.Address.Where(a => a.Id == addressId && a.ContactId == contactId)
                                         .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Address>> GetAddresses(int contactId)
        {
            return await _context.Address.Where(a=>a.ContactId == contactId).ToListAsync();
        }

        void IAddressRepository.CreateAddress(Contact contact, Address address)
        {
            contact.Addresses.Add(address);
        }

        void IAddressRepository.DeleteAddress(Address address)
        {
           _context.Address.Remove(address);
        }
    }
}
