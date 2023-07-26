using System.ComponentModel.DataAnnotations;

namespace ContactManager.API.Entities
{
    public class Contact
    {
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
    }
}
