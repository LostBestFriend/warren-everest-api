using AppModels.EnumModels;
using System;

namespace AppModels.AppModels.Orders
{
    public class CreateOrder
    {
        public CreateOrder(int quotes, decimal unitPrice, DateTime liquidatedAt, OrderDirection direction, long productId, long portfolioId)
        {
            Quotes = quotes;
            NetValue = unitPrice * quotes;
            LiquidatedAt = liquidatedAt;
            Direction = direction;
            ProductId = productId;
            PortfolioId = portfolioId;
        }

        public int Quotes { get; set; }
        public decimal NetValue { get; set; }
        public DateTime LiquidatedAt { get; set; }
        public OrderDirection Direction { get; set; }
        public long ProductId { get; set; }
        public long PortfolioId { get; set; }

    }
}
