using System.ComponentModel.DataAnnotations;

namespace ContactManager.API.Models.CreationDtos
{
    public class NumberCreationDto
    {
        [Required(ErrorMessage = "Type is Required")]
        [MaxLength(50)]
        public string Type { get; set; } = string.Empty;
        [Required(ErrorMessage = "Contact Number is Required")]
        [MaxLength(50)]
        public string ContactNumber { get; set; } = string.Empty;
    }
}
