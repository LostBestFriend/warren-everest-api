using AppModels.AppModels.Customers;
using AppModels.AppModels.Orders;
using AppModels.AppModels.PortfolioProducts;
using System.Collections.Generic;

namespace AppModels.AppModels.Portfolios
{
    public class PortfolioResponse
    {
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
