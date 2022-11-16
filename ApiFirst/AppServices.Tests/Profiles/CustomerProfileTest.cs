using ApiFirst.Tests.Fixtures.DomainServices;
using AppModels.AppModels.Customers;
using AppServices.Profiles;
using AutoMapper;
using DomainModels.Models;
using FluentAssertions;
using Xunit;

namespace ApiFirst.Tests.Profiles
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
            var customer = CustomerFixture.GenerateCustomerFixture();

            var updateCustomer = new UpdateCustomer(
                fullName: customer.FullName,
                email: customer.Email,
                cpf: customer.Cpf,
                cellphone: customer.Cellphone,
                dateOfBirth: customer.DateOfBirth,
                emailSms: customer.EmailSms,
                whatsapp: customer.Whatsapp,
                country: customer.Country,
                city: customer.City,
                postalCode: customer.PostalCode,
                address: customer.Address,
                number: customer.Number);

            var result = _mapper.Map<Customer>(updateCustomer);

            result.Should().BeEquivalentTo(customer);

        }

        [Fact]
        public void Should_Map_CreateCustomer_Sucessfully()
        {
            var customer = CustomerFixture.GenerateCustomerFixture();

            var createCustomer = new CreateCustomer(
                fullName: customer.FullName,
                email: customer.Email,
                emailConfirmation: customer.Email,
                cpf: customer.Cpf,
                cellphone: customer.Cellphone,
                dateOfBirth: customer.DateOfBirth,
                emailSms: customer.EmailSms,
                whatsapp: customer.Whatsapp,
                country: customer.Country,
                city: customer.City,
                postalCode: customer.PostalCode,
                address: customer.Address,
                number: customer.Number);

            var result = _mapper.Map<Customer>(createCustomer);

            result.Should().BeEquivalentTo(customer);
        }

        [Fact]
        public void Should_Map_CustomerResponse_Sucessfully()
        {
            var customer = CustomerFixture.GenerateCustomerFixture();

            var result = _mapper.Map<CustomerResponse>(customer);

            result.Should().NotBeNull();
        }
    }
}
