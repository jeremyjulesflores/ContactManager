using ContactManager.API.Entities;
using static ContactManager.API.Entities.Types;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ContactManager.API.Models
{
    public class NumberDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string ContactNumber { get; set; }

    }
}
