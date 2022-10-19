using AppModels.AppModels.Customer;
using AppModels.AppModels.Order;
using AppModels.AppModels.PortfolioProduct;
using System.Collections.Generic;

namespace AppModels.AppModels.Portfolio
{
    public class PortfolioResponse
    {
        public PortfolioResponse(long id, string name, string description, decimal totalBalance, decimal accountBalance, CustomerResponse customer, IEnumerable<PortfolioProductResponse> products, IEnumerable<OrderResponse> orders)
        {
            Id = id;
            Name = name;
            Description = description;
            TotalBalance = totalBalance;
            AccountBalance = accountBalance;
            Customer = customer;
            Products = products;
            Orders = orders;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal TotalBalance { get; set; }
        public decimal AccountBalance { get; set; }
        public CustomerResponse Customer { get; set; }
        public IEnumerable<PortfolioProductResponse> Products { get; set; }
        public IEnumerable<OrderResponse> Orders { get; set; }
    }
}
