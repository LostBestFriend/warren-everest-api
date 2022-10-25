using DomainModels.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainServices.Interfaces
{
    public interface IPortfolioService
    {
        Task<long> CreateAsync(Portfolio model);
        Task<IEnumerable<Portfolio>> GetAllAsync();
        Task<Portfolio> GetByIdAsync(long id);
        Task<decimal> GetAccountBalanceAsync(long portfolioId);
        void DepositAsync(decimal amount, long portfolioId);
        Task WithdrawAsync(decimal amount, long portfolioId);
        Task DepositAccountBalanceAsync(decimal amount, long portfolioId);
        Task WithdrawAccountBalanceAsync(decimal amount, long portfolioId);
        void ExecuteBuyOrderAsync(decimal amount, long portfolioId);
        void ExecuteSellOrderAsync(decimal amount, long portfolioId);
        Task DeleteAsync(long portfolioId);
    }
}
