using ContactManager.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.API.DbContexts
{
    public class ContactInfoContext : DbContext
    {
        public DbSet<User> User { get; set; } = null!;
        public DbSet<Contact> Contacts { get; set; } = null!;
        public DbSet<Address> Address { get; set; } = null!;
        public DbSet<Number> Number { get; set; } = null!;
        public DbSet<Email> Email { get; set; } = null!;

        public DbSet<UserLogs> UserLogs { get; set; } = null!;

        public DbSet<ContactLogs> ContactLogs { get; set; } = null!;

        public ContactInfoContext(DbContextOptions<ContactInfoContext> options)
            : base(options)
        {

        }

        public void Initialize(){
            Database.Migrate();
            SaveChanges();
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //modelBuilder.Entity<User>().HasData(
        //    //    new User()
        //    //    {
        //    //        Id = 1,
        //    //        FirstName = "FirstNameTest",
        //    //        LastName = "LastNameTest",
        //    //        Email = "Email@Test.com",
        //    //        Username = "UserNameTest",
        //    //        PasswordHash = Encoding.ASCII.
        //    //        GetBytes("KFlRVuLl72kiIUkGftgBRUgFhhVpKZDLAouuWHfJP1W7wrW21xniaiZ8lJXF9b/3a5t6/Ubh0K6ioRBXMuxkvg=="),
        //    //        PasswordSalt = Encoding.ASCII.
        //    //        GetBytes("ggQlEtdP+kgYZBKUSngJtYqpphwu6U+/odwx7hV2h0E2AAccbFFRp4I0LP62mInVEASXp/1TS7wTS/4bOlN5glD4Hv92jO6bkwt/W/m9l1zcOaErwb0j4ynDth07dYMt9KAHlF/xT53ynopUf7WJzRQvMXg5Ih3MdvA3wsOBXD4="),
        //    //        VerificationToken = "VERIFICATIONTOKENTEST",
        //    //        VerifiedAt = DateTime.UtcNow
        //    //    }
        //    //    ) ;
        //    //modelBuilder.Entity<Contact>().HasData(
        //    //    new Contact("Jeremy", "Flores")
        //    //    {
        //    //        Id = 1,
        //    //        UserId = 1,
        //    //        Favorite = false,
        //    //        Emergency = false,
        //    //        Note = "This is a Note"
        //    //    },
        //    //    new Contact("Lirae", "Data")
        //    //    {
        //    //        Id = 2,
        //    //        UserId = 1,
        //    //        Favorite = false,
        //    //        Emergency = false,
        //    //        Note = "This is a Note 2"
        //    //    },
        //    //    new Contact("Charis Arlie", "Baclayon")
        //    //    {
        //    //        Id = 3,
        //    //        UserId = 1,
        //    //    }
        //    //    ) ;

        //    //modelBuilder.Entity<Address>().HasData(
        //    //     new Address()
        //    //     {
        //    //         Id = 1,
        //    //         ContactId = 1,
        //    //         Type = Entities.Types.AddressType.Home.ToString(),
        //    //         AddressDetails = "This is my address 1"
        //    //     },
        //    //    new Address()
        //    //    {
        //    //        Id = 2,
        //    //        ContactId = 1,
        //    //        Type = Entities.Types.AddressType.Work.ToString(),
        //    //        AddressDetails = "This is Fullscale Address"
        //    //    },
        //    //    new Address()
        //    //    {
        //    //        Id = 3,
        //    //        ContactId = 2,
        //    //        Type = Entities.Types.AddressType.Home.ToString(),
        //    //        AddressDetails = "This is my address"
        //    //    },
        //    //    new Address()
        //    //    {
        //    //        Id = 4,
        //    //        ContactId = 3,
        //    //        Type = Entities.Types.AddressType.Work.ToString(),
        //    //        AddressDetails = "This is Fullscale Address"
        //    //    }
        //    //    );

        //    //modelBuilder.Entity<Number>().HasData(
        //    //    new Number()
        //    //    {
        //    //        Id = 1,
        //    //        ContactId = 1,
        //    //        Type = Entities.Types.NumberType.Work.ToString(),
        //    //        ContactNumber = "099292929"
        //    //    }
        //    //    );
        //    //modelBuilder.Entity<Email>().HasData(
        //    //    new Email()
        //    //    {
        //    //        Id = 1,
        //    //        ContactId = 3,
        //    //        Type = Entities.Types.EmailType.Home.ToString(),
        //    //        EmailAddress = "jeremygwapo@gmail.com"
        //    //    }
        //    //    );
        //}
    }
}
