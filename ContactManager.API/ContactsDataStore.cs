using ContactManager.API.Models;

namespace ContactManager.API
{
    public class ContactsDataStore
    {
        public List<ContactDto> Contacts { get; set; }
        public static ContactsDataStore Current { get; } = new ContactsDataStore();

        public ContactsDataStore()
        {
            // init dummy data
            Contacts = new List<ContactDto>()
            {
                new ContactDto()
                {
                    Id = 1,
                    FirstName = "Jeremy",
                    LastName = "Flores",
                    Favorite = false,
                    Emergency = false,
                    Note = "LOL",
                    Addresses = new List<AddressDto>()
                    {
                        new AddressDto()
                        {
                            Id = 1,
                            Type = Entities.Types.AddressType.Home,
                            AddressDetails = "This is my address"
                        },
                        new AddressDto()
                        {
                            Id = 2,
                            Type = Entities.Types.AddressType.Work,
                            AddressDetails = "This is Fullscale Address"
                        }
                    },
                    Numbers = new List<NumberDto>()
                    {
                        new NumberDto()
                        {
                            Id = 1,
                            Type = Entities.Types.NumberType.Work,
                            ContactNumber = "099292929"
                        },
                        new NumberDto()
                        {
                            Id = 2,
                            Type = Entities.Types.NumberType.Home,
                            ContactNumber = "24129429"
                        }
                        
                    },
                    Emails = new List<EmailDto>()
                    {
                        new EmailDto()
                        {
                            Id = 1,
                            Type = Entities.Types.EmailType.Home,
                            EmailAddress = "jeremyGwapo@gmail.com"
                        }
                    }
                },
                new ContactDto()
                {
                    Id = 1,
                    FirstName = "Charis",
                    LastName = "Baclayon",
                    Favorite = false,
                    Emergency = false,
                    Note = "LOL2",
                    Addresses = new List<AddressDto>()
                    {
                        new AddressDto()
                        {
                            Id = 1,
                            Type = Entities.Types.AddressType.Home,
                            AddressDetails = "This is my address"
                        },
                        new AddressDto()
                        {
                            Id = 2,
                            Type = Entities.Types.AddressType.Work,
                            AddressDetails = "This is Fullscale Address"
                        }
                    },
                    Numbers = new List<NumberDto>()
                    {
                        new NumberDto()
                        {
                            Id = 1,
                            Type = Entities.Types.NumberType.Work,
                            ContactNumber = "0994423"
                        },
                        new NumberDto()
                        {
                            Id = 2,
                            Type = Entities.Types.NumberType.Home,
                            ContactNumber = "4325634"
                        }

                    }
                }
             };
            
        }
    }
}
