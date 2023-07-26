﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static ContactManager.API.Entities.Types;

namespace ContactManager.API.Entities
{
    public class Number
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("ContactId")]
        public Contact Contact { get; set; }
        public int ContactId { get; set; }

        [Required]
        public NumberType Type { get; set; }

        [Required]
        [MaxLength(15)]
        public string ContactNumber { get; set; }

    }

}