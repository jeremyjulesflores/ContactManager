using AutoMapper;
using ContactManager.API.Entities;
using ContactManager.API.Models;
using ContactManager.API.Models.CreationDtos;
using ContactManager.API.Models.UpdateDtos;

namespace ContactManager.API.Mappings
{
    public class EmailMappings : Profile
    {
        public EmailMappings()
        {
            CreateMap<Email, EmailDto>();
            CreateMap<Email, EmailCreationDto>();
            CreateMap<Email, EmailUpdateDto>();
            CreateMap<EmailCreationDto, Email>();
            CreateMap<EmailUpdateDto, Email>();
        }
    }
}
