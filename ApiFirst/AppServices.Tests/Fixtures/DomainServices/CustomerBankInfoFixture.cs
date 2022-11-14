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
                    customerId: p.Random.Long(0, 10),
                    accountBalance: p.Random.Decimal(0, 200000000),
                    customer: CustomerFixture.GenerateCustomerFixture()))
                .Generate(quantity);
        }
        public static CustomerBankInfo GenerateCustomerBankInfoFixture()
        {
            return new Faker<CustomerBankInfo>("en_US")
                .CustomInstantiator(p => new CustomerBankInfo(
                    customerId: p.Random.Long(0, 10),
                    accountBalance: p.Random.Decimal(0, 200000000),
                    customer: CustomerFixture.GenerateCustomerFixture()))
                .Generate();
        }
    }
}
