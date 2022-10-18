using AppModels.AppModels.Customer;
using Bogus;
using Moq;
using System.Collections.Generic;

namespace AppServices.Tests.Fixtures.Customer
{
    public class CustomerResponseFixture
    {
        public static List<CustomerResponse> GenerateCustomerFixture(int quantity)
        {
            return new Faker<CustomerResponse>("en_US")
                .CustomInstantiator(p => new CustomerResponse(id: 1, cpf: "42713070848", country: "Los Angeles",
                city: "America", cellphone: "1234566", whatsapp: false, emailSms: false, email:
                "a@g", number: It.IsAny<int>(), address: "axes", fullName: "Rob",
                dateOfBirth: System.DateTime.Now.AddYears(-18), postalCode: "89035360"))
                .Generate(quantity);
        }
        public static CustomerResponse GenerateCustomerFixture()
        {
            return new Faker<CustomerResponse>("en_US")
                .CustomInstantiator(p => new CustomerResponse(id: 1, cpf: "42713070848", country: "Los Angeles",
                city: "America", cellphone: "1234566", whatsapp: false, emailSms: false, email:
                "a@g", number: It.IsAny<int>(), address: "axes", fullName: "Rob",
                dateOfBirth: System.DateTime.Now.AddYears(-18), postalCode: "89035360"))
                .Generate();
        }
    }
}
