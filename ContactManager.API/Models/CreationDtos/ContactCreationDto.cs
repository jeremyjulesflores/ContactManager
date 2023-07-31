using System.ComponentModel.DataAnnotations;

namespace ContactManager.API.Models.CreationDtos
{
    public class ContactCreationDto
    {
        [Required(ErrorMessage = "First Name is Required")]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Last Name is Required")]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;
    }
}
