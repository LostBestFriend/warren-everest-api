using AppModels.AppModels.Customer;
using Bogus;
using Moq;
using System.Collections.Generic;

namespace AppServices.Tests.Fixtures.Customer
{
    public class UpdateCustomerFixture
    {
        public static List<UpdateCustomer> GenerateCustomerFixture(int quantity)
        {
            return new Faker<UpdateCustomer>("en_US")
                .CustomInstantiator(p => new UpdateCustomer(cpf: "42713070848", country: "Los Angeles",
                city: "America", cellphone: "1234566", whatsapp: false, emailSms: false, email:
                "a@g", number: It.IsAny<int>(), address: "axes", fullName: "Rob",
                dateOfBirth: System.DateTime.Now.AddYears(-18), postalCode: "89035360"))
                .Generate(quantity);
        }
        public static UpdateCustomer GenerateCustomerFixture()
        {
            return new Faker<UpdateCustomer>("en_US")
                .CustomInstantiator(p => new UpdateCustomer(cpf: "42713070848", country: "Los Angeles",
                city: "America", cellphone: "1234566", whatsapp: false, emailSms: false, email:
                "a@g", number: It.IsAny<int>(), address: "axes", fullName: "Rob",
                dateOfBirth: System.DateTime.Now.AddYears(-18), postalCode: "89035360"))
                .Generate();
        }
    }
}
