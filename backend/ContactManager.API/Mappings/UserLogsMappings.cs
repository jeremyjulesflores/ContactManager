using AutoMapper;
using ContactManager.API.Entities;
using ContactManager.API.Models;

namespace ContactManager.API.Mappings
{
    public class UserLogsMappings : Profile
    {
        public UserLogsMappings()
        {
            CreateMap<UserLogDto, UserLogs>();
            CreateMap<UserLogs, UserLogDto>();
        }
    }
}
