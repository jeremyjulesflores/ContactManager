using AutoMapper;
using ContactManager.API.Entities;
using ContactManager.API.Models;

namespace ContactManager.API.Mappings
{
    public class ContactMappings : Profile
    {
        public ContactMappings()
        {
            CreateMap<Contact, ContactWithoutDetailsDto>();
            CreateMap<Contact, ContactDto>();
        }
    }
}
