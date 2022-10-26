using AppModels.AppModels.Customers;
using AppServices.Validator.Customers;
using FluentAssertions;
using System;
using Xunit;

namespace AppServices.Tests.Validator
{
    public class CustomerValidatorTest
    {
        [Fact]
        public void Should_Validate_CreateCustomer_Sucessfully()
        {
            var createCustomer = new CreateCustomer(fullName: "João Pedro", email: "aaaaaaaaaaaaa@g", emailConfirmation: "aaaaaaaaaaaaa@g",
                cpf: "42713070848", cellphone: "47991541506",
                dateOfBirth: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                emailSms: true, whatsapp: true, country: "Brazil",
                city: "Blumenau", postalCode: "89035360",
                address: "Rua Prudente de Moraes", number: 123);
            var validator = new CustomerCreateValidator();

            var result = validator.Validate(createCustomer);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Should_Validate_UpdateCustomer_Sucessfully()
        {
            var updateCustomer = new UpdateCustomer(fullName: "João Pedro", email: "aaaaaaaaaaaaa@g",
                cpf: "42713070848", cellphone: "47991541506",
                dateOfBirth: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                emailSms: true, whatsapp: true, country: "Brazil",
                city: "Blumenau", postalCode: "89035360",
                address: "Rua Prudente de Moraes", number: 123);
            var validator = new CustomerUpdateValidator();

            var result = validator.Validate(updateCustomer);

            result.IsValid.Should().BeTrue();
        }
    }
}
