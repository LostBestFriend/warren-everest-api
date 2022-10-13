using DomainModels.Models;

namespace AppServices.Interfaces
{
    public interface IPortfolioProductAppService
    {
        void InitRelation(Portfolio portfolio, Product product);
        void DisposeRelation(Portfolio portfolio, Product product);
        bool RelationAlreadyExists(long portfolioId, long productId);
    }
}
