using DomainModels.Models;
using System.Threading.Tasks;

namespace DomainServices.Interfaces
{
    public interface IPortfolioProductService
    {
        Task InitRelationAsync(Portfolio portfolio, Product product);
        Task DisposeRelationAsync(Portfolio portfolio, Product product);
        Task<PortfolioProduct> GetByRelationAsync(long portfolioId, long productId);
        bool RelationAlreadyExists(long portfolioId, long productId);
    }
}
