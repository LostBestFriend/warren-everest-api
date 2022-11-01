using DomainModels.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainServices.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<int> GetQuotesAvaliableAsync(long portfolioId, long productId);
        Task<Order> GetByIdAsync(long id);
        Task<long> CreateAsync(Order model);
        Task<IEnumerable<Order>> GetExecutableOrdersAsync();
        void Update(Order model);
    }
}
