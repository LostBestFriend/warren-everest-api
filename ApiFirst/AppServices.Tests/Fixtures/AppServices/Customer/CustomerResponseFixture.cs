using AppModels.AppModels.Customers;
using Bogus;
using Bogus.Extensions.Brazil;
using System;
using System.Collections.Generic;

namespace ApiFirst.Tests.Fixtures.AppServices.Customer
{
    public class CustomerResponseFixture
    {
        public static List<CustomerResponse> GenerateCustomerResponseFixture(int quantity)
        {
            return new Faker<CustomerResponse>("en_US")
                .CustomInstantiator(p => new CustomerResponse(
                    id: p.Random.Long(0, 10),
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
        public static CustomerResponse GenerateCustomerResponseFixture()
        {
            return new Faker<CustomerResponse>("en_US")
                .CustomInstantiator(p => new CustomerResponse(
                    id: p.Random.Long(0, 10),
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
