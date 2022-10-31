using DomainModels.Models;
using DomainServices.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainServices.Services
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IUnitOfWork _unitOfWork;

        public PortfolioService(IUnitOfWork<WarrenContext> unitOfWork, IRepositoryFactory<WarrenContext> repository)
        {
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
            _repositoryFactory = repository ?? (IRepositoryFactory)_unitOfWork;
        }

        public async Task<decimal> GetAccountBalanceAsync(long portfolioId)
        {
            var repository = _repositoryFactory.Repository<Portfolio>();

            if (!repository.Any(portfolio => portfolio.Id == portfolioId))
                throw new ArgumentNullException($"Cliente não encontrado para o id {portfolioId}");

            var query = repository.SingleResultQuery().AndFilter(portfolio => portfolio.Id == portfolioId).Select(portfolio => portfolio.AccountBalance);
            var accountBalance = await repository.FirstOrDefaultAsync(query);

            return accountBalance;
        }

        public async Task<long> CreateAsync(Portfolio model)
        {
            var repository = _unitOfWork.Repository<Portfolio>();

            await repository.AddAsync(model).ConfigureAwait(false);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
            return model.Id;
        }

        public async Task<IEnumerable<Portfolio>> GetAllAsync()
        {
            var repository = _repositoryFactory.Repository<Portfolio>();
            var query = repository.MultipleResultQuery()
                .Include(source => source
                .Include(portfolio => portfolio.Orders)
                .Include(portfolio => portfolio.Customer)
                .Include(portfolio => portfolio.PortfolioProducts)
                .ThenInclude(pp => pp.Product)
                .Include(portfolio => portfolio.Products));
            var portfolios = await repository.SearchAsync(query);

            return portfolios;
        }

        public async Task<Portfolio> GetByIdAsync(long id)
        {
            var repository = _repositoryFactory.Repository<Portfolio>();
            var query = repository.SingleResultQuery().AndFilter(portfolio => portfolio.Id == id)
                .Include(source => source.Include(portfolio => portfolio.Products)
                .Include(portfolio => portfolio.Orders)
                .Include(portfolio => portfolio.Customer));
            var portfolio = await repository.SingleOrDefaultAsync(query);

            if (portfolio is null)
                throw new ArgumentNullException($"Carteira não encontrada para o Id: {id}");

            return portfolio;
        }

        public async Task DepositAsync(decimal amount, long portfolioId)
        {
            var repository = _unitOfWork.Repository<Portfolio>();

            var portfolio = await GetByIdAsync(portfolioId).ConfigureAwait(false);

            portfolio.TotalBalance += amount;
            repository.Update(portfolio);
            _unitOfWork.SaveChanges();
        }

        public async Task WithdrawAsync(decimal amount, long portfolioId)
        {
            var repository = _unitOfWork.Repository<Portfolio>();

            var portfolio = await GetByIdAsync(portfolioId).ConfigureAwait(false);

            if (portfolio.AccountBalance < amount)
                throw new ArgumentException("Não há saldo suficiente para o saque");

            portfolio.TotalBalance -= amount;
            repository.Update(portfolio);
            _unitOfWork.SaveChanges();
        }

        public async Task DepositAccountBalanceAsync(decimal amount, long portfolioId)
        {
            var repository = _unitOfWork.Repository<Portfolio>();

            var portfolio = await GetByIdAsync(portfolioId).ConfigureAwait(false);

            if (portfolio.AccountBalance < amount)
                throw new ArgumentException("Não há saldo suficiente para o depósito");

            portfolio.AccountBalance += amount;
            repository.Update(portfolio, portfolio => portfolio.AccountBalance);
            _unitOfWork.SaveChanges();
        }

        public async Task WithdrawAccountBalanceAsync(decimal amount, long portfolioId)
        {
            var repository = _unitOfWork.Repository<Portfolio>();

            var portfolio = await GetByIdAsync(portfolioId).ConfigureAwait(false);

            if (portfolio.AccountBalance < amount)
                throw new ArgumentException("Não há saldo suficiente para o saque");

            portfolio.AccountBalance -= amount;
            repository.Update(portfolio, portfolio => portfolio.AccountBalance);
            _unitOfWork.SaveChanges();
        }

        public async Task ExecuteBuyOrderAsync(decimal amount, long portfolioId)
        {
            var repository = _unitOfWork.Repository<Portfolio>();

            var portfolio = await GetByIdAsync(portfolioId).ConfigureAwait(false);

            portfolio.TotalBalance += amount;
            portfolio.AccountBalance -= amount;
            repository.Update(portfolio);
            _unitOfWork.SaveChanges();
        }

        public async Task ExecuteSellOrderAsync(decimal amount, long portfolioId)
        {
            var repository = _unitOfWork.Repository<Portfolio>();

            var portfolio = await GetByIdAsync(portfolioId).ConfigureAwait(false);

            portfolio.TotalBalance -= amount;
            portfolio.AccountBalance += amount;
            repository.Update(portfolio, portfolio => portfolio.TotalBalance);
            _unitOfWork.SaveChanges();
        }

        public async Task DeleteAsync(long portfolioId)
        {
            var repository = _unitOfWork.Repository<Portfolio>();
            var portfolio = await GetByIdAsync(portfolioId).ConfigureAwait(false);

            if (portfolio.AccountBalance > 0)
                throw new ArgumentException($"Não é possível excluir a carteira enquanto houver saldo");

            if (portfolio.Products is not null)
                throw new ArgumentException($"Não é possível excluir a carteira enquanto houver produtos nela investidos");

            repository.Remove(portfolio);
            _unitOfWork.SaveChanges();
        }
    }
}
