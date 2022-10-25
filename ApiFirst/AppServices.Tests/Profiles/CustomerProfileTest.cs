using AppModels.AppModels.Customers;
using AutoMapper;
using DomainModels.Models;
using FluentAssertions;
using Moq;
using System;
using Xunit;

namespace AppServices.Tests.Profiles
{
    public class CustomerProfileTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IMapper> _mapperMock;

        public CustomerProfileTest()
        {
            _mapperMock = new();
            _mapper = _mapperMock.Object;
        }

        [Fact]
        public void Should_Map_UpdateCustomer_Sucessfully()
        {
            var customer = new Customer(fullName: "João", email: "a@g",
                cpf: "42713070848", cellphone: "991541506",
                dateOfBirth: DateTime.Now.AddYears(-18),
                emailSms: true, whatsapp: true, country: "Brazil",
                city: "Blumenau", postalCode: "89035360",
                address: "Rua Prudente", number: 1234567890);

            var updateCustomer = new UpdateCustomer(fullName: "João", email: "a@g",
                cpf: "42713070848", cellphone: "991541506",
                dateOfBirth: DateTime.Now.AddYears(-18),
                emailSms: true, whatsapp: true, country: "Brazil",
                city: "Blumenau", postalCode: "89035360",
                address: "Rua Prudente", number: 1234567890);

            _mapperMock.Setup(p => p.Map<Customer>(It.IsAny<UpdateCustomer>())).Returns(customer);

            var result = _mapper.Map<Customer>(updateCustomer);

            result.Should().BeEquivalentTo(customer);

            _mapperMock.Verify(p => p.Map<Customer>(It.IsAny<UpdateCustomer>()), Times.Once);
        }

        [Fact]
        public void Should_Map_CreateCustomer_Sucessfully()
        {
            var customer = new Customer(fullName: "João", email: "a@g",
                cpf: "42713070848", cellphone: "991541506",
                dateOfBirth: DateTime.Now.AddYears(-18),
                emailSms: true, whatsapp: true, country: "Brazil",
                city: "Blumenau", postalCode: "89035360",
                address: "Rua Prudente", number: 1234567890);
            var createCustomer = new CreateCustomer(fullName: "João", email: "a@g", emailConfirmation: "a@g",
                cpf: "42713070848", cellphone: "991541506",
                dateOfBirth: DateTime.Now.AddYears(-18),
                emailSms: true, whatsapp: true, country: "Brazil",
                city: "Blumenau", postalCode: "89035360",
                address: "Rua Prudente", number: 1234567890);

            _mapperMock.Setup(p => p.Map<Customer>(It.IsAny<CreateCustomer>())).Returns(customer);

            var result = _mapper.Map<Customer>(createCustomer);

            _mapperMock.Verify(p => p.Map<Customer>(It.IsAny<CreateCustomer>()), Times.Once);

            result.Should().BeEquivalentTo(customer);
        }

        [Fact]
        public void Should_Map_CustomerResponse_Sucessfully()
        {
            var customer = new Customer(fullName: "João", email: "a@g",
                cpf: "42713070848", cellphone: "991541506",
                dateOfBirth: DateTime.Now.AddYears(-18),
                emailSms: true, whatsapp: true, country: "Brazil",
                city: "Blumenau", postalCode: "89035360",
                address: "Rua Prudente", number: 1234567890);
            var customerResponse = new CustomerResponse(id: 1, fullName: "João", email: "a@g",
                cpf: "42713070848", cellphone: "991541506",
                dateOfBirth: DateTime.Now.AddYears(-18),
                emailSms: true, whatsapp: true, country: "Brazil",
                city: "Blumenau", postalCode: "89035360",
                address: "Rua Prudente", number: 1234567890);

            _mapperMock.Setup(p => p.Map<CustomerResponse>(It.IsAny<Customer>())).Returns(customerResponse);

            var result = _mapper.Map<CustomerResponse>(customer);

            result.Should().BeEquivalentTo(customerResponse);

            _mapperMock.Verify(p => p.Map<CustomerResponse>(It.IsAny<Customer>()), Times.Once);
        }
    }
}
