using DomainModels.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainServices.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<Order> GetAll();
        int GetAvailableQuotes(long portfolioId, long productId);
        Task<Order> GetByIdAsync(long id);
        Task<long> CreateAsync(Order model);
        IList<Order> GetOrdersToExecute();
        void Update(Order model);
        void Delete(long id);
    }
}
