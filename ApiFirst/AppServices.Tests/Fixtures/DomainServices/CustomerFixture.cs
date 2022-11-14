using Bogus;
using Bogus.Extensions.Brazil;
using DomainModels.Models;
using System;
using System.Collections.Generic;

namespace ApiFirst.Tests.Fixtures.DomainServices
{
    public class CustomerFixture
    {
        public static List<Customer> GenerateCustomerFixture(int quantity)
        {
            return new Faker<Customer>("en_US")
                .CustomInstantiator(p => new Customer(
                    cpf: p.Person.Cpf(),
                    country: p.Address.Country(),
                    city: p.Address.City(),
                    cellphone: p.Phone.PhoneNumberFormat(),
                    whatsapp: p.Random.Bool(),
                    emailSms: p.Random.Bool(),
                    email: p.Internet.Email(),
                    number: int.Parse(p.Address.BuildingNumber()),
                    address: p.Address.StreetAddress(),
                    fullName: p.Person.FullName,
                    dateOfBirth: DateTime.Now.AddYears(-20),
                    postalCode: p.Address.ZipCode()))
                .Generate(quantity);
        }
        public static Customer GenerateCustomerFixture()
        {
            return new Faker<Customer>("en_US")
                .CustomInstantiator(p => new Customer(
                    cpf: p.Person.Cpf(),
                    country: p.Address.Country(),
                    city: p.Address.City(),
                    cellphone: p.Phone.PhoneNumberFormat(),
                    whatsapp: p.Random.Bool(),
                    emailSms: p.Random.Bool(),
                    email: p.Internet.Email(),
                    number: int.Parse(p.Address.BuildingNumber()),
                    address: p.Address.StreetAddress(),
                    fullName: p.Person.FullName,
                    dateOfBirth: DateTime.Now.AddYears(-20),
                    postalCode: p.Address.ZipCode()))
                .Generate();
        }
    }
}
