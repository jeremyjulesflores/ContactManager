using ContactManager.API.Entities;
using System.ComponentModel.DataAnnotations;
using System.Security;

namespace ContactManager.API.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        //Contacts of User
        public ICollection<Contact> Contacts { get; set; } 
            = new List<Contact>();
    }
}
