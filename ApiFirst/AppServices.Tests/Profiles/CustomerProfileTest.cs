using AppModels.AppModels.Customers;
using AppServices.Profiles;
using AutoMapper;
using DomainModels.Models;
using FluentAssertions;
using System;
using Xunit;

namespace AppServices.Tests.Profiles
{
    public class CustomerProfileTest
    {
        private readonly IMapper _mapper;


        public CustomerProfileTest()
        {
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<CustomerProfile>()).CreateMapper();
        }

        [Fact]
        public void Should_Map_UpdateCustomer_Sucessfully()
        {
            var customer = new Customer(fullName: "João", email: "a@g",
                cpf: "42713070848", cellphone: "991541506",
                dateOfBirth: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                emailSms: true, whatsapp: true, country: "Brazil",
                city: "Blumenau", postalCode: "89035360",
                address: "Rua Prudente", number: 1234567890);

            var updateCustomer = new UpdateCustomer(fullName: "João", email: "a@g",
                cpf: "42713070848", cellphone: "991541506",
                dateOfBirth: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                emailSms: true, whatsapp: true, country: "Brazil",
                city: "Blumenau", postalCode: "89035360",
                address: "Rua Prudente", number: 1234567890);

            var result = _mapper.Map<Customer>(updateCustomer);

            result.Should().BeEquivalentTo(customer);

        }

        [Fact]
        public void Should_Map_CreateCustomer_Sucessfully()
        {
            var customer = new Customer(fullName: "João", email: "a@g",
                cpf: "42713070848", cellphone: "991541506",
                dateOfBirth: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                emailSms: true, whatsapp: true, country: "Brazil",
                city: "Blumenau", postalCode: "89035360",
                address: "Rua Prudente", number: 1234567890);
            var createCustomer = new CreateCustomer(fullName: "João", email: "a@g", emailConfirmation: "a@g",
                cpf: "42713070848", cellphone: "991541506",
                dateOfBirth: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                emailSms: true, whatsapp: true, country: "Brazil",
                city: "Blumenau", postalCode: "89035360",
                address: "Rua Prudente", number: 1234567890);

            var result = _mapper.Map<Customer>(createCustomer);

            result.Should().BeEquivalentTo(customer);
        }

        [Fact]
        public void Should_Map_CustomerResponse_Sucessfully()
        {
            var customer = new Customer(fullName: "João", email: "a@g",
                cpf: "42713070848", cellphone: "991541506",
                dateOfBirth: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                emailSms: true, whatsapp: true, country: "Brazil",
                city: "Blumenau", postalCode: "89035360",
                address: "Rua Prudente", number: 1234567890);
            var customerResponse = new CustomerResponse(id: 1, fullName: "João", email: "a@g",
                cpf: "42713070848", cellphone: "991541506",
                dateOfBirth: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                emailSms: true, whatsapp: true, country: "Brazil",
                city: "Blumenau", postalCode: "89035360",
                address: "Rua Prudente", number: 1234567890);

            var result = _mapper.Map<CustomerResponse>(customer);

            result.Should().NotBeNull();
        }
    }
}
