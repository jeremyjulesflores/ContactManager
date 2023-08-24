using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ContactManager.API.Entities
{
    public class ContactLogs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
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
