using AutoMapper;
using ContactManager.API.Entities;
using ContactManager.API.Models;

namespace ContactManager.API.Mappings
{
    public class EmailMappings : Profile
    {
        public EmailMappings()
        {
            CreateMap<Email, EmailDto>();
        }
    }
}
