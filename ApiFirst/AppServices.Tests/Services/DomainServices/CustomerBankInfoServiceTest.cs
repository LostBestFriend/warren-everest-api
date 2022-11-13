using ApiFirst.Tests.Fixtures.DomainServices;
using DomainModels.Models;
using DomainServices.Services;
using EntityFrameworkCore.QueryBuilder.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using FluentAssertions;
using Infrastructure.Data.Context;
using Moq;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace ApiFirst.Tests.Services.DomainServices
{
    public class CustomerBankInfoServiceTest
    {
        readonly CustomerBankInfoService _customerBankInfoService;
        readonly IUnitOfWork<WarrenContext> _unitOfWork;
        readonly IRepositoryFactory<WarrenContext> _repositoryFactory;
        readonly Mock<IRepositoryFactory<WarrenContext>> _repositoryFactoryMock;
        readonly Mock<IUnitOfWork<WarrenContext>> _unitOfWorkMock;

        public CustomerBankInfoServiceTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork<WarrenContext>>();
            _unitOfWork = _unitOfWorkMock.Object;
            _repositoryFactoryMock = new Mock<IRepositoryFactory<WarrenContext>>();
            _repositoryFactory = _repositoryFactoryMock.Object;

            _customerBankInfoService = new CustomerBankInfoService(_unitOfWork, _repositoryFactory);
        }

        [Fact]
        public async void Should_DeleteAsync_Sucessfully()
        {
            long customerId = 1;

            _unitOfWorkMock.Setup(p => p.Repository<CustomerBankInfo>().RemoveAsync(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>(), default));

            await _customerBankInfoService.DeleteAsync(customerId);

            _unitOfWorkMock.Verify(p => p.Repository<CustomerBankInfo>().RemoveAsync(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>(), default), Times.Once);
        }

        [Fact]
        public async void Should_GetByCustomerIdAsync_Sucessfully()
        {
            long customerId = 1;
            var bankInfo = CustomerBankInfoFixture.GenerateCustomerBankInfoFixture();

            _repositoryFactoryMock.Setup(p => p.Repository<CustomerBankInfo>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>())).Returns(It.IsAny<IQuery<CustomerBankInfo>>());

            _repositoryFactoryMock.Setup(p => p.Repository<CustomerBankInfo>().FirstOrDefaultAsync(It.IsAny<IQuery<CustomerBankInfo>>(), default)).ReturnsAsync(bankInfo);

            var result = await _customerBankInfoService.GetByCustomerIdAsync(customerId);

            result.Should().Be(bankInfo);

            _repositoryFactoryMock.Verify(p => p.Repository<CustomerBankInfo>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>()), Times.Once);

            _repositoryFactoryMock.Verify(p => p.Repository<CustomerBankInfo>().FirstOrDefaultAsync(It.IsAny<IQuery<CustomerBankInfo>>(), default), Times.Once);
        }

        [Fact]
        public async void Should_Not_GetByCustomerIdAsync_When_CustomerId_Dismatch()
        {
            long customerId = 1;
            var bankInfo = CustomerBankInfoFixture.GenerateCustomerBankInfoFixture();

            _repositoryFactoryMock.Setup(p => p.Repository<CustomerBankInfo>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>())).Returns(It.IsAny<IQuery<CustomerBankInfo>>());

            _repositoryFactoryMock.Setup(p => p.Repository<CustomerBankInfo>().FirstOrDefaultAsync(It.IsAny<IQuery<CustomerBankInfo>>(), default));

            var action = () => _customerBankInfoService.GetByCustomerIdAsync(customerId);

            await action.Should().ThrowAsync<ArgumentNullException>($"Cliente não encontrado para o Id {customerId}");

            _repositoryFactoryMock.Verify(p => p.Repository<CustomerBankInfo>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>()), Times.Once);

            _repositoryFactoryMock.Verify(p => p.Repository<CustomerBankInfo>().FirstOrDefaultAsync(It.IsAny<IQuery<CustomerBankInfo>>(), default), Times.Once);
        }

        [Fact]
        public async void Should_Create_SucessFully()
        {
            long customerId = 1;

            _unitOfWorkMock.Setup(p => p.Repository<CustomerBankInfo>().AddAsync(It.IsAny<CustomerBankInfo>(), default));

            _unitOfWorkMock.Setup(p => p.SaveChanges(true, false));

            await _customerBankInfoService.CreateAsync(customerId);

            _unitOfWorkMock.Verify(p => p.Repository<CustomerBankInfo>().AddAsync(It.IsAny<CustomerBankInfo>(), default), Times.Once);

            _unitOfWorkMock.Verify(p => p.SaveChanges(true, false), Times.Once);
        }

        [Fact]
        public async Task Should_GetBalance_Sucessfully()
        {
            decimal bankInfoBalance = 1000;
            var customerId = 1;

            _repositoryFactoryMock.Setup(p => p.Repository<CustomerBankInfo>().Any(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>())).Returns(true);

            _repositoryFactoryMock.Setup(p => p.Repository<CustomerBankInfo>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>()).Select(It.IsAny<Expression<Func<CustomerBankInfo, decimal>>>())).Returns(It.IsAny<IQuery<CustomerBankInfo, decimal>>());

            _repositoryFactoryMock.Setup(p => p.Repository<CustomerBankInfo>().FirstOrDefaultAsync(It.IsAny<IQuery<CustomerBankInfo, decimal>>(), default)).ReturnsAsync(bankInfoBalance);

            var result = await _customerBankInfoService.GetBalanceAsync(customerId);

            result.Should().Be(bankInfoBalance);

            _repositoryFactoryMock.Verify(p => p.Repository<CustomerBankInfo>().Any(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>()), Times.Once);

            _repositoryFactoryMock.Verify(p => p.Repository<CustomerBankInfo>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>()), Times.Once);

            _repositoryFactoryMock.Verify(p => p.Repository<CustomerBankInfo>().FirstOrDefaultAsync(It.IsAny<IQuery<CustomerBankInfo, decimal>>(), default), Times.Once);
        }

        [Fact]
        public async Task Should_Not_GetBalance_When_CustomerId_Dismatch()
        {
            decimal bankInfoBalance = 1000;
            var customerId = 0;

            _repositoryFactoryMock.Setup(p => p.Repository<CustomerBankInfo>().Any(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>())).Returns(false);

            _repositoryFactoryMock.Setup(p => p.Repository<CustomerBankInfo>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>()).Select(It.IsAny<Expression<Func<CustomerBankInfo, decimal>>>())).Returns(It.IsAny<IQuery<CustomerBankInfo, decimal>>());

            _repositoryFactoryMock.Setup(p => p.Repository<CustomerBankInfo>().FirstOrDefaultAsync(It.IsAny<IQuery<CustomerBankInfo, decimal>>(), default)).ReturnsAsync(bankInfoBalance);

            var action = () => _customerBankInfoService.GetBalanceAsync(customerId);

            await action.Should().ThrowAsync<ArgumentNullException>($"Cliente não encontrado para o Id {customerId}");

            _repositoryFactoryMock.Verify(p => p.Repository<CustomerBankInfo>().Any(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>()), Times.Once);

            _repositoryFactoryMock.Verify(p => p.Repository<CustomerBankInfo>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>()), Times.Never);

            _repositoryFactoryMock.Verify(p => p.Repository<CustomerBankInfo>().FirstOrDefaultAsync(It.IsAny<IQuery<CustomerBankInfo, decimal>>(), default), Times.Never);
        }



        [Fact]
        public async void Should_Deposit_Sucessfully()
        {
            var bankInfo = CustomerBankInfoFixture.GenerateCustomerBankInfoFixture();
            decimal amount = 10;
            long id = 1;

            _repositoryFactoryMock.Setup(p => p.Repository<CustomerBankInfo>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>())).Returns(It.IsAny<IQuery<CustomerBankInfo>>());

            _repositoryFactoryMock.Setup(p => p.Repository<CustomerBankInfo>().FirstOrDefaultAsync(It.IsAny<IQuery<CustomerBankInfo>>(), default)).ReturnsAsync(bankInfo);

            _unitOfWorkMock.Setup(p => p.Repository<CustomerBankInfo>().Update(It.IsAny<CustomerBankInfo>(), It.IsAny<Expression<Func<CustomerBankInfo, object>>>()));

            _unitOfWorkMock.Setup(p => p.SaveChanges(true, false));

            await _customerBankInfoService.DepositAsync(id, amount);

            _repositoryFactoryMock.Verify(p => p.Repository<CustomerBankInfo>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>()), Times.Once);

            _repositoryFactoryMock.Verify(p => p.Repository<CustomerBankInfo>().FirstOrDefaultAsync(It.IsAny<IQuery<CustomerBankInfo>>(), default), Times.Once);

            _unitOfWorkMock.Verify(p => p.Repository<CustomerBankInfo>().Update(It.IsAny<CustomerBankInfo>(), It.IsAny<Expression<Func<CustomerBankInfo, object>>>()), Times.Once);

            _unitOfWorkMock.Verify(p => p.SaveChanges(true, false), Times.Once);
        }

        [Fact]
        public async void Should_Withdraw_Sucessfully()
        {
            var bankInfo = CustomerBankInfoFixture.GenerateCustomerBankInfoFixture();
            decimal amount = 1;
            long id = 1;

            _repositoryFactoryMock.Setup(p => p.Repository<CustomerBankInfo>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>())).Returns(It.IsAny<IQuery<CustomerBankInfo>>());

            _repositoryFactoryMock.Setup(p => p.Repository<CustomerBankInfo>().FirstOrDefaultAsync(It.IsAny<IQuery<CustomerBankInfo>>(), default)).ReturnsAsync(bankInfo);

            _unitOfWorkMock.Setup(p => p.Repository<CustomerBankInfo>().Update(It.IsAny<CustomerBankInfo>(), It.IsAny<Expression<Func<CustomerBankInfo, object>>>()));

            _unitOfWorkMock.Setup(p => p.SaveChanges(true, false));

            await _customerBankInfoService.WithdrawAsync(id, amount);

            _repositoryFactoryMock.Verify(p => p.Repository<CustomerBankInfo>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>()), Times.Once);

            _repositoryFactoryMock.Verify(p => p.Repository<CustomerBankInfo>().FirstOrDefaultAsync(It.IsAny<IQuery<CustomerBankInfo>>(), default), Times.Once);

            _unitOfWorkMock.Verify(p => p.Repository<CustomerBankInfo>().Update(It.IsAny<CustomerBankInfo>(), It.IsAny<Expression<Func<CustomerBankInfo, object>>>()), Times.Once);

            _unitOfWorkMock.Verify(p => p.SaveChanges(true, false), Times.Once);
        }

        [Fact]
        public async Task Should_Not_Withdraw_When_Balance_Is_Lower_Than_AmountAsync()
        {
            var bankInfo = CustomerBankInfoFixture.GenerateCustomerBankInfoFixture();
            bankInfo.AccountBalance = 0;
            decimal amount = 1;
            long id = 1;

            _repositoryFactoryMock.Setup(p => p.Repository<CustomerBankInfo>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>())).Returns(It.IsAny<IQuery<CustomerBankInfo>>());

            _repositoryFactoryMock.Setup(p => p.Repository<CustomerBankInfo>().FirstOrDefaultAsync(It.IsAny<IQuery<CustomerBankInfo>>(), default)).ReturnsAsync(bankInfo);

            _unitOfWorkMock.Setup(p => p.Repository<CustomerBankInfo>().Update(It.IsAny<CustomerBankInfo>(), It.IsAny<Expression<Func<CustomerBankInfo, object>>>()));

            _unitOfWorkMock.Setup(p => p.SaveChanges(true, false));

            var action = () => _customerBankInfoService.WithdrawAsync(id, amount);

            await action.Should().ThrowAsync<ArgumentException>("Não é possível sacar o valor informado pois não há saldo suficiente");

            _repositoryFactoryMock.Verify(p => p.Repository<CustomerBankInfo>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>()), Times.Once);

            _repositoryFactoryMock.Verify(p => p.Repository<CustomerBankInfo>().FirstOrDefaultAsync(It.IsAny<IQuery<CustomerBankInfo>>(), default), Times.Once);

            _unitOfWorkMock.Verify(p => p.Repository<CustomerBankInfo>().Update(It.IsAny<CustomerBankInfo>(), It.IsAny<Expression<Func<CustomerBankInfo, object>>>()), Times.Never);

            _unitOfWorkMock.Verify(p => p.SaveChanges(true, false), Times.Never);
        }
    }
}
