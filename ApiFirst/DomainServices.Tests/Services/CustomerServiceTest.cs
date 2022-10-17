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
        public void Should_Delete_SucessFully()
        {
            long id = 1;
            var customer = CustomerFixture.GenerateCustomerFixture();

            _repositoryFactoryMock.Setup(p => p.Repository<Customer>()
                .SingleResultQuery().AndFilter(custom => custom.Id == id));
            _repositoryFactoryMock.Setup(p => p.Repository<Customer>()
                .FirstOrDefault(p.Repository<Customer>().SingleResultQuery()
                .AndFilter(custom => custom.Id == id))).Returns(customer);
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Remove(customer));
            _unitOfWorkMock.Setup(p => p.SaveChanges(true, false));

            _customerService.Delete(id);

            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Remove(customer), Times.Once);
            _unitOfWorkMock.Verify(p => p.SaveChanges(true, false), Times.Once);
        }

        [Fact]
        public void Should_Not_Delete_When_Id_Doesnt_Exist()
        {
            Customer customer = CustomerFixture.GenerateCustomerFixture();

            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Remove(It.IsAny<Customer>()));
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().SingleResultQuery().AndFilter(custom => custom.Id == It.IsAny<long>())).Returns(It.IsAny<IQuery<Customer>>());
            _unitOfWorkMock.Setup(p => p.SaveChanges(true, false));

            try
            {
                _customerService.Delete(customer.Id);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
            }

            _unitOfWorkMock.Verify(P => P.Repository<Customer>().Remove(It.IsAny<Customer>()), Times.Never);
            _unitOfWorkMock.Verify(p => p.SaveChanges(true, false), Times.Never);
        }

        [Fact]
        public void Should_GetAll_SucessFully()
        {
            var customer = CustomerFixture.GenerateCustomerFixture(2);

            _repositoryFactoryMock.Setup(p => p.Repository<Customer>().MultipleResultQuery()).Returns(It.IsAny<IMultipleResultQuery<Customer>>());
            _repositoryFactoryMock.Setup(p => p.Repository<Customer>().Search(It.IsAny<IMultipleResultQuery<Customer>>()));

            var customers = _customerService.GetAll();

            _repositoryFactoryMock.Verify(p => p.Repository<Customer>().MultipleResultQuery(), Times.Once);
            _repositoryFactoryMock.Verify(p => p.Repository<Customer>().Search(It.IsAny<IMultipleResultQuery<Customer>>()), Times.Once);
            customers.Should().HaveCountGreaterThanOrEqualTo(0);
        }

        [Fact]
        public void Should_GetByCpf_SucessFully()
        {
            var customer = CustomerFixture.GenerateCustomerFixture();
            string cpf = "42713070848";
            _repositoryFactoryMock.Setup(p => p.Repository<Customer>()
                .SingleResultQuery().AndFilter(custom => custom.Cpf == customer.Cpf));
            _repositoryFactoryMock.Setup(p => p.Repository<Customer>()
                .FirstOrDefault(It.IsAny<ISingleResultQuery<Customer>>()
                .AndFilter(custom => custom.Cpf == customer.Cpf)));

            var customers = _customerService.GetByCpfAsync(cpf);

            _repositoryFactoryMock.Verify(p => p.Repository<Customer>()
                .SingleResultQuery().AndFilter(custom => custom.Cpf == customer.Cpf), Times.Once);
            _repositoryFactoryMock.Verify(p => p.Repository<Customer>()
                .FirstOrDefault(p.Repository<Customer>().SingleResultQuery()
                .AndFilter(custom => custom.Cpf == customer.Cpf)), Times.Once);

            customers.Should().NotBeNull();
        }

        [Fact]
        public void Should_Not_GetByCpf_When_Cpf_Dismatch()
        {
            var customer = CustomerFixture.GenerateCustomerFixture();
            string cpf = "";
            _repositoryFactoryMock.Setup(p => p.Repository<Customer>()
                .SingleResultQuery().AndFilter(custom => custom.Cpf ==
                customer.Cpf));
            _repositoryFactoryMock.Setup(p => p.Repository<Customer>()
                .FirstOrDefault(p.Repository<Customer>().SingleResultQuery()
                .AndFilter(custom => custom.Cpf == customer.Cpf))).Throws(It.IsAny<ArgumentNullException>());

            try
            {
                var customers = _customerService.GetByCpfAsync(cpf);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
            }

            _repositoryFactoryMock.Verify(p => p.Repository<Customer>()
                .SingleResultQuery().AndFilter(custom => custom.Cpf == customer.Cpf), Times.Once);
            _repositoryFactoryMock.Verify(p => p.Repository<Customer>()
                .FirstOrDefault(p.Repository<Customer>().SingleResultQuery()
                .AndFilter(custom => custom.Cpf == customer.Cpf)), Times.Once);
        }

        [Fact]
        public void Should_Update_Sucessfully()
        {
            var customer = CustomerFixture.GenerateCustomerFixture();

            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Any(custom => custom.Id == customer.Id)).Returns(true);
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Any(custom => custom.Email == customer.Email && custom.Id != customer.Id)).Returns(false);
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Any(custom => custom.Cpf == customer.Cpf && custom.Id != customer.Id)).Returns(false);
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Update(customer));
            _unitOfWorkMock.Setup(p => p.SaveChanges(true, false));

            _customerService.Update(customer);

            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Any(custom => custom.Id == customer.Id), Times.Once);
            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Any(custom => custom.Email == customer.Email && custom.Id != customer.Id), Times.Once);
            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Any(custom => custom.Cpf == customer.Cpf && custom.Id != customer.Id), Times.Once);
            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Update(customer), Times.Once);
            _unitOfWorkMock.Verify(p => p.SaveChanges(true, false), Times.Once);
        }

        [Fact]
        public void Should_Not_Update_When_Id_Dismatch()
        {
            var customer = CustomerFixture.GenerateCustomerFixture();

            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Any(custom => custom.Id == customer.Id)).Returns(false);
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Any(custom => custom.Email == customer.Email && custom.Id != customer.Id)).Returns(false);
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Any(custom => custom.Cpf == customer.Cpf && custom.Id != customer.Id)).Returns(false);
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Update(customer));
            _unitOfWorkMock.Setup(p => p.SaveChanges(true, false));

            try
            {
                _customerService.Update(customer);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
            }

            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Any(custom => custom.Id == customer.Id), Times.Once);
            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Any(custom => custom.Email == customer.Email && custom.Id != customer.Id), Times.Never);
            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Any(custom => custom.Cpf == customer.Cpf && custom.Id != customer.Id), Times.Never);
            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Update(customer), Times.Never);
            _unitOfWorkMock.Verify(p => p.SaveChanges(true, false), Times.Never);
        }

        [Fact]
        public void Should_Not_Update_When_Cpf_Already_Exists()
        {
            var customer = CustomerFixture.GenerateCustomerFixture();

            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Any(custom => custom.Id == customer.Id)).Returns(true);
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Any(custom => custom.Email == customer.Email && custom.Id != customer.Id)).Returns(false);
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Any(custom => custom.Cpf == customer.Cpf && custom.Id != customer.Id)).Returns(true);
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Update(customer));
            _unitOfWorkMock.Setup(p => p.SaveChanges(true, false));

            try
            {
                _customerService.Update(customer);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }

            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Any(custom => custom.Id == customer.Id), Times.Once);
            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Any(custom => custom.Email == customer.Email && custom.Id != customer.Id), Times.Once);
            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Any(custom => custom.Cpf == customer.Cpf && custom.Id != customer.Id), Times.Once);
            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Update(customer), Times.Never);
            _unitOfWorkMock.Verify(p => p.SaveChanges(true, false), Times.Never);
        }

        [Fact]
        public void Should_Not_Update_When_Email_Already_Exists()
        {
            var customer = CustomerFixture.GenerateCustomerFixture();

            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Any(custom => custom.Id == customer.Id)).Returns(true);
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Any(custom => custom.Email == customer.Email && custom.Id != customer.Id)).Returns(true);
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Any(custom => custom.Cpf == customer.Cpf && custom.Id != customer.Id)).Returns(false);
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Update(customer));
            _unitOfWorkMock.Setup(p => p.SaveChanges(true, false));

            try
            {
                _customerService.Update(customer);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }

            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Any(custom => custom.Id == customer.Id), Times.Once);
            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Any(custom => custom.Email == customer.Email && custom.Id != customer.Id), Times.Once);
            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Any(custom => custom.Cpf == customer.Cpf && custom.Id != customer.Id), Times.Never);
            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Update(customer), Times.Never);
            _unitOfWorkMock.Verify(p => p.SaveChanges(true, false), Times.Never);
        }

        [Fact]
        public void Should_GetById_SucessFully()
        {
            Customer customer = CustomerFixture.GenerateCustomerFixture();
            long id = 1;
            //_repositoryFactoryMock.Setup(p => p.Repository<Customer>()
            //    .SingleResultQuery().AndFilter(custom => custom.Id == id))
            //    .Returns(It.IsAny<IQuery<Customer>>());

            _repositoryFactoryMock.Setup(p => p.Repository<Customer>()
                .FirstOrDefault(p.Repository<Customer>().SingleResultQuery()
                .AndFilter(custom => custom.Id == It.IsAny<Customer>().Id))).Returns(customer);

            var customers = _customerService.GetByIdAsync(id);

            //_repositoryFactoryMock.Verify(p => p.Repository<Customer>()
            //    .SingleResultQuery().AndFilter(custom => custom.Id == id),
            //    Times.Once);
            _repositoryFactoryMock.Verify(p => p.Repository<Customer>()
                .FirstOrDefault(p.Repository<Customer>().SingleResultQuery()
                .AndFilter(custom => custom.Id == id)), Times.Once);

            customers.Should().NotBeNull();
        }

        [Fact]
        public async void Should_Not_GetById_When_Id_Dismatch()
        {
            long id = 5;
            _repositoryFactoryMock.Setup(e => e.Repository<Customer>()
                .SingleResultQuery().AndFilter(custom => custom.Id == id));
            _repositoryFactoryMock.Setup(e => e.Repository<Customer>()
                .FirstOrDefault(e.Repository<Customer>().SingleResultQuery()
                .AndFilter(custom => custom.Id == id))).Throws(It.IsAny<ArgumentNullException>());

            try
            {
                await _customerService.GetByIdAsync(id);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }

            _repositoryFactoryMock.Verify(e => e.Repository<Customer>()
                .SingleResultQuery().AndFilter(custom => custom.Id == id), Times.Once);
            _repositoryFactoryMock.Verify(e => e.Repository<Customer>()
                .FirstOrDefault(e.Repository<Customer>().SingleResultQuery()
                .AndFilter(custom => custom.Id == id)), Times.Once);
        }
    }
}
