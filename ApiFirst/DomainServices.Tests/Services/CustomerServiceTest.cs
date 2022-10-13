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
            var customer = new Faker<Customer>("en_US")
                .RuleFor(p => p.Cpf, f => f.Person.Cpf())
                .RuleFor(p => p.FullName, c => c.Name.FullName())
                .RuleFor(p => p.Email, c => c.Person.Email)
                .RuleFor(p => p.Address, c => c.Address.FullAddress())
                .RuleFor(p => p.Country, c => c.Address.Country())
                .RuleFor(p => p.City, c => c.Address.City())
                .RuleFor(p => p.PostalCode, c => c.Address.Locale)
                .RuleFor(p => p.Whatsapp, c => c.PickRandom<bool>())
                .RuleFor(p => p.EmailSms, c => c.PickRandom<bool>())
                .RuleFor(p => p.Number, c => int.Parse(c.Address.BuildingNumber()))
                .RuleFor(p => p.Cellphone, c => c.Phone.PhoneNumber())
                .RuleFor(p => p.DateOfBirth, c => c.Person.DateOfBirth);

            customer.Generate();
        }
    }
}
