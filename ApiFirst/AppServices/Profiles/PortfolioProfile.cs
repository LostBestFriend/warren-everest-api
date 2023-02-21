using AppModels.AppModels.Portfolios;
using AutoMapper;
using DomainModels.Models;

namespace AppServices.Profiles
{
    public class PortfolioProfile : Profile
    {
        public PortfolioProfile()
        {
            CreateMap<Portfolio, PortfolioResponse>()
    .ForMember(portfolioResult => portfolioResult.Products, p => p.MapFrom(portfolio => portfolio.PortfolioProducts));
            CreateMap<CreatePortfolio, Portfolio>();
            CreateMap<UpdatePortfolio, Portfolio>();
        }
    }
}
