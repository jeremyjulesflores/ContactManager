using System.ComponentModel.DataAnnotations;

namespace ContactManager.API.Models.CreationDtos
{
    public class EmailCreationDto
    {
        [Required(ErrorMessage = "Type is Required")]
        [MaxLength(50)]
        public string Type { get; set; } = string.Empty;
        [Required(ErrorMessage = "Email Address is Required")]
        [MaxLength(50)]
        [EmailAddress]
        public string EmailAddress { get; set; } = string.Empty;
    }
}
