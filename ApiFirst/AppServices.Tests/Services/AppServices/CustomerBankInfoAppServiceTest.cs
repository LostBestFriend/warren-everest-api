using AppServices.Services;
using DomainServices.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace AppServices.Tests.Services.AppServices
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
        public async void Should_Create_Sucessfully()
        {
            long customerId = 1;

            _customerBankInfoServiceMock.Setup(p => p.CreateAsync(It.IsAny<long>()));

            await _customerBankInfoAppService.CreateAsync(customerId);

            _customerBankInfoServiceMock.Verify(p => p.CreateAsync(It.IsAny<long>()), Times.Once);
        }

        [Fact]
        public async void Should_Deposit_Sucessfully()
        {
            long customerId = 1;
            decimal amount = 100;

            _customerBankInfoServiceMock.Setup(p => p.DepositAsync(It.IsAny<long>(), It.IsAny<decimal>()));

            await _customerBankInfoAppService.DepositAsync(customerId, amount);

            _customerBankInfoServiceMock.Verify(p => p.DepositAsync(It.IsAny<long>(), It.IsAny<decimal>()), Times.Once);
        }

        [Fact]
        public async void Should_GetBalance_Sucessfully()
        {
            long customerId = 1;
            decimal balance = 100;

            _customerBankInfoServiceMock.Setup(p => p.GetBalanceAsync(It.IsAny<long>())).ReturnsAsync(balance);

            var result = await _customerBankInfoAppService.GetBalanceAsync(customerId);

            result.Should().Be(balance);

            _customerBankInfoServiceMock.Verify(p => p.GetBalanceAsync(It.IsAny<long>()), Times.Once);
        }

        [Fact]
        public async void Should_Withdraw_Sucessfully()
        {
            long customerId = 1;
            decimal amount = 100;

            _customerBankInfoServiceMock.Setup(p => p.WithdrawAsync(It.IsAny<long>(), It.IsAny<decimal>()));

            await _customerBankInfoAppService.WithdrawAsync(customerId, amount);

            _customerBankInfoServiceMock.Verify(p => p.WithdrawAsync(It.IsAny<long>(), It.IsAny<decimal>()), Times.Once);
        }

        [Fact]
        public async void Should_DeleteAsync_Sucessfully()
        {
            long customerId = 1;

            _customerBankInfoServiceMock.Setup(p => p.DeleteAsync(It.IsAny<long>()));

            await _customerBankInfoAppService.DeleteAsync(customerId);

            _customerBankInfoServiceMock.Verify(p => p.DeleteAsync(It.IsAny<long>()), Times.Once);
        }
    }
}
