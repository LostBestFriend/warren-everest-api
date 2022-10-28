using AppModels.AppModels.Orders;
using AppModels.AppModels.Portfolios;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppServices.Interfaces
{
    public interface IPortfolioAppService
    {
        Task<long> CreateAsync(CreatePortfolio model);
        Task<IEnumerable<PortfolioResponse>> GetAllAsync();
        Task<PortfolioResponse> GetByIdAsync(long id);
        Task<decimal> GetAccountBalanceAsync(long portfolioId);
        Task DepositAsync(decimal amount, long customerId, long portfolioId);
        Task WithdrawAsync(decimal amount, long customerId, long portfolioId);
        Task ExecuteNowOrdersAsync();
        Task ExecuteBuyOrderAsync(OrderResponse order);
        Task ExecuteSellOrderAsync(OrderResponse order);
        Task InvestAsync(int quotes, DateTime liquidateAt, long productId, long portfolioId);
        Task WithdrawProductAsync(int quotes, DateTime liquidateAt, long productId, long portfolioId);
        Task DeleteAsync(long portfolioId);
    }
}
