using System.ComponentModel.DataAnnotations;

namespace ContactManager.API.Models
{
    public class UserLoginDto
    {
        [Required]
        [MinLength(6)]
        [MaxLength(15)]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
