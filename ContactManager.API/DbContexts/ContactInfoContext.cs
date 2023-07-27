using ContactManager.API.Entities;
using ContactManager.API.Models;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>().HasData(
                new Contact("Jeremy", "Flores")
                {
                    Id = 1,
                    Favorite = false,
                    Emergency = false,
                    Note = "This is a Note"
                },
                new Contact("Lirae", "Data")
                {
                    Id = 2,
                    Favorite = false,
                    Emergency = false,
                    Note = "This is a Note 2"
                },
                new Contact("Charis Arlie", "Baclayon")
                {
                    Id = 3
                }
                ) ;

            modelBuilder.Entity<Address>().HasData(
                 new Address()
                 {
                     Id = 1,
                     ContactId = 1,
                     Type = Entities.Types.AddressType.Home.ToString(),
                     AddressDetails = "This is my address 1"
                 },
                new Address()
                {
                    Id = 2,
                    ContactId = 1,
                    Type = Entities.Types.AddressType.Work.ToString(),
                    AddressDetails = "This is Fullscale Address"
                },
                new Address()
                {
                    Id = 3,
                    ContactId = 2,
                    Type = Entities.Types.AddressType.Home.ToString(),
                    AddressDetails = "This is my address"
                },
                new Address()
                {
                    Id = 4,
                    ContactId = 3,
                    Type = Entities.Types.AddressType.Work.ToString(),
                    AddressDetails = "This is Fullscale Address"
                }
                );

            modelBuilder.Entity<Number>().HasData(
                new Number()
                {
                    Id = 1,
                    ContactId = 1,
                    Type = Entities.Types.NumberType.Work.ToString(),
                    ContactNumber = "099292929"
                }
                );
            modelBuilder.Entity<Email>().HasData(
                new Email()
                {
                    Id = 1,
                    ContactId = 3,
                    Type = Entities.Types.EmailType.Home.ToString(),
                    EmailAddress = "jeremygwapo@gmail.com"
                }
                );
        }
    }
}
