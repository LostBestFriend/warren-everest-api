using DomainModels.Models;
using DomainServices.Interfaces;
using DomainServices.Tests.Fixtures;
using FluentAssertions;
using Moq;
using System;
using Xunit;

namespace DomainServices.Tests.Services
{
    public class CustomerServiceTest
    {
        readonly Mock<ICustomerService> _customerService;

        public CustomerServiceTest()
        {
            _customerService = new Mock<ICustomerService>();
        }

        [Fact]
        public async void Should_Create_SucessFully()
        {
            var customer = CustomerFixture.GenerateCustomerFixture();
            _customerService.Setup(p => p.CreateAsync(It.IsAny<Customer>())).ReturnsAsync(It.IsAny<long>());

            var result = await _customerService.Object.CreateAsync(customer);

            result.Should().BeGreaterThanOrEqualTo(0);
        }

        [Fact]
        public async void Should_Not_Create_When_Cpf_Already_Exists()
        {
            var customer = CustomerFixture.GenerateCustomerFixture();
            customer.Cpf = "42713070848";
            customer.Id = 0;
            await _customerService.Object.CreateAsync(customer);

            var customer2 = CustomerFixture.GenerateCustomerFixture();
            customer2.Cpf = "";
            customer2.Id = 1;
            await _customerService.Object.CreateAsync(customer2);

            customer2.Cpf = "42713070848";

            ArgumentException e = new();
            _customerService.Setup(p => p.CreateAsync(It.IsAny<Customer>())).Throws(e);

            try
            {
                await _customerService.Object.CreateAsync(customer2);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [Fact]
        public async void Should_Not_Create_When_Email_Already_Exists()
        {
            var customer = CustomerFixture.GenerateCustomerFixture();
            customer.Email = "a@g";
            customer.Id = 0;
            await _customerService.Object.CreateAsync(customer);

            var customer2 = CustomerFixture.GenerateCustomerFixture();
            customer2.Email = "";
            customer2.Id = 1;
            await _customerService.Object.CreateAsync(customer2);

            customer2.Email = "a@g";
            ArgumentException e = new();
            _customerService.Setup(p => p.CreateAsync(It.IsAny<Customer>())).Throws(e);

            try
            {
                await _customerService.Object.CreateAsync(customer2);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [Fact]
        public void Should_Delete_SucessFully()
        {
            long id = 1;
            _customerService.Setup(p => p.Delete(It.IsAny<long>()));

            _customerService.Object.Delete(id);
        }

        [Fact]
        public void Should_Not_Delete_When_Id_Dont_Exist()
        {
            long id = -1;
            ArgumentNullException e = new();
            _customerService.Setup(p => p.Delete(It.IsAny<long>())).Throws(e);

            try
            {
                _customerService.Object.Delete(id);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [Fact]
        public void Should_GetAll_SucessFully()
        {
            var customer = CustomerFixture.GenerateCustomerFixture(It.IsAny<int>());
            _customerService.Setup(p => p.GetAll()).Returns(customer);

            var customers = _customerService.Object.GetAll();

            customers.Should().HaveCountGreaterThanOrEqualTo(0);
        }

        [Fact]
        public async void Should_GetByCpf_SucessFully()
        {
            var customer = CustomerFixture.GenerateCustomerFixture();
            await _customerService.Object.CreateAsync(customer);
            string cpf = "42713070848";
            _customerService.Setup(p => p.GetByCpfAsync(It.IsAny<string>())).ReturnsAsync(It.IsAny<Customer>());

            var customers = _customerService.Object.GetByCpfAsync(cpf);

            customers.Should().NotBeNull();
        }

        [Fact]
        public async void Should_Not_GetByCpf_When_Cpf_Dismatch()
        {
            string cpf = "";
            ArgumentException e = new();
            _customerService.Setup(p => p.GetByCpfAsync(It.IsAny<string>())).Throws(e);

            try
            {
                await _customerService.Object.GetByCpfAsync(cpf);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [Fact]
        public void Should_Update_Sucessfully()
        {
            var customer = CustomerFixture.GenerateCustomerFixture();
            _customerService.Setup(p => p.Update(It.IsAny<Customer>()));

            _customerService.Object.Update(customer);
        }

        [Fact]
        public void Should_Not_Update_When_Id_Dismatch()
        {
            var customer = CustomerFixture.GenerateCustomerFixture();
            customer.Id = -1;
            ArgumentNullException e = new();
            _customerService.Setup(p => p.Update(It.IsAny<Customer>())).Throws(e);

            try
            {
                _customerService.Object.Update(customer);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [Fact]
        public async void Should_Not_Update_When_Cpf_Already_Exists()
        {
            var customer = CustomerFixture.GenerateCustomerFixture();
            customer.Cpf = "42713070848";
            customer.Id = 0;
            await _customerService.Object.CreateAsync(customer);

            var customer2 = CustomerFixture.GenerateCustomerFixture();
            customer2.Cpf = "";
            customer2.Id = 1;
            await _customerService.Object.CreateAsync(customer2);

            customer2.Cpf = "42713070848";

            ArgumentException e = new();
            _customerService.Setup(p => p.Update(It.IsAny<Customer>())).Throws(e);

            try
            {
                _customerService.Object.Update(customer2);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [Fact]
        public async void Should_Not_Update_When_Email_Already_Exists()
        {
            var customer = CustomerFixture.GenerateCustomerFixture();
            customer.Email = "a@g";
            customer.Id = 0;
            await _customerService.Object.CreateAsync(customer);

            var customer2 = CustomerFixture.GenerateCustomerFixture();
            customer2.Email = "";
            customer2.Id = 1;
            await _customerService.Object.CreateAsync(customer2);

            customer2.Email = "a@g";
            ArgumentException e = new();
            _customerService.Setup(p => p.Update(It.IsAny<Customer>())).Throws(e);

            try
            {
                _customerService.Object.Update(customer2);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [Fact]
        public async void Should_GetById_SucessFully()
        {
            var customer = CustomerFixture.GenerateCustomerFixture();
            customer.Id = 1;
            await _customerService.Object.CreateAsync(customer);
            long id = 1;

            _customerService.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(It.IsAny<Customer>());

            var customers = _customerService.Object.GetByIdAsync(id);

            customers.Should().NotBeNull();
        }

        [Fact]
        public async void Should_Not_GetById_When_Id_Dismatch()
        {
            var customer = CustomerFixture.GenerateCustomerFixture();
            customer.Id = 1;
            await _customerService.Object.CreateAsync(customer);
            long id = -1;
            ArgumentException e = new();
            _customerService.Setup(p => p.GetByIdAsync(It.IsAny<long>())).Throws(e);

            try
            {
                await _customerService.Object.GetByIdAsync(id);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
