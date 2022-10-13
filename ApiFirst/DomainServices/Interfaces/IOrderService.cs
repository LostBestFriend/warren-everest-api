using DomainModels.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainServices.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<Order> GetAll();
        int GetQuotesAvaliable(long portfolioId, long productId);
        Task<Order> GetByIdAsync(long id);
        Task<long> CreateAsync(Order model);
        IList<Order> GetExecutableOrders();
        void Update(Order model);
        void Delete(long id);
    }
}
