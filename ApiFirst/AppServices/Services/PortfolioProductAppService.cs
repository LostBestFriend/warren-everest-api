using AppServices.Interfaces;
using DomainModels.Models;
using DomainServices.Interfaces;
using System;

namespace AppServices.Services
{
    public class PortfolioProductAppService : IPortfolioProductAppService
    {
        private readonly IPortfolioProductService _portfolioProductService;

        public PortfolioProductAppService(IPortfolioProductService portfolioProductServices)
        {
            _portfolioProductService = portfolioProductServices ?? throw new ArgumentNullException(nameof(portfolioProductServices));
        }

        public void InitRelation(Portfolio portfolio, Product product)
        {
            _portfolioProductService.InitRelationAsync(portfolio, product);
        }

        public void DisposeRelation(Portfolio portfolio, Product product)
        {
            _portfolioProductService.DisposeRelationAsync(portfolio, product);
        }

        public bool RelationAlreadyExists(long portfolioId, long productId)
        {
            return _portfolioProductService.RelationAlreadyExists(portfolioId, productId);
        }
    }
}
