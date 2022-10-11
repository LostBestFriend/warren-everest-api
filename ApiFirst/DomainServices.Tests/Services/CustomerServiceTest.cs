using Bogus;
using Bogus.Extensions.Brazil;
using DomainModels.Models;
using Xunit;

namespace DomainServices.Tests.Services
{
    public class CustomerServiceTest
    {
        [Fact]
        public void Should_Create_SucessFully()
        {
            var customer = new Faker<Customer>("en_US").CustomInstantiator(
                c => new Customer(
                fullName: c.Name.FullName(),
                email: c.Person.Email,
                cpf: c.Person.Cpf(),
                address: c.Address.FullAddress(),
                country: c.Address.Country(),
                city: c.Address.City(),
                postalCode: c.Address.Locale,
                whatsapp: c.PickRandom<bool>(),
                emailSms: c.PickRandom<bool>(),
                number: int.Parse(c.Address.BuildingNumber()),
                cellphone: c.Phone.PhoneNumber(),
                dateOfBirth: c.Person.DateOfBirth
                )
            );
        }
    }
}
