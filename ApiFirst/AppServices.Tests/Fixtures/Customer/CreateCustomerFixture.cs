using AppModels.AppModels.Customer;
using Bogus;
using Bogus.Extensions.Brazil;
using System;
using System.Collections.Generic;

namespace AppServices.Tests.Fixtures.Customer
{
    public class CreateCustomerFixture
    {
        public static List<CreateCustomer> GenerateCustomerFixture(int quantity)
        {
            return new Faker<CreateCustomer>("en_US")
                .CustomInstantiator(p => new CreateCustomer(cpf: p.Person.Cpf(false), country: p.Address.Country(),
                city: p.Address.City(), cellphone: p.Phone.PhoneNumber(), whatsapp: p.PickRandom(true, false), emailSms: p.PickRandom(true, false), email:
                p.Person.Email, emailConfirmation: p.Person.Email, number: int.Parse(p.Address.BuildingNumber()), address: p.Address.FullAddress(), fullName: p.Person.FullName,
                dateOfBirth: p.Date.BetweenDateOnly(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddYears(-18))).ToDateTime(TimeOnly.FromDateTime(DateTime.Now)), postalCode: p.Address.ZipCode()))
                .Generate(quantity);
        }
        public static CreateCustomer GenerateCustomerFixture()
        {
            return new Faker<CreateCustomer>("en_US")
                .CustomInstantiator(p => new CreateCustomer(cpf: p.Person.Cpf(false), country: p.Address.Country(),
                city: p.Address.City(), cellphone: p.Phone.PhoneNumber(), whatsapp: p.PickRandom(false, true), emailSms: p.PickRandom(true, false), email:
                p.Person.Email, emailConfirmation: p.Person.Email, number: int.Parse(p.Address.BuildingNumber()), address: p.Address.FullAddress(), fullName: p.Person.FullName,
                dateOfBirth: p.Date.BetweenDateOnly(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddYears(-18))).ToDateTime(TimeOnly.FromDateTime(DateTime.Now)), postalCode: p.Address.ZipCode()))
                .Generate();
        }
    }
}
