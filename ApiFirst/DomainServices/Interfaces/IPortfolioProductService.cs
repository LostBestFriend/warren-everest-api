using DomainModels.Models;
using System.Threading.Tasks;

namespace DomainServices.Interfaces
{
    public interface IPortfolioProductService
    {
        Task CreateRelationAsync(Portfolio portfolio, Product product);
        Task DeleteRelationAsync(Portfolio portfolio, Product product);
        Task<PortfolioProduct> GetByRelationAsync(long portfolioId, long productId);
        bool RelationExists(long portfolioId, long productId);
    }
}
