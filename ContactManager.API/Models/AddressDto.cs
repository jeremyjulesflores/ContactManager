﻿using static ContactManager.API.Entities.Types;

namespace ContactManager.API.Models
{
    public class AddressDto
    {
        public int Id { get; set; }
        public AddressType Type { get; set; }
        public string AddressDetails { get; set; } = string.Empty;
    }
}