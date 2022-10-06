using DomainModels.Models;

namespace AppServices.Interfaces
{
    public interface IPortfolioProductAppService
    {
        void CreateRelation(Portfolio portfolio, Product product);
        void DeleteRelation(Portfolio portfolio, Product product);
        bool RelationExists(long portfolioId, long productId);
    }
}
