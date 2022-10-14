using Bogus;
using DomainModels.Models;
using System.Collections.Generic;

namespace DomainServices.Tests.Fixtures
{
    public class CustomerFixture
    {
        public static List<Customer> GenerateCustomerFixture(int quantity)
        {
            return new Faker<Customer>("en_US")
                .CustomInstantiator(p => new Customer(cpf: "42713070848", country: "Los Angeles",
                city: "America", cellphone: "1234566", whatsapp: false, emailSms: false, email:
                "a@g", number: 123456789, address: "axes", fullName: "Rob",
                dateOfBirth: System.DateTime.Now.AddYears(-18), postalCode: "89035360"))
                .Generate(quantity);
        }
        public static Customer GenerateCustomerFixture()
        {
            return new Faker<Customer>("en_US")
                .CustomInstantiator(p => new Customer(cpf: "42713070848", country: "Los Angeles",
                city: "America", cellphone: "1234566", whatsapp: false, emailSms: false, email:
                "a@g", number: 123456789, address: "axes", fullName: "Rob",
                dateOfBirth: System.DateTime.Now.AddYears(-18), postalCode: "89035360"))
                .Generate();
        }
    }
}
