using AppModels.MapperModels;
using AutoMapper;
using DomainModels.Models;

namespace AppServices.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerUpdateDTO, Customer>();
            CreateMap<CustomerCreateDTO, Customer>();
            CreateMap<Customer, CustomerResponseDTO>();
        }
    }
}
