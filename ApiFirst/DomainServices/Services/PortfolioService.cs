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
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _repositoryFactory = repository ?? (IRepositoryFactory)_unitOfWork;
        }

        public decimal GetAccountBalance(long portfolioId)
        {
            var repository = _repositoryFactory.Repository<Portfolio>();

            if (!repository.Any(portfolio => portfolio.Id == portfolioId))
            {
                throw new ArgumentNullException($"Cliente não encontrato para o id {portfolioId}");
            }

            var query = repository.SingleResultQuery().AndFilter(portfolio => portfolio.Id == portfolioId).Select(portfolio => portfolio.AccountBalance);

            var accountBalance = repository.FirstOrDefault(query);

            return accountBalance;
        }

        public async Task<long> CreateAsync(Portfolio model)
        {
            var repository = _unitOfWork.Repository<Portfolio>();
            await repository.AddAsync(model).ConfigureAwait(false);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
            return model.Id;
        }

        public IEnumerable<Portfolio> GetAll()
        {
            var repository = _repositoryFactory.Repository<Portfolio>();

            var query = repository.MultipleResultQuery()
                .Include(source => source
                .Include(portfolio => portfolio.Orders)
                .Include(portfolio => portfolio.Customer)
                .Include(portfolio => portfolio.PortfolioProducts)
                .ThenInclude(pp => pp.Product)
                .Include(portfolio => portfolio.Products));


            var portfolios = repository.Search(query);
            return portfolios;
        }

        public async Task<Portfolio> GetByIdAsync(long id)
        {
            var repository = _unitOfWork.Repository<Portfolio>();

            var query = repository.SingleResultQuery().AndFilter(portfolio => portfolio.Id == id)
                .Include(source => source.Include(portfolio => portfolio.Products)
                .Include(portfolio => portfolio.Orders)
                .Include(portfolio => portfolio.Customer));

            var portfolio = await repository.SingleOrDefaultAsync(query);

            if (portfolio is null)
            {
                throw new ArgumentNullException($"Carteirar não encontrada para o id: {id}");
            }
            return portfolio;
        }

        public void Deposit(decimal amount, long portfolioId)
        {
            var repository = _unitOfWork.Repository<Portfolio>();

            if (!repository.Any(portfolio => portfolio.Id == portfolioId))
            {
                throw new ArgumentNullException($"Carteira não encontrada para o id: {portfolioId}");
            }

            var query = repository.SingleResultQuery().AndFilter(portfolio => portfolio.Id == portfolioId);

            var portfolio = repository.SingleOrDefault(query);

            portfolio.AccountBalance += amount;
            repository.Update(portfolio);
            _unitOfWork.SaveChanges();
        }

        public void Withdraw(decimal amount, long portfolioId)
        {
            var repository = _unitOfWork.Repository<Portfolio>();

            if (!repository.Any(portfolio => portfolio.Id == portfolioId))
            {
                throw new ArgumentNullException($"Carteira não encontrada para o id: {portfolioId}");
            }

            var query = repository.SingleResultQuery().AndFilter(portfolio => portfolio.Id == portfolioId);

            var portfolio = repository.SingleOrDefault(query);

            if (portfolio.AccountBalance < amount)
            {
                throw new ArgumentException("Não há saldo suficiente para o saque");
            }

            portfolio.AccountBalance -= amount;
            repository.Update(portfolio);
            _unitOfWork.SaveChanges();
        }

        public void DepositAccountBalance(decimal amount, long portfolioId)
        {
            var repository = _unitOfWork.Repository<Portfolio>();

            if (!repository.Any(portfolio => portfolio.Id == portfolioId))
            {
                throw new ArgumentNullException($"Carteira não encontrada para o id: {portfolioId}");
            }

            var query = repository.SingleResultQuery().AndFilter(portfolio => portfolio.Id == portfolioId);

            var portfolio = repository.SingleOrDefault(query);

            if (portfolio.AccountBalance < amount)
            {
                throw new ArgumentException("Não há saldo suficiente para o saque");
            }

            portfolio.AccountBalance += amount;
            repository.Update(portfolio, portfolio => portfolio.AccountBalance);
            _unitOfWork.SaveChanges();
        }

        public void WithdrawAccountBalance(decimal amount, long portfolioId)
        {
            var repository = _unitOfWork.Repository<Portfolio>();

            if (!repository.Any(portfolio => portfolio.Id == portfolioId))
            {
                throw new ArgumentNullException($"Carteira não encontrada para o id: {portfolioId}");
            }

            var query = repository.SingleResultQuery().AndFilter(portfolio => portfolio.Id == portfolioId);

            var portfolio = repository.SingleOrDefault(query);

            if (portfolio.AccountBalance < amount)
            {
                throw new ArgumentException("Não há saldo suficiente para o saque");
            }

            portfolio.AccountBalance -= amount;
            repository.Update(portfolio, portfolio => portfolio.AccountBalance);
            _unitOfWork.SaveChanges();
        }

        public void ExecuteBuyOrder(decimal amount, long portfolioId)
        {
            var repository = _unitOfWork.Repository<Portfolio>();

            if (!repository.Any(portfolio => portfolio.Id == portfolioId))
            {
                throw new ArgumentNullException($"Carteira não encontrada para o id: {portfolioId}");
            }

            var query = repository.SingleResultQuery().AndFilter(portfolio => portfolio.Id == portfolioId);

            var portfolio = repository.SingleOrDefault(query);
            portfolio.TotalBalance += amount;
            portfolio.AccountBalance -= amount;
            repository.Update(portfolio);
            _unitOfWork.SaveChanges();
        }

        public void ExecuteSellOrder(decimal amount, long portfolioId)
        {
            var repository = _unitOfWork.Repository<Portfolio>();

            if (!repository.Any(portfolio => portfolio.Id == portfolioId))
            {
                throw new ArgumentNullException($"Carteira não encontrada para o id: {portfolioId}");
            }

            var query = repository.SingleResultQuery().AndFilter(portfolio => portfolio.Id == portfolioId);

            var portfolio = repository.SingleOrDefault(query);
            portfolio.TotalBalance -= amount;
            portfolio.AccountBalance += amount;
            repository.Update(portfolio, portfolio => portfolio.TotalBalance);
            _unitOfWork.SaveChanges();
        }

        public void Delete(long portfolioId)
        {
            var repository = _unitOfWork.Repository<Portfolio>();

            var query = repository.SingleResultQuery().AndFilter(portfolio => portfolio.Id == portfolio.Id);

            var portfolio = repository.SingleOrDefault(query);

            if (portfolio is null)
            {
                throw new ArgumentNullException($"Não encontrada carteira para o Id: {portfolioId}");
            }

            if (portfolio.AccountBalance > 0) throw new ArgumentException($"Não é possível excluir a carteira enquanto houver saldo");

            if (portfolio.Products.Count > 0) throw new ArgumentException($"Não é possível exluir a carteira enquanto houver produtos nela investidos");

            repository.Remove(portfolio);
            _unitOfWork.SaveChanges();
        }
    }
}
