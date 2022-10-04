using AppModels.AppModels;
using AutoMapper;
using DomainModels.Models;

namespace AppServices.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<UpdateCustomer, Customer>();
            CreateMap<CreateCustomer, Customer>();
            CreateMap<Customer, CustomerResponse>();
        }
    }
}
