using AppModels.AppModels.Order;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppServices.Interfaces
{
    public interface IOrderAppService
    {
        Task<long> CreateAsync(CreateOrder model);
        IEnumerable<OrderResponse> GetAll();
        Task<OrderResponse> GetByIdAsync(long id);
        IEnumerable<OrderResponse> GetExecutableOrders();
        int GetQuotesAvaliable(long portfolioId, long productId);
        void Update(long id, UpdateOrder model);
        void Delete(long id);
    }
}
