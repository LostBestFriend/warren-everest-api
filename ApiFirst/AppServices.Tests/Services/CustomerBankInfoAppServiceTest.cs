using AppServices.Services;
using DomainServices.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace AppServices.Tests.Services
{
    public class CustomerBankInfoAppServiceTest
    {
        readonly CustomerBankInfoAppService _customerBankInfoAppService;
        readonly Mock<ICustomerBankInfoService> _customerBankInfoServiceMock;

        public CustomerBankInfoAppServiceTest()
        {
            _customerBankInfoServiceMock = new();
            _customerBankInfoAppService = new CustomerBankInfoAppService(_customerBankInfoServiceMock.Object);
        }

        [Fact]
        public void Should_Create_Sucessfully()
        {
            long customerId = 1;

            _customerBankInfoServiceMock.Setup(p => p.Create(It.IsAny<long>()));

            _customerBankInfoAppService.Create(customerId);

            _customerBankInfoServiceMock.Verify(p => p.Create(It.IsAny<long>()), Times.Once);
        }

        [Fact]
        public void Should_Deposit_Sucessfully()
        {
            long customerId = 1;
            decimal amount = 100;

            _customerBankInfoServiceMock.Setup(p => p.DepositAsync(It.IsAny<long>(), It.IsAny<decimal>()));

            _customerBankInfoAppService.Deposit(customerId, amount);

            _customerBankInfoServiceMock.Verify(p => p.DepositAsync(It.IsAny<long>(), It.IsAny<decimal>()), Times.Once);
        }

        [Fact]
        public void Should_GetBalance_Sucessfully()
        {
            long customerId = 1;
            decimal balance = 100;

            _customerBankInfoServiceMock.Setup(p => p.GetBalance(It.IsAny<long>())).Returns(balance);

            var result = _customerBankInfoAppService.GetBalance(customerId);
            result.Should().BeGreaterThanOrEqualTo(0);

            _customerBankInfoServiceMock.Verify(p => p.GetBalance(It.IsAny<long>()), Times.Once);
        }

        [Fact]
        public void Should_Withdraw_Sucessfully()
        {
            long customerId = 1;
            decimal amount = 100;

            _customerBankInfoServiceMock.Setup(p => p.WithdrawAsync(It.IsAny<long>(), It.IsAny<decimal>()));

            _customerBankInfoAppService.Withdraw(customerId, amount);

            _customerBankInfoServiceMock.Verify(p => p.WithdrawAsync(It.IsAny<long>(), It.IsAny<decimal>()), Times.Once);
        }
    }
}
