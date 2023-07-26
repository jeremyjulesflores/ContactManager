using ContactManager.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.API.DbContexts
{
    public class ContactInfoContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; } = null!;
        public DbSet<Address> Address { get; set; } = null!;
        public DbSet<Number> Number { get; set; } = null!;
        public DbSet<Email> Email { get; set; } = null!;

        public ContactInfoContext(DbContextOptions<ContactInfoContext> options)
            : base(options)
        {

        }
    }
}
