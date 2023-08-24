using System.ComponentModel.DataAnnotations;
using System.Security;

namespace ContactManager.API.Models.CreationDtos
{
    public class UserCreationDto
    {
        //For Registration
        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [MaxLength(25)]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [MinLength(6)]
        [MaxLength(15)]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
