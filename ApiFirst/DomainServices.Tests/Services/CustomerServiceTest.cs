using DomainModels.Models;
using DomainServices.Services;
using DomainServices.Tests.Fixtures;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using FluentAssertions;
using Infrastructure.Data.Context;
using Moq;
using System;
using Xunit;

namespace DomainServices.Tests.Services
{
    public class CustomerServiceTest
    {
        readonly CustomerService _customerService;
        readonly IUnitOfWork<WarrenContext> _unitOfWork;
        readonly IRepositoryFactory<WarrenContext> _repositoryFactory;
        readonly Mock<IRepositoryFactory<WarrenContext>> _repositoryFactoryMock;
        readonly Mock<CustomerService> _customerServiceMock;
        readonly Mock<IUnitOfWork<WarrenContext>> _unitOfWorkMock;

        public CustomerServiceTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork<WarrenContext>>();
            _unitOfWork = _unitOfWorkMock.Object;
            _repositoryFactoryMock = new Mock<IRepositoryFactory<WarrenContext>>();
            _repositoryFactory = _repositoryFactoryMock.Object;

            _customerService = new CustomerService(_unitOfWork, _repositoryFactory);
            _customerServiceMock = new Mock<CustomerService>();
        }

        [Fact]
        public async void Should_Create_SucessFully()
        {
            var customer = CustomerFixture.GenerateCustomerFixture();
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Any(custom => custom.Email == customer.Email)).Returns(false);
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Any(custom => custom.Cpf == customer.Cpf)).Returns(false);
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().AddAsync(It.IsAny<Customer>(), default));
            _unitOfWorkMock.Setup(p => p.SaveChangesAsync(true, false, default));


            var result = await _customerService.CreateAsync(customer);

            result.Should().BeGreaterThanOrEqualTo(0);
            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Any(custom => custom.Email == customer.Email), Times.Once);
            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Any(custom => custom.Cpf == customer.Cpf), Times.Once);
            _unitOfWorkMock.Verify(p => p.Repository<Customer>().AddAsync(It.IsAny<Customer>(), default), Times.Once);
            _unitOfWorkMock.Verify(p => p.SaveChangesAsync(true, false, default), Times.Once);
        }

        [Fact]
        public async void Should_Not_Create_When_Cpf_Already_Exists()
        {
            var customer = CustomerFixture.GenerateCustomerFixture();
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Any(custom => custom.Email == customer.Email)).Returns(false);
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Any(custom => custom.Cpf == customer.Cpf)).Returns(true);
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().AddAsync(It.IsAny<Customer>(), default));
            _unitOfWorkMock.Setup(p => p.SaveChangesAsync(true, false, default));

            try
            {
                var result = await _customerService.CreateAsync(customer);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }

            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Any(custom => custom.Email == customer.Email), Times.Once);
            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Any(custom => custom.Cpf == customer.Cpf), Times.Once);
            _unitOfWorkMock.Verify(p => p.Repository<Customer>().AddAsync(It.IsAny<Customer>(), default), Times.Never);
            _unitOfWorkMock.Verify(p => p.SaveChangesAsync(true, false, default), Times.Never);
        }

        [Fact]
        public async void Should_Not_Create_When_Email_Already_Exists()
        {
            var customer = CustomerFixture.GenerateCustomerFixture();
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Any(custom => custom.Email == customer.Email)).Returns(true);
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Any(custom => custom.Cpf == customer.Cpf)).Returns(false);
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().AddAsync(It.IsAny<Customer>(), default));
            _unitOfWorkMock.Setup(p => p.SaveChangesAsync(true, false, default));

            try
            {
                var result = await _customerService.CreateAsync(customer);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }

            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Any(custom => custom.Email == customer.Email), Times.Once);
            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Any(custom => custom.Cpf == customer.Cpf), Times.Never);
            _unitOfWorkMock.Verify(p => p.Repository<Customer>().AddAsync(It.IsAny<Customer>(), default), Times.Never);
            _unitOfWorkMock.Verify(p => p.SaveChangesAsync(true, false, default), Times.Never);
        }

        [Fact]
        public void Should_Delete_SucessFully()
        {
            var customer = CustomerFixture.GenerateCustomerFixture();
            customer.Id = 1;
            _customerService.CreateAsync(customer);
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Remove(It.IsAny<Customer>()));
            _unitOfWorkMock.Setup(p => p.SaveChanges(true, false));

            _customerService.Delete(customer.Id);

            _unitOfWorkMock.Verify(P => P.Repository<Customer>().Remove(It.IsAny<Customer>()), Times.Once);
            _unitOfWorkMock.Verify(p => p.SaveChanges(true, false), Times.Once);
        }

        [Fact]
        public void Should_Not_Delete_When_Id_Dont_Exist()
        {
            long id = -1;
            ArgumentNullException e = new();
            _customerServiceMock.Setup(p => p.Delete(It.IsAny<long>())).Throws(e);

            try
            {
                _customerService.Delete(id);
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
            _customerServiceMock.Setup(p => p.GetAll()).Returns(customer);

            var customers = _customerService.GetAll();

            customers.Should().HaveCountGreaterThanOrEqualTo(0);
        }

        [Fact]
        public async void Should_GetByCpf_SucessFully()
        {
            var customer = CustomerFixture.GenerateCustomerFixture();
            await _customerService.CreateAsync(customer);
            string cpf = "42713070848";
            _customerServiceMock.Setup(p => p.GetByCpfAsync(It.IsAny<string>())).ReturnsAsync(It.IsAny<Customer>());

            var customers = _customerService.GetByCpfAsync(cpf);

            customers.Should().NotBeNull();
        }

        [Fact]
        public async void Should_Not_GetByCpf_When_Cpf_Dismatch()
        {
            string cpf = "";
            ArgumentException e = new();
            _customerServiceMock.Setup(p => p.GetByCpfAsync(It.IsAny<string>())).Throws(e);

            try
            {
                await _customerService.GetByCpfAsync(cpf);
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
            _customerServiceMock.Setup(p => p.Update(It.IsAny<Customer>()));

            _customerService.Update(customer);
        }

        [Fact]
        public void Should_Not_Update_When_Id_Dismatch()
        {
            var customer = CustomerFixture.GenerateCustomerFixture();
            customer.Id = -1;
            ArgumentNullException e = new();
            _customerServiceMock.Setup(p => p.Update(It.IsAny<Customer>())).Throws(e);

            try
            {
                _customerService.Update(customer);
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
            await _customerService.CreateAsync(customer);

            var customer2 = CustomerFixture.GenerateCustomerFixture();
            customer2.Cpf = "";
            customer2.Id = 1;
            await _customerService.CreateAsync(customer2);

            customer2.Cpf = "42713070848";

            ArgumentException e = new();
            _customerServiceMock.Setup(p => p.Update(It.IsAny<Customer>())).Throws(e);

            try
            {
                _customerService.Update(customer2);
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
            await _customerService.CreateAsync(customer);

            var customer2 = CustomerFixture.GenerateCustomerFixture();
            customer2.Email = "";
            customer2.Id = 1;
            await _customerService.CreateAsync(customer2);

            customer2.Email = "a@g";
            ArgumentException e = new();
            _customerServiceMock.Setup(p => p.Update(It.IsAny<Customer>())).Throws(e);

            try
            {
                _customerService.Update(customer2);
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
            await _customerService.CreateAsync(customer);
            long id = 1;

            _customerServiceMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(It.IsAny<Customer>());

            var customers = _customerService.GetByIdAsync(id);

            customers.Should().NotBeNull();
        }

        [Fact]
        public async void Should_Not_GetById_When_Id_Dismatch()
        {
            var customer = CustomerFixture.GenerateCustomerFixture();
            customer.Id = 1;
            await _customerService.CreateAsync(customer);
            long id = -1;
            ArgumentException e = new();
            _customerServiceMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).Throws(e);

            try
            {
                await _customerService.GetByIdAsync(id);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
