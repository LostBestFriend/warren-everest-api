using AppModels.AppModels.Customer;
using AppServices.Services;
using AppServices.Tests.Fixtures.Customer;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Services;
using DomainServices.Tests.Fixtures;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using FluentAssertions;
using Infrastructure.Data.Context;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace AppServices.Tests.Services
{
    public class CustomerAppServiceTest
    {
        private readonly CustomerAppService _customerAppService;
        private readonly Mock<CustomerService> _customerServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<CustomerBankInfoService> _customerBankInfoServiceMock;

        public CustomerAppServiceTest()
        {
            Mock<IUnitOfWork<WarrenContext>> _unitOfWorkMock = new();
            IUnitOfWork<WarrenContext> _unitOfWork = _unitOfWorkMock.Object;
            Mock<IRepositoryFactory<WarrenContext>> _repositoryFactoryMock = new();
            IRepositoryFactory<WarrenContext> _repositoryFactory = _repositoryFactoryMock.Object;

            _mapperMock = new();
            _customerBankInfoServiceMock = new(_unitOfWork, _repositoryFactory);
            _customerServiceMock = new(_unitOfWork, _repositoryFactory);
            _customerAppService = new CustomerAppService(_customerBankInfoServiceMock.Object, _customerServiceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async void Should_Create_SucessFully()
        {
            var customer = CreateCustomerFixture.GenerateCustomerFixture();
            Customer custom = CustomerFixture.GenerateCustomerFixture();
            long id = 1;

            _mapperMock.Setup(p => p.Map<Customer>(It.IsAny<CreateCustomer>())).Returns(custom);
            _customerServiceMock.Setup(p => p.CreateAsync(It.IsAny<Customer>())).ReturnsAsync(id);
            _customerBankInfoServiceMock.Setup(p => p.Create(id));

            long idResult = await _customerAppService.CreateAsync(customer);

            _mapperMock.Verify(p => p.Map<Customer>(It.IsAny<CreateCustomer>()), Times.Once);
            _customerServiceMock.Verify(p => p.CreateAsync(It.IsAny<Customer>()), Times.Once);
            _customerBankInfoServiceMock.Verify(p => p.Create(id), Times.Once);

            idResult.Should().BeGreaterThanOrEqualTo(1);
        }

        [Fact]
        public void Should_Not_Create_When_Cpf_Already_Exists()
        {

        }

        [Fact]
        public void Should_Not_Create_When_Email_Already_Exists()
        {

        }

        [Fact]
        public void Should_Delete_SucessFully()
        {
            long id = 1;
            _customerServiceMock.Setup(p => p.Delete(It.IsAny<long>()));

            _customerAppService.Delete(id);
            _customerServiceMock.Verify(p => p.Delete(It.IsAny<long>()), Times.Once);
        }

        [Fact]
        public void Should_Not_Delete_When_Id_Doesnt_Exist()
        {

        }

        [Fact]
        public void Should_GetAll_SucessFully()
        {
            var customer = CustomerResponseFixture.GenerateCustomerFixture(3);
            var customers1 = new List<Customer>();

            _customerServiceMock.Setup(p => p.GetAll()).Returns(customers1);
            _mapperMock.Setup(p => p.Map<IEnumerable<CustomerResponse>>(customers1)).Returns(customer);

            var customersResponse = _customerAppService.GetAll();

            _customerServiceMock.Verify(p => p.GetAll(), Times.Once);
            _mapperMock.Verify(p => p.Map<IEnumerable<CustomerResponse>>(customers1), Times.Once);

            customersResponse.Should().HaveCountGreaterThanOrEqualTo(0);
        }

        [Fact]
        public async void Should_GetByCpf_SucessFully()
        {
            var cpf = "42713070848";
            var customer = CustomerFixture.GenerateCustomerFixture();
            var customerResponse = CustomerResponseFixture.GenerateCustomerFixture();

            _customerServiceMock.Setup(p => p.GetByCpfAsync(It.IsAny<int>().ToString())).ReturnsAsync(customer);
            _mapperMock.Setup(p => p.Map<CustomerResponse>(It.IsAny<Customer>())).Returns(customerResponse);

            var result = await _customerAppService.GetByCpfAsync(cpf);

            _customerServiceMock.Verify(p => p.GetByCpfAsync(It.IsAny<int>().ToString()), Times.Once);
            _mapperMock.Verify(p => p.Map<CustomerResponse>(It.IsAny<Customer>()), Times.Once);

            result.Should().NotBeNull();
        }

        [Fact]
        public void Should_Not_GetByCpf_When_Cpf_Dismatch()
        {

        }

        [Fact]
        public void Should_Update_Sucessfully()
        {
            var customer = CustomerFixture.GenerateCustomerFixture();
            var UpdateCustomer = UpdateCustomerFixture.GenerateCustomerFixture();

            _mapperMock.Setup(p => p.Map<Customer>(It.IsAny<UpdateCustomer>())).Returns(customer);
            _customerServiceMock.Setup(p => p.Update(It.IsAny<Customer>()));

            _customerAppService.Update(UpdateCustomer);

            _mapperMock.Verify(p => p.Map<Customer>(It.IsAny<UpdateCustomer>()), Times.Once);
            _customerServiceMock.Verify(p => p.Update(It.IsAny<Customer>()), Times.Once);

            customer.Should().NotBeNull();
        }

        [Fact]
        public void Should_Not_Update_When_Id_Dismatch()
        {

        }

        [Fact]
        public void Should_Not_Update_When_Cpf_Already_Exists()
        {

        }

        [Fact]
        public void Should_Not_Update_When_Email_Already_Exists()
        {

        }

        [Fact]
        public async void Should_GetById_SucessFully()
        {
            var id = 1;
            var customer = CustomerFixture.GenerateCustomerFixture();
            var customerResponse = CustomerResponseFixture.GenerateCustomerFixture();

            _customerServiceMock.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(customer);
            _mapperMock.Setup(p => p.Map<CustomerResponse>(It.IsAny<Customer>())).Returns(customerResponse);

            var result = await _customerAppService.GetByIdAsync(id);

            _customerServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<int>()), Times.Once);
            _mapperMock.Verify(p => p.Map<CustomerResponse>(It.IsAny<Customer>()), Times.Once);

            result.Should().NotBeNull();
        }

        [Fact]
        public void Should_Not_GetById_When_Id_Dismatch()
        {

        }
    }
}
