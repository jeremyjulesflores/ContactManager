using System.ComponentModel.DataAnnotations;
using System.Security;

namespace ContactManager.API.Models
{
    public class UserChangePasswordDto
    {
        [Required]
        public string Token { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
