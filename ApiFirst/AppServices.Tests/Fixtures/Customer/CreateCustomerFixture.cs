using AppModels.AppModels.Customer;
using Bogus;
using Moq;
using System.Collections.Generic;

namespace AppServices.Tests.Fixtures.Customer
{
    public class CreateCustomerFixture
    {
        public static List<CreateCustomer> GenerateCustomerFixture(int quantity)
        {
            return new Faker<CreateCustomer>("en_US")
                .CustomInstantiator(p => new CreateCustomer(cpf: "42713070848", country: "Los Angeles",
                city: "America", cellphone: "1234566", whatsapp: false, emailSms: false, email:
                "a@g", number: It.IsAny<int>(), address: "axes", fullName: "Rob", emailConfirmation: "a@g",
                dateOfBirth: System.DateTime.Now.AddYears(-18), postalCode: "89035360"))
                .Generate(quantity);
        }
        public static CreateCustomer GenerateCustomerFixture()
        {
            return new Faker<CreateCustomer>("en_US")
                .CustomInstantiator(p => new CreateCustomer(cpf: "42713070848", country: "Los Angeles",
                city: "America", cellphone: "1234566", whatsapp: false, emailSms: false, email:
                "a@g", number: It.IsAny<int>(), address: "axes", fullName: "Rob", emailConfirmation: "a@g",
                dateOfBirth: System.DateTime.Now.AddYears(-18), postalCode: "89035360"))
                .Generate();
        }
    }
}
