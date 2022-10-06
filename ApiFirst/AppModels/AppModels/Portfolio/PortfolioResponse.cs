using AppModels.AppModels.Customer;
using AppModels.AppModels.Order;
using AppModels.AppModels.PortfolioProduct;
using System.Collections.Generic;

namespace AppModels.AppModels.Portfolio
{
    public class PortfolioResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal TotalBalance { get; set; }
        public decimal AccountBalance { get; set; }
        public CustomerResponse Customer { get; set; }
        public IEnumerable<PortfolioProductResult> Products { get; set; }
        public IEnumerable<OrderResponse> Orders { get; set; }
    }
}
