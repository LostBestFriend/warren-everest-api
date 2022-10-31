using AppServices.Interfaces;
using DomainServices.Interfaces;
using System;
using System.Threading.Tasks;

namespace AppServices.Services
{
    public class CustomerBankInfoAppService : ICustomerBankInfoAppService
    {
        private readonly ICustomerBankInfoService _customerBankInfoServices;

        public CustomerBankInfoAppService(ICustomerBankInfoService customerBankInfoServices)
        {
            _customerBankInfoServices = customerBankInfoServices ??
                throw new ArgumentNullException(nameof(customerBankInfoServices));
        }

        public async Task CreateAsync(long customerId)
        {
            await _customerBankInfoServices.CreateAsync(customerId).ConfigureAwait(false);
        }

        public async Task DepositAsync(long customerId, decimal amount)
        {
            await _customerBankInfoServices.DepositAsync(customerId, amount).ConfigureAwait(false);
        }

        public async Task<decimal> GetBalanceAsync(long customerId)
        {
            return await _customerBankInfoServices.GetBalanceAsync(customerId).ConfigureAwait(false);
        }

        public async Task WithdrawAsync(long customerId, decimal amount)
        {
            await _customerBankInfoServices.WithdrawAsync(customerId, amount).ConfigureAwait(false);
        }

        public async Task DeleteAsync(long customerId)
        {
            await _customerBankInfoServices.DeleteAsync(customerId).ConfigureAwait(false);
        }
    }
}
