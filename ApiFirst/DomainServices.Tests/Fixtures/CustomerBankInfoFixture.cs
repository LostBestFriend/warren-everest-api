using Bogus;
using DomainModels.Models;
using System.Collections.Generic;

namespace DomainServices.Tests.Fixtures
{
    public class CustomerBankInfoFixture
    {
        public static List<CustomerBankInfo> GenerateCustomerBankInfoFixture(int quantity)
        {
            return new Faker<CustomerBankInfo>("en_US")
                .CustomInstantiator(p => new CustomerBankInfo(customerId: 1))
                .Generate(quantity);
        }
        public static CustomerBankInfo GenerateCustomerBankInfoFixture()
        {
            return new Faker<CustomerBankInfo>("en_US")
                .CustomInstantiator(p => new CustomerBankInfo(customerId: 1))
                .Generate();
        }
    }
}
