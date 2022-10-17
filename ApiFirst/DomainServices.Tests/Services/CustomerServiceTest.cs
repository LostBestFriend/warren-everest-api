using DomainModels.Models;
using DomainServices.Services;
using DomainServices.Tests.Fixtures;
using EntityFrameworkCore.QueryBuilder.Interfaces;
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
        public async void Should_Delete_SucessFully()
        {
            var customer = CustomerFixture.GenerateCustomerFixture();
            customer.Id = 1;
            long id = await _customerService.CreateAsync(customer);
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Remove(It.IsAny<Customer>()));
            _unitOfWorkMock.Setup(p => p.SaveChanges(true, false));

            _customerService.Delete(id);

            _unitOfWorkMock.Verify(P => P.Repository<Customer>().Remove(It.IsAny<Customer>()), Times.Once);
            _unitOfWorkMock.Verify(p => p.SaveChanges(true, false), Times.Once);
        }

        [Fact]
        public async void Should_Not_Delete_When_Id_Doesnt_Exist()
        {
            var customer = CustomerFixture.GenerateCustomerFixture();
            customer.Id = 1;
            await _customerService.CreateAsync(customer);
            long id = -1;
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Remove(It.IsAny<Customer>()));
            _unitOfWorkMock.Setup(p => p.SaveChanges(true, false));

            try
            {
                _customerService.Delete(id);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
            }

            _unitOfWorkMock.Verify(P => P.Repository<Customer>().Remove(It.IsAny<Customer>()), Times.Once);
            _unitOfWorkMock.Verify(p => p.SaveChanges(true, false), Times.Once);
        }

        [Fact]
        public async void Should_GetAll_SucessFully()
        {
            var customer = CustomerFixture.GenerateCustomerFixture(2);

            await _customerService.CreateAsync(customer[0]);
            await _customerService.CreateAsync(customer[1]);
            _repositoryFactoryMock.Setup(p => p.Repository<Customer>().Search(It.IsAny<IMultipleResultQuery<Customer>>()));

            var customers = _customerService.GetAll();

            _repositoryFactoryMock.Verify(p => p.Repository<Customer>().Search(It.IsAny<IMultipleResultQuery<Customer>>()), Times.Once);
            customers.Should().HaveCountGreaterThanOrEqualTo(0);
        }

        [Fact]
        public async void Should_GetByCpf_SucessFully()
        {
            var customer = CustomerFixture.GenerateCustomerFixture();
            await _customerService.CreateAsync(customer);
            string cpf = "42713070848";
            _repositoryFactoryMock.Setup(p => p.Repository<Customer>()
                .SingleResultQuery().AndFilter(custom => custom.Cpf == It
                .IsAny<Customer>().Cpf));
            _repositoryFactoryMock.Setup(p => p.Repository<Customer>()
                .FirstOrDefault(p.Repository<Customer>().SingleResultQuery()
                .AndFilter(custom => custom.Cpf == It.IsAny<Customer>().Cpf)));

            var customers = _customerService.GetByCpfAsync(cpf);

            _repositoryFactoryMock.Verify(p => p.Repository<Customer>()
                .SingleResultQuery().AndFilter(custom => custom.Cpf == It
                .IsAny<Customer>().Cpf), Times.Once);
            _repositoryFactoryMock.Verify(p => p.Repository<Customer>()
                .FirstOrDefault(p.Repository<Customer>().SingleResultQuery()
                .AndFilter(custom => custom.Cpf == It.IsAny<Customer>().Cpf)), Times.Once);

            customers.Should().NotBeNull();
        }

        [Fact]
        public async void Should_Not_GetByCpf_When_Cpf_Dismatch()
        {
            string cpf = "";
            var customer = CustomerFixture.GenerateCustomerFixture();
            await _customerService.CreateAsync(customer);
            _repositoryFactoryMock.Setup(p => p.Repository<Customer>()
                .SingleResultQuery().AndFilter(custom => custom.Cpf == It
                .IsAny<Customer>().Cpf));
            _repositoryFactoryMock.Setup(p => p.Repository<Customer>()
                .FirstOrDefault(p.Repository<Customer>().SingleResultQuery()
                .AndFilter(custom => custom.Cpf == It.IsAny<Customer>().Cpf))).Throws(It.IsAny<ArgumentNullException>());

            try
            {
                var customers = _customerService.GetByCpfAsync(cpf);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
            }

            _repositoryFactoryMock.Verify(p => p.Repository<Customer>()
                .SingleResultQuery().AndFilter(custom => custom.Cpf == It
                .IsAny<Customer>().Cpf), Times.Once);
            _repositoryFactoryMock.Verify(p => p.Repository<Customer>()
                .FirstOrDefault(p.Repository<Customer>().SingleResultQuery()
                .AndFilter(custom => custom.Cpf == It.IsAny<Customer>().Cpf)), Times.Once);
        }

        [Fact]
        public void Should_Update_Sucessfully()
        {
            var customer = CustomerFixture.GenerateCustomerFixture();
            customer.Id = 1;

            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Any(custom => custom.Id == It.IsAny<Customer>().Id)).Returns(false);
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Any(custom => custom.Email == It.IsAny<Customer>().Email && custom.Id != It.IsAny<Customer>().Id)).Returns(false);
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Any(custom => custom.Cpf == It.IsAny<Customer>().Cpf && It.IsAny<Customer>().Id != custom.Id)).Returns(false);
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Update(It.IsAny<Customer>()));
            _unitOfWorkMock.Setup(p => p.SaveChanges(true, false));

            _customerService.Update(customer);

            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Any(custom => custom.Id == It.IsAny<Customer>().Id), Times.Once);
            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Any(custom => custom.Email == It.IsAny<Customer>().Email && custom.Id != It.IsAny<Customer>().Id), Times.Once);
            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Any(custom => custom.Cpf == It.IsAny<Customer>().Cpf && It.IsAny<Customer>().Id != custom.Id), Times.Once);
            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Update(It.IsAny<Customer>()), Times.Once);
            _unitOfWorkMock.Verify(p => p.SaveChanges(true, false), Times.Once);
        }

        [Fact]
        public void Should_Not_Update_When_Id_Dismatch()
        {
            var customer = CustomerFixture.GenerateCustomerFixture();
            customer.Id = 0;

            var customer2 = CustomerFixture.GenerateCustomerFixture();
            customer.Id = 1;

            _unitOfWorkMock.Setup(p => !p.Repository<Customer>().Any(custom => custom.Id == It.IsAny<Customer>().Id)).Returns(true);
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Any(custom => custom.Email == It.IsAny<Customer>().Email && custom.Id != It.IsAny<Customer>().Id)).Returns(false);
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Any(custom => custom.Cpf == It.IsAny<Customer>().Cpf && It.IsAny<Customer>().Id != custom.Id)).Returns(false);
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Update(It.IsAny<Customer>()));
            _unitOfWorkMock.Setup(p => p.SaveChanges(true, false));

            try
            {
                _customerService.Update(customer2);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
            }

            _unitOfWorkMock.Verify(p => !p.Repository<Customer>().Any(custom => custom.Id == It.IsAny<Customer>().Id), Times.Once);
            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Any(custom => custom.Email == It.IsAny<Customer>().Email && custom.Id != It.IsAny<Customer>().Id), Times.Never);
            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Any(custom => custom.Cpf == It.IsAny<Customer>().Cpf && It.IsAny<Customer>().Id != custom.Id), Times.Never);
            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Update(It.IsAny<Customer>()), Times.Never);
            _unitOfWorkMock.Verify(p => p.SaveChanges(true, false), Times.Never);
        }

        [Fact]
        public void Should_Not_Update_When_Cpf_Already_Exists()
        {
            var custom = CustomerFixture.GenerateCustomerFixture();

            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Any(customer => customer.Id == custom.Id)).Returns(true);
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Any(customer => customer.Email == It.IsAny<Customer>().Email && customer.Id != It.IsAny<Customer>().Id)).Returns(false);
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Any(customer => customer.Cpf == It.IsAny<Customer>().Cpf && It.IsAny<Customer>().Id != customer.Id)).Returns(true);
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Update(It.IsAny<Customer>()));
            _unitOfWorkMock.Setup(p => p.SaveChanges(true, false));

            try
            {
                _customerService.Update(custom);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
            }

            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Any(customer => customer.Id == custom.Id), Times.Once);
            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Any(customer => customer.Email == custom.Email && customer.Id != custom.Id), Times.Once);
            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Any(customer => customer.Cpf == It.IsAny<Customer>().Cpf && customer.Id != It.IsAny<Customer>().Id), Times.Once);
            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Update(custom), Times.Never);
            _unitOfWorkMock.Verify(p => p.SaveChanges(true, false), Times.Never);
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
            _unitOfWorkMock.Setup(p => !p.Repository<Customer>().Any(custom => custom.Id == It.IsAny<Customer>().Id)).Returns(false);
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Any(custom => custom.Email == It.IsAny<Customer>().Email && custom.Id != It.IsAny<Customer>().Id)).Returns(true);
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Any(custom => custom.Cpf == It.IsAny<Customer>().Cpf && It.IsAny<Customer>().Id != custom.Id)).Returns(false);
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Update(It.IsAny<Customer>()));
            _unitOfWorkMock.Setup(p => p.SaveChanges(true, false));

            try
            {
                _customerService.Update(customer2);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
            }

            _unitOfWorkMock.Verify(p => !p.Repository<Customer>().Any(custom => custom.Id == It.IsAny<Customer>().Id), Times.Once);
            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Any(custom => custom.Email == It.IsAny<Customer>().Email && custom.Id != It.IsAny<Customer>().Id), Times.Once);
            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Any(custom => custom.Cpf == It.IsAny<Customer>().Cpf && It.IsAny<Customer>().Id != custom.Id), Times.Never);
            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Update(It.IsAny<Customer>()), Times.Never);
            _unitOfWorkMock.Verify(p => p.SaveChanges(true, false), Times.Never);
        }

        [Fact]
        public async void Should_GetById_SucessFully()
        {
            var customer = CustomerFixture.GenerateCustomerFixture();
            customer.Id = 1;
            await _customerService.CreateAsync(customer);
            long id = 1;
            _repositoryFactoryMock.Setup(p => p.Repository<Customer>()
                .SingleResultQuery().AndFilter(custom => custom.Id == It
                .IsAny<Customer>().Id)).Returns(It.IsAny<IQuery<Customer>>());
            _repositoryFactoryMock.Setup(p => p.Repository<Customer>()
                .FirstOrDefault(p.Repository<Customer>().SingleResultQuery()
                .AndFilter(custom => custom.Id == It.IsAny<Customer>().Id))).Returns(It.IsAny<Customer>());

            var customers = _customerService.GetByIdAsync(id);

            _repositoryFactoryMock.Verify(p => p.Repository<Customer>()
                .SingleResultQuery().AndFilter(custom => custom.Id == It
                .IsAny<Customer>().Id), Times.Once);
            _repositoryFactoryMock.Verify(p => p.Repository<Customer>()
                .FirstOrDefault(p.Repository<Customer>().SingleResultQuery()
                .AndFilter(custom => custom.Id == It.IsAny<Customer>().Id)), Times.Once);

            customers.Should().NotBeNull();
        }

        [Fact]
        public async void Should_Not_GetById_When_Id_Dismatch()
        {
            var customer = CustomerFixture.GenerateCustomerFixture();
            customer.Id = 1;
            await _customerService.CreateAsync(customer);
            long id = -1;
            _repositoryFactoryMock.Setup(p => p.Repository<Customer>()
                .SingleResultQuery().AndFilter(custom => custom.Id == It
                .IsAny<Customer>().Id));
            _repositoryFactoryMock.Setup(p => p.Repository<Customer>()
                .FirstOrDefault(p.Repository<Customer>().SingleResultQuery()
                .AndFilter(custom => custom.Id == It.IsAny<Customer>().Id))).Throws(It.IsAny<ArgumentNullException>());

            try
            {
                await _customerService.GetByIdAsync(id);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }

            _repositoryFactoryMock.Verify(p => p.Repository<Customer>()
                .SingleResultQuery().AndFilter(custom => custom.Id == It
                .IsAny<Customer>().Id), Times.Once);
            _repositoryFactoryMock.Verify(p => p.Repository<Customer>()
                .FirstOrDefault(p.Repository<Customer>().SingleResultQuery()
                .AndFilter(custom => custom.Id == It.IsAny<Customer>().Id)), Times.Once);
        }
    }
}
