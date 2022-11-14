using AppModels.AppModels.Customers;
using Bogus;
using Bogus.Extensions.Brazil;
using System;
using System.Collections.Generic;

namespace ApiFirst.Tests.Fixtures.AppServices.Customer
{
    public class CreateCustomerFixture
    {
        public static List<CreateCustomer> GenerateCreateCustomerFixture(int quantity)
        {
            Faker e = new Faker();
            string email = e.Internet.Email();

            return new Faker<CreateCustomer>("en_US")
                .CustomInstantiator(p => new CreateCustomer(
                    cpf: p.Person.Cpf(),
                    country: p.Address.Country(),
                    city: p.Address.City(),
                    cellphone: p.Phone.PhoneNumberFormat(),
                    whatsapp: p.Random.Bool(),
                    emailSms: p.Random.Bool(),
                    email: email,
                    emailConfirmation: email,
                    number: int.Parse(p.Address.BuildingNumber()),
                    address: p.Address.StreetAddress(),
                    fullName: p.Person.FullName,
                    dateOfBirth: DateTime.Now.AddYears(-20),
                    postalCode: p.Address.ZipCode()))
                .Generate(quantity);
        }
        public static CreateCustomer GenerateCreateCustomerFixture()
        {
            Faker e = new Faker();
            string email = e.Internet.Email();

            return new Faker<CreateCustomer>("en_US")
                .CustomInstantiator(p => new CreateCustomer(
                    cpf: p.Person.Cpf(),
                    country: p.Address.Country(),
                    city: p.Address.City(),
                    cellphone: p.Phone.PhoneNumberFormat(),
                    whatsapp: p.Random.Bool(),
                    emailSms: p.Random.Bool(),
                    email: email,
                    emailConfirmation: email,
                    number: int.Parse(p.Address.BuildingNumber()),
                    address: p.Address.StreetAddress(),
                    fullName: p.Person.FullName,
                    dateOfBirth: DateTime.Now.AddYears(-20),
                    postalCode: p.Address.ZipCode()))
                .Generate();
        }
    }
}
