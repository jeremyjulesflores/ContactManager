﻿using System.ComponentModel.DataAnnotations;

namespace ContactManager.API.Models.CreationDtos
{
    public class AddressCreationDto
    {
        [Required(ErrorMessage ="The Type Field is Required")]
        [MaxLength(30)]
        public string Type { get; set; } = string.Empty;
        [Required(ErrorMessage ="The Address Field is Required")]
        [MaxLength(200)]
        public string AddressDetails { get; set; } = string.Empty;
    }
}
 