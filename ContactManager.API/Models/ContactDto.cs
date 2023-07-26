using ContactManager.API.Entities;
using System.ComponentModel.DataAnnotations;

namespace ContactManager.API.Models
{
    public class ContactDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public bool Favorite { get; set; } = false;
        public bool Emergency { get; set; } = false;
        public string? Note { get; set; }

        // Addresses for the contact
        public ICollection<AddressDto> Addresses { get; set; }
            =new List<AddressDto>();

        //Numbers for the contact
        public ICollection<NumberDto> Numbers { get; set; }
            = new List<NumberDto>();

        //Emails for the contact
        public ICollection<EmailDto> Emails { get; set; }
           = new List<EmailDto>();
    }
}
