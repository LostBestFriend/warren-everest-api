namespace DomainServices.Interfaces
{
    public interface ICustomerBankInfoService
    {
        void Create(long customerId);
        decimal GetBalance(long customerId);
        void DepositAsync(long customerId, decimal amount);
        void WithdrawAsync(long customerId, decimal amount);
        void DeleteAsync(long customerId);
    }
}
