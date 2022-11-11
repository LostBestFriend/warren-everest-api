using DomainModels.Models;
using DomainServices.Services;
using DomainServices.Tests.Fixtures;
using EntityFrameworkCore.QueryBuilder;
using EntityFrameworkCore.QueryBuilder.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using FluentAssertions;
using Infrastructure.Data.Context;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

namespace DomainServices.Tests.Services
{
    public class CustomerServiceTest
    {
        readonly CustomerService _customerService;
        readonly IUnitOfWork<WarrenContext> _unitOfWork;
        readonly IRepositoryFactory<WarrenContext> _repositoryFactory;
        readonly Mock<IRepositoryFactory<WarrenContext>> _repositoryFactoryMock;
        readonly Mock<IUnitOfWork<WarrenContext>> _unitOfWorkMock;

        public CustomerServiceTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork<WarrenContext>>();
            _unitOfWork = _unitOfWorkMock.Object;
            _repositoryFactoryMock = new Mock<IRepositoryFactory<WarrenContext>>();
            _repositoryFactory = _repositoryFactoryMock.Object;

            _customerService = new CustomerService(_unitOfWork, _repositoryFactory);
        }

        [Fact]
        public async void Should_Create_SucessFully()
        {
            var customer = CustomerFixture.GenerateCustomerFixture();

            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Any(It.IsAny<Expression<Func<Customer, bool>>>())).Returns(false);
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().AddAsync(It.IsAny<Customer>(), default));
            _unitOfWorkMock.Setup(p => p.SaveChangesAsync(true, false, default));

            var result = await _customerService.CreateAsync(customer);

            result.Should().Be(customer.Id);

            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Any(It.IsAny<Expression<Func<Customer, bool>>>()), Times.Exactly(2));
            _unitOfWorkMock.Verify(p => p.Repository<Customer>().AddAsync(It.IsAny<Customer>(), default), Times.Once);
            _unitOfWorkMock.Verify(p => p.SaveChangesAsync(true, false, default), Times.Once);
        }

        [Fact]
        public async void Should_Not_Create_When_Cpf_Already_Exists()
        {
            var customer = CustomerFixture.GenerateCustomerFixture();

            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Any(custom => custom.Cpf == customer.Cpf)).Returns(true);
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Any(custom => custom.Email == customer.Email)).Returns(false);
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().AddAsync(It.IsAny<Customer>(), default));
            _unitOfWorkMock.Setup(p => p.SaveChangesAsync(true, false, default));

            var action = () => _customerService.CreateAsync(customer);

            await action.Should().ThrowAsync<ArgumentException>("O CPF já está sendo usado.");

            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Any(custom => custom.Cpf == customer.Cpf), Times.Once);
            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Any(custom => custom.Email == customer.Email), Times.Once);
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

            var action = () => _customerService.CreateAsync(customer);

            await action.Should().ThrowAsync<ArgumentException>("Email já está sendo usado.");

            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Any(custom => custom.Email == customer.Email), Times.Once);
            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Any(custom => custom.Cpf == customer.Cpf), Times.Never);
            _unitOfWorkMock.Verify(p => p.Repository<Customer>().AddAsync(It.IsAny<Customer>(), default), Times.Never);
            _unitOfWorkMock.Verify(p => p.SaveChangesAsync(true, false, default), Times.Never);
        }



        [Fact]
        public async void Should_Delete_SucessFully()
        {
            long id = 1;
            var customer = CustomerFixture.GenerateCustomerFixture();

            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Remove(It.IsAny<Customer>()));
            _unitOfWorkMock.Setup(p => p.SaveChanges(true, false));
            _repositoryFactoryMock.Setup(p => p.Repository<Customer>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Customer, bool>>>())).Returns(It.IsAny<IQuery<Customer>>());
            _repositoryFactoryMock.Setup(p => p.Repository<Customer>().FirstOrDefaultAsync(It.IsAny<IQuery<Customer>>(), default)).ReturnsAsync(customer);

            await _customerService.DeleteAsync(id);

            _repositoryFactoryMock.Verify(p => p.Repository<Customer>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Customer, bool>>>()), Times.Once);
            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Remove(It.IsAny<Customer>()), Times.Once);
            _unitOfWorkMock.Verify(p => p.SaveChanges(true, false), Times.Once);
            _repositoryFactoryMock.Verify(p => p.Repository<Customer>().FirstOrDefaultAsync(It.IsAny<IQuery<Customer>>(), default), Times.Once);
        }

        [Fact]
        public async void Should_GetAll_SucessFully()
        {
            List<Customer> customerList = CustomerFixture.GenerateCustomerFixture(2);

            _repositoryFactoryMock.Setup(p => p.Repository<Customer>().SearchAsync(It.IsAny<IMultipleResultQuery<Customer>>(), default)).ReturnsAsync(customerList);

            var customers = await _customerService.GetAllAsync();

            customers.Should().HaveCount(2);

            _repositoryFactoryMock.Verify(p => p.Repository<Customer>().SearchAsync(It.IsAny<IMultipleResultQuery<Customer>>(), default), Times.Once);
        }

        [Fact]
        public async void Should_GetByCpf_SucessFully()
        {
            var cpf = "42713070848";
            var customer = CustomerFixture.GenerateCustomerFixture();

            _repositoryFactoryMock.Setup(p => p.Repository<Customer>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Customer, bool>>>())).Returns(It.IsAny<IQuery<Customer>>());
            _repositoryFactoryMock.Setup(p => p.Repository<Customer>().FirstOrDefaultAsync(It.IsAny<IQuery<Customer>>(), default)).ReturnsAsync(customer);

            var result = await _customerService.GetByCpfAsync(cpf);

            result.Should().Be(customer);

            _repositoryFactoryMock.Verify(p => p.Repository<Customer>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Customer, bool>>>()), Times.Once);
            _repositoryFactoryMock.Verify(p => p.Repository<Customer>().FirstOrDefaultAsync(It.IsAny<IQuery<Customer>>(), default), Times.Once);
        }

        [Fact]
        public async void Should_Not_GetByCpf_When_Cpf_Dismatch()
        {
            var cpf = "42713070848";
            var customer = CustomerFixture.GenerateCustomerFixture();

            _repositoryFactoryMock.Setup(p => p.Repository<Customer>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Customer, bool>>>())).Returns(It.IsAny<IQuery<Customer>>());
            _repositoryFactoryMock.Setup(p => p.Repository<Customer>().FirstOrDefaultAsync(It.IsAny<IQuery<Customer>>(), default));

            var action = () => _customerService.GetByCpfAsync(cpf);

            await action.Should().ThrowAsync<ArgumentNullException>($"Não foi encontrado Customer para o CPF: {cpf}");

            _repositoryFactoryMock.Verify(p => p.Repository<Customer>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Customer, bool>>>()), Times.Once);
            _repositoryFactoryMock.Verify(p => p.Repository<Customer>().FirstOrDefaultAsync(It.IsAny<IQuery<Customer>>(), default), Times.Once);
        }

        [Fact]
        public void Should_Update_Sucessfully()
        {
            var customer = CustomerFixture.GenerateCustomerFixture();
            customer.Id = 1;

            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Any(custom => custom.Id == customer.Id)).Returns(true);
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Any(custom => custom.Email == customer.Email && custom.Id != customer.Id)).Returns(false);
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Any(custom => custom.Cpf == customer.Cpf && custom.Id != customer.Id)).Returns(false);
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Update(It.IsAny<Customer>()));
            _unitOfWorkMock.Setup(p => p.SaveChanges(true, false));

            _customerService.Update(customer);

            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Any(custom => custom.Id == customer.Id), Times.Once);
            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Any(custom => custom.Email == customer.Email && custom.Id != customer.Id), Times.Once);
            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Any(custom => custom.Cpf == customer.Cpf && custom.Id != customer.Id), Times.Once);
            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Update(It.IsAny<Customer>()), Times.Once);
            _unitOfWorkMock.Verify(p => p.SaveChanges(true, false), Times.Once);
        }

        [Fact]
        public void Should_Not_Update_When_Id_Dismatch()
        {
            var customer = CustomerFixture.GenerateCustomerFixture();

            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Any(It.IsAny<Expression<Func<Customer, bool>>>())).Returns(false);
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Update(It.IsAny<Customer>()));
            _unitOfWorkMock.Setup(p => p.SaveChanges(true, false));

            var action = () => _customerService.Update(customer);

            action.Should().Throw<ArgumentNullException>($"Não foi encontrado Customer para o Id: {customer.Id}");

            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Any(It.IsAny<Expression<Func<Customer, bool>>>()), Times.Once);
            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Update(It.IsAny<Customer>()), Times.Never);
            _unitOfWorkMock.Verify(p => p.SaveChanges(true, false), Times.Never);
        }

        [Fact]
        public void Should_Not_Update_When_Cpf_Already_Exists()
        {
            var customer = CustomerFixture.GenerateCustomerFixture();

            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Any(custom => custom.Id == customer.Id)).Returns(true);
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Any(custom => custom.Email == customer.Email && custom.Id != customer.Id)).Returns(false);
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Any(custom => custom.Cpf == customer.Cpf && custom.Id != customer.Id)).Returns(true);
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Update(It.IsAny<Customer>()));
            _unitOfWorkMock.Setup(p => p.SaveChanges(true, false));

            var action = () => _customerService.Update(customer);

            action.Should().Throw<ArgumentException>($"Já existe usuário com o CPF {customer.Cpf}");

            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Any(custom => custom.Id == customer.Id), Times.Once);
            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Any(custom => custom.Email == customer.Email && custom.Id != customer.Id), Times.Once);
            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Any(custom => custom.Cpf == customer.Cpf && custom.Id != customer.Id), Times.Once);
            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Update(It.IsAny<Customer>()), Times.Never);
            _unitOfWorkMock.Verify(p => p.SaveChanges(true, false), Times.Never);
        }

        [Fact]
        public void Should_Not_Update_When_Email_Already_Exists()
        {
            var customer = CustomerFixture.GenerateCustomerFixture();

            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Any(It.IsAny<Expression<Func<Customer, bool>>>())).Returns(true);
            _unitOfWorkMock.Setup(p => p.Repository<Customer>().Update(It.IsAny<Customer>()));
            _unitOfWorkMock.Setup(p => p.SaveChanges(true, false));

            var action = () => _customerService.Update(customer);

            action.Should().Throw<ArgumentException>($"Já existe usuário com o Email {customer.Email}");

            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Any(It.IsAny<Expression<Func<Customer, bool>>>()), Times.Exactly(2));
            _unitOfWorkMock.Verify(p => p.Repository<Customer>().Update(It.IsAny<Customer>()), Times.Never);
            _unitOfWorkMock.Verify(p => p.SaveChanges(true, false), Times.Never);
        }

        [Fact]
        public async void Should_GetById_SucessFully()
        {
            var id = 1;
            var customer = CustomerFixture.GenerateCustomerFixture();

            _repositoryFactoryMock.Setup(p => p.Repository<Customer>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Customer, bool>>>())).Returns(It.IsAny<IQuery<Customer>>());
            _repositoryFactoryMock.Setup(p => p.Repository<Customer>().FirstOrDefaultAsync(It.IsAny<IQuery<Customer>>(), default)).ReturnsAsync(customer);

            var customers = await _customerService.GetByIdAsync(id);

            customers.Should().Be(customer);

            _repositoryFactoryMock.Verify(p => p.Repository<Customer>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Customer, bool>>>()), Times.Once);
            _repositoryFactoryMock.Verify(p => p.Repository<Customer>().FirstOrDefaultAsync(It.IsAny<IQuery<Customer>>(), default), Times.Once);
        }

        [Fact]
        public async void Should_Not_GetById_When_Id_Dismatch()
        {
            var id = 0;
            var customer = CustomerFixture.GenerateCustomerFixture();

            _repositoryFactoryMock.Setup(p => p.Repository<Customer>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Customer, bool>>>())).Returns(It.IsAny<IQuery<Customer>>());
            _repositoryFactoryMock.Setup(p => p.Repository<Customer>().FirstOrDefaultAsync(It.IsAny<SingleResultQuery<Customer>>(), default));

            var action = () => _customerService.GetByIdAsync(id);

            await action.Should().ThrowAsync<ArgumentNullException>($"Não foi encontrado Customer para o Id: {id}");

            _repositoryFactoryMock.Verify(p => p.Repository<Customer>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Customer, bool>>>()), Times.Once);
            _repositoryFactoryMock.Verify(p => p.Repository<Customer>().FirstOrDefaultAsync(It.IsAny<SingleResultQuery<Customer>>(), default), Times.Once);
        }
    }
}
