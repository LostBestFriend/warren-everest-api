using System.Threading.Tasks;

namespace DomainServices.Interfaces
{
    public interface ICustomerBankInfoService
    {
        Task CreateAsync(long customerId);
        Task<decimal> GetBalanceAsync(long customerId);
        Task DepositAsync(long customerId, decimal amount);
        Task WithdrawAsync(long customerId, decimal amount);
        Task DeleteAsync(long customerId);
    }
}
