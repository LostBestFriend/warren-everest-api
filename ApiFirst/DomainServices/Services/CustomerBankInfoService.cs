using DomainModels.Models;
using DomainServices.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Infrastructure.Data.Context;
using System;
using System.Threading.Tasks;

namespace DomainServices.Services
{
    public class CustomerBankInfoService : ICustomerBankInfoService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerBankInfoService(IUnitOfWork<WarrenContext> unitOfWork, IRepositoryFactory<WarrenContext> repository)
        {
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
            _repositoryFactory = repository ?? (IRepositoryFactory)_unitOfWork;
        }

        public async Task CreateAsync(long customerId)
        {
            var repository = _unitOfWork.Repository<CustomerBankInfo>();

            await repository.AddAsync(new CustomerBankInfo(customerId)).ConfigureAwait(false);
            _unitOfWork.SaveChanges();
        }

        public async Task DeleteAsync(long customerId)
        {
            var repository = _unitOfWork.Repository<CustomerBankInfo>();

            await repository.RemoveAsync(bankInfo => bankInfo.CustomerId == customerId);
        }

        public async Task<decimal> GetBalanceAsync(long customerId)
        {
            var repository = _repositoryFactory.Repository<CustomerBankInfo>();

            if (!repository.Any(bankinfo => bankinfo.CustomerId == customerId))

                throw new ArgumentNullException($"Cliente não encontrado para o Id {customerId}");

            var query = repository.SingleResultQuery().AndFilter(bankinfo => bankinfo.CustomerId == customerId).Select(bankinfo => bankinfo.AccountBalance);
            var accountBalance = await repository.FirstOrDefaultAsync(query);

            return accountBalance;
        }

        public async Task DepositAsync(long customerId, decimal amount)
        {
            var repository = _unitOfWork.Repository<CustomerBankInfo>();
            var customerBankInfo = await GetByCustomerIdAsync(customerId).ConfigureAwait(false);

            customerBankInfo.AccountBalance += amount;
            repository.Update(customerBankInfo, bankinfo => bankinfo.AccountBalance);
            _unitOfWork.SaveChanges();
        }

        public async Task<CustomerBankInfo> GetByCustomerIdAsync(long customerId)
        {
            var repository = _repositoryFactory.Repository<CustomerBankInfo>();
            var query = repository.SingleResultQuery().AndFilter(bankinfo => bankinfo.CustomerId == customerId);
            var customerBankInfo = await repository.FirstOrDefaultAsync(query);

            if (customerBankInfo is null)
                throw new ArgumentNullException($"Cliente não encontrato para o id {customerId}");

            return customerBankInfo;
        }

        public async Task WithdrawAsync(long customerId, decimal amount)
        {
            var repository = _unitOfWork.Repository<CustomerBankInfo>();
            var customerBankInfo = await GetByCustomerIdAsync(customerId).ConfigureAwait(false);

            if (customerBankInfo.AccountBalance < amount)
                throw new ArgumentException("Não é possível sacar o valor informado pois não há saldo suficiente");

            customerBankInfo.AccountBalance -= amount;
            repository.Update(customerBankInfo, bankinfo => bankinfo.AccountBalance);
            _unitOfWork.SaveChanges();
        }
    }
}
