using AutoMapper;
using ContactManager.API.Entities;
using ContactManager.API.Models;
using ContactManager.API.Models.CreationDtos;
using ContactManager.API.Models.UpdateDtos;

namespace ContactManager.API.Mappings
{
    public class NumberMappings : Profile
    {
        public NumberMappings()
        {
            CreateMap<Number, NumberDto>();
            CreateMap<NumberCreationDto, Number>();
            CreateMap<NumberUpdateDto, Number>();
            CreateMap<Number, NumberUpdateDto>();
        }
    }
}
