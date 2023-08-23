using AutoMapper;
using ContactManager.API.Entities;
using ContactManager.API.Models;
using ContactManager.API.Models.CreationDtos;
using ContactManager.API.Models.UpdateDtos;

namespace ContactManager.API.Mappings
{
    public class ContactMappings : Profile
    {
        public ContactMappings()
        {
            CreateMap<Contact, ContactWithoutDetailsDto>();
            CreateMap<Contact, ContactDto>();
            CreateMap<ContactCreationDto, Contact>();
            CreateMap<ContactUpdateDto, Contact>();
            CreateMap<Contact, ContactCreationDto>();
            CreateMap<Contact, ContactUpdateDto>();

        }
    }
}
