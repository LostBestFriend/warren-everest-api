using AppServices.Interfaces;
using DomainModels.Models;
using DomainServices.Interfaces;
using System;
using System.Threading.Tasks;

namespace AppServices.Services
{
    public class PortfolioProductAppService : IPortfolioProductAppService
    {
        private readonly IPortfolioProductService _portfolioProductService;

        public PortfolioProductAppService(IPortfolioProductService portfolioProductServices)
        {
            _portfolioProductService = portfolioProductServices ??
                throw new ArgumentNullException(nameof(portfolioProductServices));
        }

        public async Task InitRelationAsync(Portfolio portfolio, Product product)
        {
            await _portfolioProductService.InitRelationAsync(portfolio, product);
        }

        public void DisposeRelation(Portfolio portfolio, Product product)
        {
            _portfolioProductService.DisposeRelationAsync(portfolio, product);
        }

        public async Task<bool> RelationAlreadyExists(long portfolioId, long productId)
        {
            return await _portfolioProductService.RelationAlreadyExists(portfolioId, productId);
        }
    }
}
