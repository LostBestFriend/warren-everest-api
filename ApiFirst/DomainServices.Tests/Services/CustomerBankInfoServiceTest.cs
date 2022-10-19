using DomainModels.Models;
using DomainServices.Services;
using EntityFrameworkCore.QueryBuilder.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using FluentAssertions;
using Infrastructure.Data.Context;
using Moq;
using System;
using System.Linq.Expressions;
using Xunit;

namespace DomainServices.Tests.Services
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
        public void Should_Create_SucessFully()
        {
            long customerId = 1;

            _unitOfWorkMock.Setup(p => p.Repository<CustomerBankInfo>().Add(It.IsAny<CustomerBankInfo>()));
            _unitOfWorkMock.Setup(p => p.SaveChanges(true, false));

            _customerBankInfoService.Create(customerId);

            _unitOfWorkMock.Verify(p => p.Repository<CustomerBankInfo>().Add(It.IsAny<CustomerBankInfo>()), Times.Once);
            _unitOfWorkMock.Verify(p => p.SaveChanges(true, false), Times.Once);
        }

        [Fact]
        public void Should_GetBalance_Sucessfully()
        {
            decimal bankInfoBalance = 1000;
            var customerId = 1;

            _repositoryFactoryMock.Setup(p => p.Repository<CustomerBankInfo>().Any(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>())).Returns(true);
            _repositoryFactoryMock.Setup(p => p.Repository<CustomerBankInfo>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>()).Select(It.IsAny<Expression<Func<CustomerBankInfo, decimal>>>())).Returns(It.IsAny<IQuery<CustomerBankInfo, decimal>>());
            _repositoryFactoryMock.Setup(p => p.Repository<CustomerBankInfo>().FirstOrDefault(It.IsAny<IQuery<CustomerBankInfo, decimal>>())).Returns(bankInfoBalance);

            var result = _customerBankInfoService.GetBalance(customerId);

            result.Should().BeGreaterThanOrEqualTo(0);

            _repositoryFactoryMock.Verify(p => p.Repository<CustomerBankInfo>().Any(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>()), Times.Once);
            _repositoryFactoryMock.Verify(p => p.Repository<CustomerBankInfo>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>()), Times.Once);
            _repositoryFactoryMock.Verify(p => p.Repository<CustomerBankInfo>().FirstOrDefault(It.IsAny<IQuery<CustomerBankInfo, decimal>>()), Times.Once);
        }

        [Fact]
        public void Should_Not_GetBalance_When_CustomerId_Dismatch()
        {
            decimal bankInfoBalance = 1000;
            var customerId = 0;

            _repositoryFactoryMock.Setup(p => p.Repository<CustomerBankInfo>().Any(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>())).Returns(false);
            _repositoryFactoryMock.Setup(p => p.Repository<CustomerBankInfo>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>()).Select(It.IsAny<Expression<Func<CustomerBankInfo, decimal>>>())).Returns(It.IsAny<IQuery<CustomerBankInfo, decimal>>());
            _repositoryFactoryMock.Setup(p => p.Repository<CustomerBankInfo>().FirstOrDefault(It.IsAny<IQuery<CustomerBankInfo, decimal>>())).Returns(bankInfoBalance);

            try
            {
                var result = _customerBankInfoService.GetBalance(customerId);
                result.Should().BeGreaterThanOrEqualTo(0);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
            }

            _repositoryFactoryMock.Verify(p => p.Repository<CustomerBankInfo>().Any(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>()), Times.Once);
            _repositoryFactoryMock.Verify(p => p.Repository<CustomerBankInfo>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>()), Times.Never);
            _repositoryFactoryMock.Verify(p => p.Repository<CustomerBankInfo>().FirstOrDefault(It.IsAny<IQuery<CustomerBankInfo, decimal>>()), Times.Never);
        }
    }
}
