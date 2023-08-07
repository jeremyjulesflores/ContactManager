using System.ComponentModel.DataAnnotations;

namespace ContactManager.API.Models.UpdateDtos
{
    public class ContactUpdateDto
    {
        [Required(ErrorMessage = "First Name is Required")]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Last Name is Required")]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;
        public bool Favorite { get; set; } = false;
        public bool Emergency { get; set; } = false;
        public string? Note { get; set; }
    }
}
