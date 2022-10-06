using AppServices.Interfaces;
using DomainServices.Interfaces;
using System;

namespace AppServices.Services
{
    public class CustomerBankInfoAppService : ICustomerBankInfoAppService
    {
        private readonly ICustomerBankInfoService _customerBankInfoServices;

        public CustomerBankInfoAppService(ICustomerBankInfoService customerBankInfoServices)
        {
            _customerBankInfoServices = customerBankInfoServices ?? throw new ArgumentNullException(nameof(customerBankInfoServices));
        }

        public void Create(long customerId)
        {
            _customerBankInfoServices.Create(customerId);
        }

        public void Deposit(long customerId, decimal amount)
        {
            _customerBankInfoServices.Deposit(customerId, amount);
        }

        public decimal GetBalance(long customerId)
        {
            return _customerBankInfoServices.GetBalance(customerId);
        }

        public void Withdraw(long customerId, decimal amount)
        {
            _customerBankInfoServices.Withdraw(customerId, amount);
        }
    }
}
