using AppModels.EnumModels;
using System;

namespace AppModels.AppModels.Orders
{
    public class UpdateOrder
    {
        public UpdateOrder(int quotes, decimal unitPrice, DateTime liquidateAt, OrderDirection direction, long productId, long portfolioId)
        {
            Quotes = quotes;
            LiquidateAt = liquidateAt;
            NetValue = quotes * unitPrice;
            Direction = direction;
            ProductId = productId;
            PortfolioId = portfolioId;
        }

        public UpdateOrder()
        { }

        public int Quotes { get; set; }
        public decimal NetValue { get; set; }
        public DateTime LiquidateAt { get; set; }
        public OrderDirection Direction { get; set; }
        public long ProductId { get; set; }
        public long PortfolioId { get; set; }
    }
}
