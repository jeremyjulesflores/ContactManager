using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactManager.API.Entities
{
    public class Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        public bool Favorite { get; set; } = false;
        public bool Emergency { get; set; } = false;
        [MaxLength(200)]
        public string? Note { get; set; }

        // Addresses for the contact
        public ICollection<Address>? Addresses { get; set; }
            = new List<Address>();
            

        //Numbers for the contact
        public ICollection<Number>? Numbers { get; set; }
            = new List<Number>();

        //Emails for the contact
        public ICollection<Email>? Emails { get; set; }
            = new List<Email>();

        public Contact(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

    }
}
