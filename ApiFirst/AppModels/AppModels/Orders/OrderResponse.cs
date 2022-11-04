using AppModels.EnumModels;
using System;

namespace AppModels.AppModels.Orders
{
    public class OrderResponse
    {
        public OrderResponse(int id, int quotes, int unitPrice, DateTime liquidatedAt, OrderDirection direction, long productId, long portfolioId)
        {
            Id = id;
            Quotes = quotes;
            NetValue = quotes * unitPrice;
            LiquidatedAt = liquidatedAt;
            Direction = direction;
            ProductId = productId;
            PortfolioId = portfolioId;
        }

        public OrderResponse()
        {

        }

        public long Id { get; set; }
        public int Quotes { get; set; }
        public decimal NetValue { get; set; }
        public DateTime LiquidatedAt { get; set; }
        public OrderDirection Direction { get; set; }
        public long ProductId { get; set; }
        public long PortfolioId { get; set; }
    }
}
