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
        public bool Favorite { get; set; }
        public bool Emergency { get; set; }
        [MaxLength(200)]
        public string? Note { get; set; }

        // Addresses for the contact
        public List<Address>? Addresses { get; set; }

        //Numbers for the contact
        public List<Number>? Numbers { get; set; }

        //Emails for the contact
        public List<Email>? Emails { get; set; }
    }
}
