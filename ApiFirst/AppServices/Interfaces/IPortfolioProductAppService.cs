using DomainModels.Models;
using System.Threading.Tasks;

namespace AppServices.Interfaces
{
    public interface IPortfolioProductAppService
    {
        void InitRelationAsync(Portfolio portfolio, Product product);
        void DisposeRelation(Portfolio portfolio, Product product);
        Task<bool> RelationAlreadyExists(long portfolioId, long productId);
    }
}
