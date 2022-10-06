using AppModels.AppModels.Order;
using AutoMapper;
using DomainModels.Models;

namespace AppServices.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<UpdateOrder, Order>();
            CreateMap<CreateOrder, Order>();
            CreateMap<Order, OrderResponse>();
        }
    }
}
