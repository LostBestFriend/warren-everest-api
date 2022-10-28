using DomainModels.Models;
using System.Threading.Tasks;

namespace AppServices.Interfaces
{
    public interface IPortfolioProductAppService
    {
        Task InitRelationAsync(Portfolio portfolio, Product product);
        Task DisposeRelationAsync(Portfolio portfolio, Product product);
        Task<bool> RelationAlreadyExistsAsync(long portfolioId, long productId);
    }
}
