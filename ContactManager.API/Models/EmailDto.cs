using ContactManager.API.Entities;
using static ContactManager.API.Entities.Types;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ContactManager.API.Models
{
    public class EmailDto
    {
        public int Id { get; set; }
        public EmailType Type { get; set; }
        public string EmailAddress { get; set; }

    }
}
