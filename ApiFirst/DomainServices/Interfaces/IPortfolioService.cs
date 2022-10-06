using DomainModels.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainServices.Interfaces
{
    public interface IPortfolioService
    {
        Task<long> CreateAsync(Portfolio model);
        IEnumerable<Portfolio> GetAll();
        Task<Portfolio> GetByIdAsync(long id);
        public decimal GetAccountBalance(long portfolioId);
        void Deposit(decimal amount, long portfolioId);
        void Withdraw(decimal amount, long portfolioId);
        void DepositAccountBalance(decimal amount, long portfolioId);
        void WithdrawAccountBalance(decimal amount, long portfolioId);
        void ExecuteBuyOrder(decimal amount, long portfolioId);
        void ExecuteSellOrder(decimal amount, long portfolioId);
        void Delete(long portfolioId);
    }
}
