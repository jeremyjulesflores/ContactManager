﻿using AutoMapper;
using ContactManager.API.Entities;
using ContactManager.API.Models;
using ContactManager.API.Models.CreationDtos;
using ContactManager.API.Models.UpdateDtos;

namespace ContactManager.API.Mappings
{
    public class AddressMappings : Profile
    {
        public AddressMappings()
        {
            CreateMap<Address, AddressDto>();
            CreateMap<AddressCreationDto, Address>();
            CreateMap<AddressUpdateDto, Address>();
            CreateMap<Address, AddressUpdateDto>();
        }
    }
}
   