using DomainModels.Models;
using DomainServices.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Infrastructure.Data.Context;
using System;

namespace DomainServices.Services
{
    public class CustomerBankInfoService : ICustomerBankInfoService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerBankInfoService(IUnitOfWork<WarrenContext> unitOfWork, IRepositoryFactory<WarrenContext> repository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _repositoryFactory = repository ?? (IRepositoryFactory)_unitOfWork;
        }

        public void Create(long customerId)
        {
            var repository = _unitOfWork.Repository<CustomerBankInfo>();

            repository.Add(new CustomerBankInfo(customerId));
            _unitOfWork.SaveChanges();
        }

        public decimal GetBalance(long customerId)
        {
            var repository = _repositoryFactory.Repository<CustomerBankInfo>();

            if (!repository.Any(bankinfo => bankinfo.CustomerId == customerId))
            {
                throw new ArgumentNullException($"Cliente não encontrato para o id {customerId}");
            }

            var query = repository.SingleResultQuery().AndFilter(bankinfo => bankinfo.CustomerId == customerId).Select(bankinfo => bankinfo.AccountBalance);

            var accountBalance = repository.FirstOrDefault(query);

            return accountBalance;
        }

        public void Deposit(long customerId, decimal amount)
        {
            var repository = _unitOfWork.Repository<CustomerBankInfo>();

            var query = repository.SingleResultQuery().AndFilter(bankinfo => bankinfo.CustomerId == customerId);

            var bankInfo = repository.FirstOrDefault(query);

            if (bankInfo is null)
            {
                throw new ArgumentNullException($"Cliente não encontrato para o id {customerId}");
            }

            bankInfo.AccountBalance += amount;
            repository.Update(bankInfo, bankinfo => bankinfo.AccountBalance);
            _unitOfWork.SaveChanges();
        }

        public void Withdraw(long customerId, decimal amount)
        {
            var repository = _unitOfWork.Repository<CustomerBankInfo>();

            var query = repository.SingleResultQuery().AndFilter(bankinfo => bankinfo.CustomerId == customerId);

            var bankInfo = repository.FirstOrDefault(query);

            if (bankInfo is null)
            {
                throw new ArgumentNullException($"Cliente não encontrato para o id {customerId}");
            }
            if (bankInfo.AccountBalance < amount)
            {
                throw new ArgumentException($"Não é possível sacar o valor informado pois não há saldo suficiente");
            }

            bankInfo.AccountBalance -= amount;
            repository.Update(bankInfo, bankinfo => bankinfo.AccountBalance);
            _unitOfWork.SaveChanges();
        }
    }
}
