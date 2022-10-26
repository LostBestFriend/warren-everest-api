using System.Threading.Tasks;

namespace DomainServices.Interfaces
{
    public interface ICustomerBankInfoService
    {
        void Create(long customerId);
        decimal GetBalance(long customerId);
        Task DepositAsync(long customerId, decimal amount);
        Task WithdrawAsync(long customerId, decimal amount);
        Task DeleteAsync(long customerId);
    }
}
