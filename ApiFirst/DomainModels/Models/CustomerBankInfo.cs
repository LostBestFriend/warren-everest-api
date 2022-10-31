namespace DomainModels.Models
{
    public class CustomerBankInfo : BaseModel
    {
        public CustomerBankInfo(long customerId)
        {
            CustomerId = customerId;
        }

        public CustomerBankInfo(decimal accountBalance, long customerId, Customer customer)
        {
            AccountBalance = accountBalance;
            CustomerId = customerId;
            Customer = customer;
        }

        public decimal AccountBalance { get; set; }
        public long CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
