using Bogus;
using DomainModels.Models;
using System.Collections.Generic;

namespace ApiFirst.Tests.Fixtures.DomainServices
{
    public class CustomerBankInfoFixture
    {
        public static List<CustomerBankInfo> GenerateCustomerBankInfoFixture(int quantity)
        {
            return new Faker<CustomerBankInfo>("en_US")
                .CustomInstantiator(p => new CustomerBankInfo(
                    customerId: 1,
                    accountBalance: 1000,
                    customer: CustomerFixture.GenerateCustomerFixture()))
                .Generate(quantity);
        }
        public static CustomerBankInfo GenerateCustomerBankInfoFixture()
        {
            return new Faker<CustomerBankInfo>("en_US")
                .CustomInstantiator(p => new CustomerBankInfo(
                    customerId: 1,
                    accountBalance: 1000,
                    customer: CustomerFixture.GenerateCustomerFixture()))
                .Generate();
        }
    }
}
