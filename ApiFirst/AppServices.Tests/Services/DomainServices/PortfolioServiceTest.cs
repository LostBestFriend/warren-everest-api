﻿using ApiFirst.Tests.Fixtures.DomainServices;
using DomainModels.Models;
using DomainServices.Services;
using EntityFrameworkCore.QueryBuilder.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using FluentAssertions;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace ApiFirst.Tests.Services.DomainServices
{
    public class PortfolioServiceTest
    {
        readonly PortfolioService _portfolioService;
        readonly IUnitOfWork<WarrenContext> _unitOfWork;
        readonly IRepositoryFactory<WarrenContext> _repositoryFactory;
        readonly Mock<IRepositoryFactory<WarrenContext>> _repositoryFactoryMock;
        readonly Mock<IUnitOfWork<WarrenContext>> _unitOfWorkMock;

        public PortfolioServiceTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork<WarrenContext>>();
            _unitOfWork = _unitOfWorkMock.Object;
            _repositoryFactoryMock = new Mock<IRepositoryFactory<WarrenContext>>();
            _repositoryFactory = _repositoryFactoryMock.Object;

            _portfolioService = new PortfolioService(_unitOfWork, _repositoryFactory);
        }

        [Fact]
        public async void Should_GetAccountBalance_Sucessfully()
        {
            var portfolioId = 1;
            var balance = 1000;

            _repositoryFactoryMock.Setup(p => p.Repository<Portfolio>().Any(It.IsAny<Expression<Func<Portfolio, bool>>>())).Returns(true);

            _repositoryFactoryMock.Setup(p => p.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>()).Select(It.IsAny<Expression<Func<Portfolio, decimal>>>())).Returns(It.IsAny<IQuery<Portfolio, decimal>>());

            _repositoryFactoryMock.Setup(p => p.Repository<Portfolio>().FirstOrDefaultAsync(It.IsAny<IQuery<Portfolio, decimal>>(), default)).ReturnsAsync(balance);

            var result = await _portfolioService.GetAccountBalanceAsync(portfolioId);

            result.Should().Be(balance);

            _repositoryFactoryMock.Verify(p => p.Repository<Portfolio>().Any(It.IsAny<Expression<Func<Portfolio, bool>>>()), Times.Once);

            _repositoryFactoryMock.Verify(p => p.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>()).Select(It.IsAny<Expression<Func<Portfolio, decimal>>>()), Times.Once);

            _repositoryFactoryMock.Verify(p => p.Repository<Portfolio>().FirstOrDefaultAsync(It.IsAny<IQuery<Portfolio, decimal>>(), default), Times.Once);
        }

        [Fact]
        public async void Should_Not_GetAccountBalance_When_Id_Dismatch()
        {
            var portfolioId = 1;
            var balance = 1000;

            _repositoryFactoryMock.Setup(p => p.Repository<Portfolio>().Any(It.IsAny<Expression<Func<Portfolio, bool>>>())).Returns(false);

            _repositoryFactoryMock.Setup(p => p.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>()).Select(It.IsAny<Expression<Func<Portfolio, decimal>>>())).Returns(It.IsAny<IQuery<Portfolio, decimal>>());

            _repositoryFactoryMock.Setup(p => p.Repository<Portfolio>().FirstOrDefaultAsync(It.IsAny<IQuery<Portfolio, decimal>>(), default)).ReturnsAsync(balance);

            var action = () => _portfolioService.GetAccountBalanceAsync(portfolioId);

            await action.Should().ThrowAsync<ArgumentNullException>($"Cliente não encontrado para o id {portfolioId}");

            _repositoryFactoryMock.Verify(p => p.Repository<Portfolio>().Any(It.IsAny<Expression<Func<Portfolio, bool>>>()), Times.Once);

            _repositoryFactoryMock.Verify(p => p.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>()).Select(It.IsAny<Expression<Func<Portfolio, decimal>>>()), Times.Never);

            _repositoryFactoryMock.Verify(p => p.Repository<Portfolio>().FirstOrDefaultAsync(It.IsAny<IQuery<Portfolio, decimal>>(), default), Times.Never);
        }

        [Fact]
        public async void Should_CreateAsync_Sucessfully()
        {
            var portfolio = PortfolioFixture.GeneratePortfolioFixture();

            _unitOfWorkMock.Setup(p => p.Repository<Portfolio>().AddAsync(It.IsAny<Portfolio>(), default));

            _unitOfWorkMock.Setup(p => p.SaveChangesAsync(true, false, default));

            var result = await _portfolioService.CreateAsync(portfolio);

            result.Should().Be(portfolio.Id);

            _unitOfWorkMock.Verify(p => p.Repository<Portfolio>().AddAsync(It.IsAny<Portfolio>(), default), Times.Once);

            _unitOfWorkMock.Verify(p => p.SaveChangesAsync(true, false, default), Times.Once);
        }

        [Fact]
        public async void Should_GetAll_Sucessfully()
        {
            var portfolios = PortfolioFixture.GeneratePortfolioFixture(3);
            _repositoryFactoryMock.Setup(p => p.Repository<Portfolio>()
                .MultipleResultQuery().Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>())).Returns(It.IsAny<IQuery<Portfolio>>());

            _repositoryFactoryMock.Setup(p => p.Repository<Portfolio>().SearchAsync(It.IsAny<IQuery<Portfolio>>(), default)).ReturnsAsync(portfolios);

            var result = await _portfolioService.GetAllAsync();
            result.Should().HaveCount(3);

            _repositoryFactoryMock.Verify(p => p.Repository<Portfolio>()
                .MultipleResultQuery().Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>()), Times.Once);

            _repositoryFactoryMock.Verify(p => p.Repository<Portfolio>().SearchAsync(It.IsAny<IQuery<Portfolio>>(), default), Times.Once);
        }

        [Fact]
        public async void Should_GetByIdAsync_Sucessfully()
        {
            var portfolioId = 1;
            var portfolio = PortfolioFixture.GeneratePortfolioFixture();

            _repositoryFactoryMock.Setup(p => p.Repository<Portfolio>()
                .SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>()).Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>())).Returns(It.IsAny<IQuery<Portfolio>>());

            _repositoryFactoryMock.Setup(p => p.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default)).ReturnsAsync(portfolio);

            var result = await _portfolioService.GetByIdAsync(portfolioId);

            result.Should().Be(portfolio);

            _repositoryFactoryMock.Verify(p => p.Repository<Portfolio>()
                .SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>()).Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>()), Times.Once);

            _repositoryFactoryMock.Verify(p => p.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default), Times.Once);
        }

        [Fact]
        public async void Should_Not_GetByIdAsync_When_Id_Dismatch()
        {
            var portfolioId = 1;
            var portfolio = PortfolioFixture.GeneratePortfolioFixture();

            _repositoryFactoryMock.Setup(p => p.Repository<Portfolio>()
                .SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>()).Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>())).Returns(It.IsAny<IQuery<Portfolio>>());

            _repositoryFactoryMock.Setup(p => p.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default));

            var action = () => _portfolioService.GetByIdAsync(portfolioId);

            await action.Should().ThrowAsync<ArgumentNullException>($"Carteira não encontrada para o Id: {portfolioId}");

            _repositoryFactoryMock.Verify(p => p.Repository<Portfolio>()
                .SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>()).Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>()), Times.Once);

            _repositoryFactoryMock.Verify(p => p.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default), Times.Once);
        }

        [Fact]
        public async void Should_Deposit_Sucessfully()
        {
            var portfolio = PortfolioFixture.GeneratePortfolioFixture();
            long portfolioId = 1;
            decimal amount = 100;

            _repositoryFactoryMock.Setup(p => p.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>()).Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>())).Returns(It.IsAny<IQuery<Portfolio>>());

            _repositoryFactoryMock.Setup(p => p.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default)).ReturnsAsync(portfolio);

            _unitOfWorkMock.Setup(p => p.Repository<Portfolio>().Update(It.IsAny<Portfolio>()));

            _unitOfWorkMock.Setup(p => p.SaveChanges(true, false));

            await _portfolioService.DepositAsync(amount, portfolioId);

            _repositoryFactoryMock.Verify(p => p.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>()).Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>()), Times.Once);

            _repositoryFactoryMock.Verify(p => p.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default), Times.Once);

            _unitOfWorkMock.Verify(p => p.Repository<Portfolio>().Update(It.IsAny<Portfolio>()), Times.Once);

            _unitOfWorkMock.Verify(p => p.SaveChanges(true, false), Times.Once);
        }

        [Fact]
        public async void Should_DepositAccountBalance_Sucessfully()
        {
            var portfolio = PortfolioFixture.GeneratePortfolioFixture();
            portfolio.AccountBalance = 1000;
            long portfolioId = 1;
            decimal amount = 100;

            _repositoryFactoryMock.Setup(p => p.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>()).Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>())).Returns(It.IsAny<IQuery<Portfolio>>());

            _repositoryFactoryMock.Setup(p => p.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default)).ReturnsAsync(portfolio);

            _unitOfWorkMock.Setup(p => p.Repository<Portfolio>().Update(It.IsAny<Portfolio>(), It.IsAny<Expression<Func<Portfolio, object>>>()));

            _unitOfWorkMock.Setup(p => p.SaveChanges(true, false));

            await _portfolioService.DepositAccountBalanceAsync(amount, portfolioId);

            _repositoryFactoryMock.Verify(p => p.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>()).Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>()), Times.Once);

            _repositoryFactoryMock.Verify(p => p.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default), Times.Once);

            _unitOfWorkMock.Verify(p => p.Repository<Portfolio>().Update(It.IsAny<Portfolio>(), It.IsAny<Expression<Func<Portfolio, object>>>()), Times.Once);

            _unitOfWorkMock.Verify(p => p.SaveChanges(true, false), Times.Once);
        }

        [Fact]
        public async void Should_Not_DepositAccountBalance_When_Balance_Is_Lesser_Than_Amount()
        {
            var portfolio = PortfolioFixture.GeneratePortfolioFixture();
            long portfolioId = 1;
            decimal amount = 10000;

            _repositoryFactoryMock.Setup(p => p.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>()).Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>())).Returns(It.IsAny<IQuery<Portfolio>>());

            _repositoryFactoryMock.Setup(p => p.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default)).ReturnsAsync(portfolio);

            _unitOfWorkMock.Setup(p => p.Repository<Portfolio>().Update(It.IsAny<Portfolio>(), It.IsAny<Expression<Func<Portfolio, object>>>()));

            _unitOfWorkMock.Setup(p => p.SaveChanges(true, false));

            var action = () => _portfolioService.DepositAccountBalanceAsync(amount, portfolioId);

            await action.Should().ThrowAsync<ArgumentException>("Não há saldo suficiente para o depósito");

            _repositoryFactoryMock.Verify(p => p.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>()).Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>()), Times.Once);

            _repositoryFactoryMock.Verify(p => p.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default), Times.Once);

            _unitOfWorkMock.Verify(p => p.Repository<Portfolio>().Update(It.IsAny<Portfolio>(), It.IsAny<Expression<Func<Portfolio, object>>>()), Times.Never);

            _unitOfWorkMock.Verify(p => p.SaveChanges(true, false), Times.Never);
        }

        [Fact]
        public async void Should_Withdraw_Sucessfully()
        {
            var portfolio = PortfolioFixture.GeneratePortfolioFixture();
            portfolio.AccountBalance = 1000;
            long portfolioId = 1;
            decimal amount = 100;

            _repositoryFactoryMock.Setup(p => p.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>()).Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>())).Returns(It.IsAny<IQuery<Portfolio>>());

            _repositoryFactoryMock.Setup(p => p.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default)).ReturnsAsync(portfolio);

            _unitOfWorkMock.Setup(p => p.Repository<Portfolio>().Update(It.IsAny<Portfolio>()));

            _unitOfWorkMock.Setup(p => p.SaveChanges(true, false));

            await _portfolioService.WithdrawAsync(amount, portfolioId);

            _repositoryFactoryMock.Verify(p => p.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>()).Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>()), Times.Once);

            _repositoryFactoryMock.Verify(p => p.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default), Times.Once);

            _unitOfWorkMock.Verify(p => p.Repository<Portfolio>().Update(It.IsAny<Portfolio>()), Times.Once);

            _unitOfWorkMock.Verify(p => p.SaveChanges(true, false), Times.Once);
        }

        [Fact]
        public async void Should_Not_Withdraw_When_Balance_Is_Lesser_Than_Amount()
        {
            var portfolio = PortfolioFixture.GeneratePortfolioFixture();
            long portfolioId = 1;
            decimal amount = 10000;

            _repositoryFactoryMock.Setup(p => p.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>()).Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>())).Returns(It.IsAny<IQuery<Portfolio>>());

            _repositoryFactoryMock.Setup(p => p.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default)).ReturnsAsync(portfolio);

            _unitOfWorkMock.Setup(p => p.Repository<Portfolio>().Update(It.IsAny<Portfolio>()));

            _unitOfWorkMock.Setup(p => p.SaveChanges(true, false)); _unitOfWorkMock.Setup(p => p.Repository<Portfolio>().Any(It.IsAny<Expression<Func<Portfolio, bool>>>())).Returns(true);

            var action = () => _portfolioService.WithdrawAsync(amount, portfolioId);

            await action.Should().ThrowAsync<ArgumentException>("Não há saldo suficiente para o saque");

            _repositoryFactoryMock.Verify(p => p.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>()).Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>()), Times.Once);

            _repositoryFactoryMock.Verify(p => p.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default), Times.Once);

            _unitOfWorkMock.Verify(p => p.Repository<Portfolio>().Update(It.IsAny<Portfolio>()), Times.Never);

            _unitOfWorkMock.Verify(p => p.SaveChanges(true, false), Times.Never);
        }

        [Fact]
        public async void Should_WithdrawAccountBalance_Sucessfully()
        {
            var portfolio = PortfolioFixture.GeneratePortfolioFixture();
            portfolio.AccountBalance = 1000;
            long portfolioId = 1;
            decimal amount = 100;

            _repositoryFactoryMock.Setup(p => p.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>()).Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>())).Returns(It.IsAny<IQuery<Portfolio>>());

            _repositoryFactoryMock.Setup(p => p.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default)).ReturnsAsync(portfolio);

            _unitOfWorkMock.Setup(p => p.Repository<Portfolio>().Update(It.IsAny<Portfolio>(), It.IsAny<Expression<Func<Portfolio, object>>>()));

            _unitOfWorkMock.Setup(p => p.SaveChanges(true, false));

            await _portfolioService.WithdrawAccountBalanceAsync(amount, portfolioId);

            _repositoryFactoryMock.Verify(p => p.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>()).Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>()), Times.Once);

            _repositoryFactoryMock.Verify(p => p.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default), Times.Once);

            _unitOfWorkMock.Verify(p => p.Repository<Portfolio>().Update(It.IsAny<Portfolio>(), It.IsAny<Expression<Func<Portfolio, object>>>()), Times.Once);

            _unitOfWorkMock.Verify(p => p.SaveChanges(true, false), Times.Once);
        }

        [Fact]
        public async void Should_Not_WithdrawAccountBalance_When_Balance_Is_Lesser_Than_Amount()
        {
            var portfolio = PortfolioFixture.GeneratePortfolioFixture();
            long portfolioId = 1;
            decimal amount = 10000;

            _repositoryFactoryMock.Setup(p => p.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>()).Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>())).Returns(It.IsAny<IQuery<Portfolio>>());

            _repositoryFactoryMock.Setup(p => p.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default)).ReturnsAsync(portfolio);

            _unitOfWorkMock.Setup(p => p.Repository<Portfolio>().Update(It.IsAny<Portfolio>(), It.IsAny<Expression<Func<Portfolio, object>>>()));

            _unitOfWorkMock.Setup(p => p.SaveChanges(true, false));

            var action = () => _portfolioService.WithdrawAccountBalanceAsync(amount, portfolioId);

            await action.Should().ThrowAsync<ArgumentException>("Não há saldo suficiente para o saque");

            _repositoryFactoryMock.Verify(p => p.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>()).Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>()), Times.Once);

            _repositoryFactoryMock.Verify(p => p.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default), Times.Once);

            _unitOfWorkMock.Verify(p => p.Repository<Portfolio>().Update(It.IsAny<Portfolio>(), It.IsAny<Expression<Func<Portfolio, object>>>()), Times.Never);

            _unitOfWorkMock.Verify(p => p.SaveChanges(true, false), Times.Never);
        }

        [Fact]
        public async void Should_ExecuteSellOrder_Sucessfully()
        {
            var portfolio = PortfolioFixture.GeneratePortfolioFixture();
            portfolio.AccountBalance = 1000;
            long portfolioId = 1;
            decimal amount = 100;

            _repositoryFactoryMock.Setup(p => p.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>()).Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>())).Returns(It.IsAny<IQuery<Portfolio>>());

            _repositoryFactoryMock.Setup(p => p.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default)).ReturnsAsync(portfolio);

            _unitOfWorkMock.Setup(p => p.Repository<Portfolio>().Update(It.IsAny<Portfolio>(), It.IsAny<Expression<Func<Portfolio, object>>>()));

            _unitOfWorkMock.Setup(p => p.SaveChanges(true, false));

            await _portfolioService.ExecuteSellOrderAsync(amount, portfolioId);

            _repositoryFactoryMock.Verify(p => p.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>()).Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>()), Times.Once);

            _repositoryFactoryMock.Verify(p => p.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default), Times.Once);

            _unitOfWorkMock.Verify(p => p.Repository<Portfolio>().Update(It.IsAny<Portfolio>(), It.IsAny<Expression<Func<Portfolio, object>>>()), Times.Once);

            _unitOfWorkMock.Verify(p => p.SaveChanges(true, false), Times.Once);
        }

        [Fact]
        public async void Should_ExecuteBuyOrder_Sucessfully()
        {
            var portfolio = PortfolioFixture.GeneratePortfolioFixture();
            portfolio.AccountBalance = 1000;
            long portfolioId = 1;
            decimal amount = 100;

            _repositoryFactoryMock.Setup(p => p.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>()).Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>())).Returns(It.IsAny<IQuery<Portfolio>>());

            _repositoryFactoryMock.Setup(p => p.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default)).ReturnsAsync(portfolio);

            _unitOfWorkMock.Setup(p => p.Repository<Portfolio>().Update(It.IsAny<Portfolio>()));

            _unitOfWorkMock.Setup(p => p.SaveChanges(true, false));

            await _portfolioService.ExecuteBuyOrderAsync(amount, portfolioId);

            _repositoryFactoryMock.Verify(p => p.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>()).Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>()), Times.Once);

            _repositoryFactoryMock.Verify(p => p.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default), Times.Once);

            _unitOfWorkMock.Verify(p => p.Repository<Portfolio>().Update(It.IsAny<Portfolio>()), Times.Once);

            _unitOfWorkMock.Verify(p => p.SaveChanges(true, false), Times.Once);
        }

        [Fact]
        public async void Should_Delete_Sucessfully()
        {
            long portfolioId = 1;
            var portfolio = PortfolioFixture.GeneratePortfolioFixture();
            portfolio.AccountBalance = 0;

            _repositoryFactoryMock.Setup(p => p.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>()).Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>())).Returns(It.IsAny<IQuery<Portfolio>>());

            _repositoryFactoryMock.Setup(p => p.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default)).ReturnsAsync(portfolio);

            _unitOfWorkMock.Setup(p => p.Repository<Portfolio>().Remove(It.IsAny<Portfolio>()));

            _unitOfWorkMock.Setup(p => p.SaveChanges(true, false));

            await _portfolioService.DeleteAsync(portfolioId);

            _repositoryFactoryMock.Verify(p => p.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>()).Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>()), Times.Once);

            _repositoryFactoryMock.Verify(p => p.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default), Times.Once);

            _unitOfWorkMock.Verify(p => p.Repository<Portfolio>().Remove(It.IsAny<Portfolio>()), Times.Once);

            _unitOfWorkMock.Verify(p => p.SaveChanges(true, false), Times.Once);
        }

        [Fact]
        public async void Should_Not_Delete_When_Balance_Is_Greater_Than_Zero()
        {
            long portfolioId = 1;
            var portfolio = PortfolioFixture.GeneratePortfolioFixture();
            portfolio.AccountBalance = 1000;

            _repositoryFactoryMock.Setup(p => p.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>()).Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>())).Returns(It.IsAny<IQuery<Portfolio>>());

            _repositoryFactoryMock.Setup(p => p.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default)).ReturnsAsync(portfolio);

            _unitOfWorkMock.Setup(p => p.Repository<Portfolio>().Remove(It.IsAny<Portfolio>()));

            _unitOfWorkMock.Setup(p => p.SaveChanges(true, false));

            var action = () => _portfolioService.DeleteAsync(portfolioId);

            await action.Should().ThrowAsync<ArgumentException>("Não é possível excluir a carteira enquanto houver saldo");

            _repositoryFactoryMock.Verify(p => p.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>()).Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>()), Times.Once);

            _repositoryFactoryMock.Verify(p => p.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default), Times.Once);

            _unitOfWorkMock.Verify(p => p.Repository<Portfolio>().Remove(It.IsAny<Portfolio>()), Times.Never);

            _unitOfWorkMock.Verify(p => p.SaveChanges(true, false), Times.Never);
        }

        [Fact]
        public async void Should_Not_Delete_When_Products_Isnt_Null()
        {
            long portfolioId = 1;
            var portfolio = PortfolioFixture.GeneratePortfolioFixture();
            portfolio.AccountBalance = 0;
            portfolio.Products = ProductFixture.GenerateProductFixture(3);

            _repositoryFactoryMock.Setup(p => p.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>()).Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>())).Returns(It.IsAny<IQuery<Portfolio>>());

            _repositoryFactoryMock.Setup(p => p.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default)).ReturnsAsync(portfolio);

            _unitOfWorkMock.Setup(p => p.Repository<Portfolio>().Remove(It.IsAny<Portfolio>()));

            _unitOfWorkMock.Setup(p => p.SaveChanges(true, false));

            var action = () => _portfolioService.DeleteAsync(portfolioId);

            await action.Should().ThrowAsync<ArgumentException>("Não é possível excluir a carteira enquanto houver produtos nela investidos");

            _repositoryFactoryMock.Verify(p => p.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>()).Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>()), Times.Once);

            _repositoryFactoryMock.Verify(p => p.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default), Times.Once);

            _unitOfWorkMock.Verify(p => p.Repository<Portfolio>().Remove(It.IsAny<Portfolio>()), Times.Never);

            _unitOfWorkMock.Verify(p => p.SaveChanges(true, false), Times.Never);
        }
    }
}
