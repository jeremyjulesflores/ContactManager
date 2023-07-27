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
        public async Task<Address?> GetAddress(int addressId)
        {
            return await _context.Address.Where(a => a.Id == addressId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Address>> GetAddresses(int contactId)
        {

            var addresses = await _context.Contacts.SelectMany(c=> c.Addresses)
                .Where(a=> a.Id == contactId).ToListAsync();
            return addresses;
        }

        async Task IAddressRepository.CreateAddress(int contactId, Address address)
        {
            var contact = await _contactRepository.GetContact(contactId, includeContactDetails: true);
            if(contact != null)
            {
                contact.Addresses.Add(address);
            }
        }
    }
}
