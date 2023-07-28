using ContactManager.API.DbContexts;
using ContactManager.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.API.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ContactInfoContext _context;
        private readonly IContactRepository _contactRepository;

        public AddressRepository(ContactInfoContext context,
                                 IContactRepository contactRepository)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
            this._contactRepository = contactRepository ?? throw new ArgumentNullException(nameof(contactRepository));
        }
        public async Task<Address?> GetAddress(int addressId, int contactId)
        {
            return await _context.Address.Where(a => a.Id == addressId && a.ContactId == contactId)
                                         .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Address>> GetAddresses(int contactId)
        {
            return await _context.Address.Where(a=>a.ContactId == contactId).ToListAsync();
        }

        async Task IAddressRepository.CreateAddress(int contactId, Address address)
        {
            var contact = await _contactRepository.GetContact(contactId, includeContactDetails: true);
            if(contact != null)
            {
                contact.Addresses.Add(address);
            }
        }

        void IAddressRepository.DeleteAddress(Address address)
        {
           _context.Address.Remove(address);
        }
    }
}
