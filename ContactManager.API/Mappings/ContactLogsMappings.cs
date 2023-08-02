using AutoMapper;
using ContactManager.API.Entities;
using ContactManager.API.Models;

namespace ContactManager.API.Mappings
{
    public class ContactLogsMappings : Profile
    {
        public ContactLogsMappings()
        {
            CreateMap<ContactLogDto, ContactLogs>();
            CreateMap<ContactLogs, ContactLogDto>();
        }
    }
}
