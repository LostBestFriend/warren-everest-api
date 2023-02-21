﻿using ApiFirst.Tests.Fixtures.AppServices.Customer;
using ApiFirst.Tests.Fixtures.DomainServices;
using AppServices.Interfaces;
using AppServices.Profiles;
using AppServices.Services;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace ApiFirst.Tests.Services.AppServices
{
    public class CustomerAppServiceTest
    {
        private readonly CustomerAppService _customerAppService;
        private readonly Mock<ICustomerService> _customerServiceMock;
        private readonly Mock<ICustomerBankInfoAppService> _customerBankInfoServiceMock;

        public CustomerAppServiceTest()
        {
            IMapper _mapper = new MapperConfiguration(cfg => cfg.AddProfile<CustomerProfile>()).CreateMapper();
            _customerServiceMock = new();
            _customerBankInfoServiceMock = new();
            _customerAppService = new CustomerAppService(_customerBankInfoServiceMock.Object, _customerServiceMock.Object, _mapper);
        }

        [Fact]
        public async void Should_Create_SucessFully()
        {
            var createCustomer = CreateCustomerFixture.GenerateCreateCustomerFixture();
            var custom = CustomerFixture.GenerateCustomerFixture();
            long id = 1;

            _customerServiceMock.Setup(p => p.CreateAsync(It.IsAny<Customer>())).ReturnsAsync(id);

            _customerBankInfoServiceMock.Setup(p => p.CreateAsync(id));

            long idResult = await _customerAppService.CreateAsync(createCustomer);

            idResult.Should().BeGreaterThanOrEqualTo(1);

            _customerServiceMock.Verify(p => p.CreateAsync(It.IsAny<Customer>()), Times.Once);

            _customerBankInfoServiceMock.Verify(p => p.CreateAsync(id), Times.Once);
        }

        [Fact]
        public async void Should_Delete_SucessFully()
        {
            long id = 1;

            _customerServiceMock.Setup(p => p.DeleteAsync(It.IsAny<long>()));

            await _customerAppService.DeleteAsync(id);

            _customerServiceMock.Verify(p => p.DeleteAsync(It.IsAny<long>()), Times.Once);
        }

        [Fact]
        public async void Should_GetAll_SucessFully()
        {
            var customers = CustomerFixture.GenerateCustomerFixture(3);

            _customerServiceMock.Setup(p => p.GetAllAsync()).ReturnsAsync(customers);

            var customersResponse = await _customerAppService.GetAllAsync();

            customersResponse.Should().HaveCount(3);

            _customerServiceMock.Verify(p => p.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async void Should_GetByCpf_SucessFully()
        {
            var cpf = "42713070848";
            var customer = CustomerFixture.GenerateCustomerFixture();

            _customerServiceMock.Setup(p => p.GetByCpfAsync(It.IsAny<string>())).ReturnsAsync(customer);

            var result = await _customerAppService.GetByCpfAsync(cpf);

            result.Should().NotBeNull();

            _customerServiceMock.Verify(p => p.GetByCpfAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void Should_Update_Sucessfully()
        {
            var customer = CustomerFixture.GenerateCustomerFixture();
            var UpdateCustomer = UpdateCustomerFixture.GenerateUpdateCustomerFixture();

            _customerServiceMock.Setup(p => p.Update(It.IsAny<Customer>()));

            _customerAppService.Update(UpdateCustomer);

            _customerServiceMock.Verify(p => p.Update(It.IsAny<Customer>()), Times.Once);
        }

        [Fact]
        public async void Should_GetById_SucessFully()
        {
            var id = 1;
            var customer = CustomerFixture.GenerateCustomerFixture();

            _customerServiceMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(customer);

            var result = await _customerAppService.GetByIdAsync(id);

            result.Should().NotBeNull();

            _customerServiceMock.Verify(p => p.GetByIdAsync(It.IsAny<long>()), Times.Once);
        }
    }
}
