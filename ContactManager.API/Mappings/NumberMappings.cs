using AutoMapper;
using ContactManager.API.Entities;
using ContactManager.API.Models;

namespace ContactManager.API.Mappings
{
    public class NumberMappings : Profile
    {
        public NumberMappings()
        {
            CreateMap<Number, NumberDto>();
        }
    }
}
