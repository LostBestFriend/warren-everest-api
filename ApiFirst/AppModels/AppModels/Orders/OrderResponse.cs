using AppModels.EnumModels;
using System;

namespace AppModels.AppModels.Orders
{
    public class OrderResponse
    {
        public long Id { get; set; }
        public int Quotes { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal NetValue { get; set; }
        public DateTime LiquidateAt { get; set; }
        public OrderDirection Direction { get; set; }
        public long ProductId { get; set; }
        public long PortfolioId { get; set; }
    }
}
