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

        public async Task DisposeRelationAsync(Portfolio portfolio, Product product)
        {
            await _portfolioProductService.DisposeRelationAsync(portfolio, product);
        }

        public async Task<bool> RelationAlreadyExistsAsync(long portfolioId, long productId)
        {
            return await _portfolioProductService.RelationAlreadyExistsAsync(portfolioId, productId);
        }
    }
}
