﻿using System.ComponentModel.DataAnnotations;

namespace ContactManager.API.Models.UpdateDtos
{
    public class EmailUpdateDto
    {
        [Required(ErrorMessage = "Type is Required")]
        [MaxLength(50)]
        public string Type { get; set; } = string.Empty;
        [Required(ErrorMessage = "Email Address is Required")]
        [MaxLength(50)]
        public string EmailAddress { get; set; } = string.Empty;
    }
}
