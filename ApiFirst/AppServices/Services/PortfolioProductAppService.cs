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

        public void CreateRelation(Portfolio portfolio, Product product)
        {
            _portfolioProductService.CreateRelationAsync(portfolio, product);
        }

        public void DeleteRelation(Portfolio portfolio, Product product)
        {
            _portfolioProductService.DeleteRelationAsync(portfolio, product);
        }

        public bool RelationExists(long portfolioId, long productId)
        {
            return _portfolioProductService.RelationExists(portfolioId, productId);
        }
    }
}
