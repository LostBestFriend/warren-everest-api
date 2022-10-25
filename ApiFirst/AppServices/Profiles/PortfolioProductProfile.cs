using AppModels.AppModels.PortfolioProducts;
using AutoMapper;
using DomainModels.Models;

namespace AppServices.Profiles
{
    public class PortfolioProductProfile : Profile
    {
        public PortfolioProductProfile()
        {
            CreateMap<PortfolioProduct, PortfolioProductResponse>();
        }
    }
}
