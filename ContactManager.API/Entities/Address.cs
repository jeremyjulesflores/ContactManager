using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactManager.API.Entities
{
    public class Address
    {
        public int Id { get; set; }

        [ForeignKey("ContactId")]
        public Contact Contact { get; set; }
        public int ContactId { get; set; }

        [Required]
        public AddressType Type { get; set; }

        [Required]
        [StringLength(100)]
        public string AddressDetails { get; set; }

    }

    public enum AddressType
    {
        Billing,
        Delivery,
        Home,
        Work
    }
}
