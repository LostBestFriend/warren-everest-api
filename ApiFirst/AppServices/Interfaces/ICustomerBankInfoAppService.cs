using System.Threading.Tasks;

namespace AppServices.Interfaces
{
    public interface ICustomerBankInfoAppService
    {
        Task CreateAsync(long customerId);
        Task<decimal> GetBalanceAsync(long customerId);
        Task DepositAsync(long customerId, decimal amount);
        Task WithdrawAsync(long customerId, decimal amount);
        Task DeleteAsync(long customerId);
    }
}
