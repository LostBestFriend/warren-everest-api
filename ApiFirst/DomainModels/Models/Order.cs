using DomainModels.Enums;
using System;

namespace DomainModels.Models
{
    public class Order : BaseModel
    {
        public Order(int quotes, decimal unitPrice, DateTime liquidatedAt, OrderDirection direction, long productId, long portfolioId)
        {
            Quotes = quotes;
            NetValue = unitPrice * quotes;
            LiquidatedAt = liquidatedAt;
            Direction = direction;
            ProductId = productId;
            PortfolioId = portfolioId;
        }

        public Order()
        { }

        public int Quotes { get; set; }
        public decimal NetValue { get; set; }
        public DateTime LiquidatedAt { get; set; }
        public OrderDirection Direction { get; set; }
        public Product Product { get; set; }
        public long ProductId { get; set; }
        public Portfolio Portfolio { get; set; }
        public long PortfolioId { get; set; }
    }
}
