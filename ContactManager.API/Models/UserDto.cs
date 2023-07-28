using ContactManager.API.Entities;
using System.ComponentModel.DataAnnotations;
using System.Security;

namespace ContactManager.API.Models
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        //Contacts of User
        public ICollection<Contact> Contacts { get; set; } 
            = new List<Contact>();
    }
}
