using AppModels.AppModels.Customer;
using Bogus;
using Bogus.Extensions.Brazil;
using System;
using System.Collections.Generic;

namespace AppServices.Tests.Fixtures.Customer
{
    public class CustomerResponseFixture
    {
        public static List<CustomerResponse> GenerateCustomerResponseFixture(int quantity)
        {
            return new Faker<CustomerResponse>("en_US")
                .CustomInstantiator(p => new CustomerResponse(id: 1, cpf: p.Person.Cpf(false), country: p.Address.Country(),
                city: p.Address.City(), cellphone: p.Phone.PhoneNumber(), whatsapp: p.PickRandom(false, true), emailSms: p.PickRandom(true, false), email:
                p.Person.Email, number: int.Parse(p.Address.BuildingNumber()), address: p.Address.FullAddress(), fullName: p.Person.FullName,
                dateOfBirth: p.Date.BetweenDateOnly(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddYears(-18))).ToDateTime(TimeOnly.FromDateTime(DateTime.Now)), postalCode: p.Address.ZipCode()))
                .Generate(quantity);
        }
        public static CustomerResponse GenerateCustomerResponseFixture()
        {
            return new Faker<CustomerResponse>("en_US")
                .CustomInstantiator(p => new CustomerResponse(id: 1, cpf: p.Person.Cpf(false), country: p.Address.Country(),
                city: p.Address.City(), cellphone: p.Phone.PhoneNumber(), whatsapp: p.PickRandom(false, true), emailSms: p.PickRandom(false, true), email:
                p.Person.Email, number: int.Parse(p.Address.BuildingNumber()), address: p.Address.FullAddress(), fullName: p.Person.FullName,
                dateOfBirth: p.Date.BetweenDateOnly(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddYears(-18))).ToDateTime(TimeOnly.FromDateTime(DateTime.Now)), postalCode: p.Address.ZipCode()))
                .Generate();
        }
    }
}
