using AppModels.AppModels.Products;
using AutoMapper;
using DomainModels.Models;

namespace AppServices.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductResponse>().ReverseMap();
            CreateMap<CreateProduct, Product>();
            CreateMap<UpdateProduct, Product>();
        }
    }
}
