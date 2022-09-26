using AppModels.AppModels;
using AutoMapper;
using DomainModels.Models;

namespace AppServices.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<UpdateCustomerDTO, Customer>();
            CreateMap<CreateCustomerDTO, Customer>();
            CreateMap<Customer, CustomerResponseDTO>();
        }
    }
}
