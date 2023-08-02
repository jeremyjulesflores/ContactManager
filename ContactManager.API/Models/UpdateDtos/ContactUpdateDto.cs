﻿using System.ComponentModel.DataAnnotations;

namespace ContactManager.API.Models.UpdateDtos
{
    public class ContactUpdateDto
    {
        [Required(ErrorMessage = "First Name is Required")]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Last Name is Required")]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;
        public bool Favorite { get; set; }
        public bool Emergency { get; set; }
        public string? Note { get; set; }
    }
}
