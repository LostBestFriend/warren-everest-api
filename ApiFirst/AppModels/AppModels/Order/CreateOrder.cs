using AppModels.EnumModels;
using System;

namespace AppModels.AppModels.Order
{
    internal class CreateOrder
    {
        public CreateOrder(int quotes, decimal unitPrice, DateTime liquidateAt, OrderEnum direction, long productId, long portfolioId)
        {
            Quotes = quotes;
            UnitPrice = unitPrice;
            NetValue = quotes * unitPrice;
            LiquidateAt = liquidateAt;
            Direction = direction;
            ProductId = productId;
            PortfolioId = portfolioId;
        }
        public int Quotes { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal NetValue { get; set; }
        public DateTime LiquidateAt { get; set; }
        public OrderEnum Direction { get; set; }
        public long ProductId { get; set; }
        public long PortfolioId { get; set; }

    }
}
