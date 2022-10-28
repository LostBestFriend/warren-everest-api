using AppModels.AppModels.Orders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppServices.Interfaces
{
    public interface IOrderAppService
    {
        Task<long> CreateAsync(CreateOrder model);
        Task<IEnumerable<OrderResponse>> GetAllAsync();
        Task<OrderResponse> GetByIdAsync(long id);
        Task<IEnumerable<OrderResponse>> GetExecutableOrdersAsync();
        Task<int> GetQuotesAvaliableAsync(long portfolioId, long productId);
        void Update(long id, UpdateOrder model);
    }
}
