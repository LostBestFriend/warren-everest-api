using DomainModels.Models;
using System.Threading.Tasks;

namespace AppServices.Interfaces
{
    public interface IPortfolioProductAppService
    {
        void InitRelation(Portfolio portfolio, Product product);
        void DisposeRelation(Portfolio portfolio, Product product);
        Task<bool> RelationAlreadyExists(long portfolioId, long productId);
    }
}
