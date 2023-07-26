using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ContactManager.API.Entities
{
    public class Email
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("ContactId")]
        public Contact Contact { get; set; }
        public int ContactId { get; set; }

        [Required]
        public EmailType Type { get; set; }

        [Required]
        [StringLength(100)]
        public string EmailAddress { get; set; }

    }

    public enum EmailType
    {
        Work,
        Home,
        School
    }
      
}
