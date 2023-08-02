using System.ComponentModel.DataAnnotations;

namespace ContactManager.API.Models
{
    public class ContactLogDto { 
        [Required]
        [MaxLength(50)]
        public string Action { get; set; }
        [Required]
        [MaxLength(20)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(50)]
        public string ContactName { get; set; }
        [Required]
        [MaxLength(200)]
        public string? Details { get; set; }
        [Required]
        public DateTime TimeStamp { get; set; }
    }
}
