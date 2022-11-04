using Bogus;
using Bogus.Extensions.Brazil;
using DomainModels.Models;
using System;
using System.Collections.Generic;

namespace DomainServices.Tests.Fixtures
{
    public class CustomerFixture
    {
        public static List<Customer> GenerateCustomerFixture(int quantity)
        {
            return new Faker<Customer>("en_US")
                .CustomInstantiator(p => new Customer(
                    cpf: p.Person.Cpf(false),
                    country: p.Address.Country(),
                    city: p.Address.City(),
                    cellphone: p.Phone.PhoneNumber(),
                    whatsapp: p.PickRandom(false, true),
                    emailSms: p.PickRandom(false, true),
                    email: p.Person.Email,
                    number: int.Parse(p.Address.BuildingNumber()),
                    address: p.Address.FullAddress(),
                    fullName: p.Person.FullName,
                    dateOfBirth: DateTime.Now.AddYears(-2),
                    postalCode: p.Locale))
                .Generate(quantity);
        }
        public static Customer GenerateCustomerFixture()
        {
            return new Faker<Customer>("en_US")
                .CustomInstantiator(p => new Customer(
                    cpf: p.Person.Cpf(false),
                    country: p.Address.Country(),
                    city: p.Address.City(),
                    cellphone: p.Phone.PhoneNumber(),
                    whatsapp: p.PickRandom(false, true),
                    emailSms: p.PickRandom(false, true),
                    email: p.Person.Email,
                    number: int.Parse(p.Address.BuildingNumber()),
                    address: p.Address.FullAddress(),
                    fullName: p.Person.FullName,
                    dateOfBirth: DateTime.Now.AddYears(-2),
                    postalCode: p.Locale))
                .Generate();
        }
    }
}
