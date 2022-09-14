using AppServices.DTOs;
using AutoMapper;
using DomainModels.Entities;

namespace Data.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Customer, CustomerDTO>().ReverseMap();
        }
    }
}
