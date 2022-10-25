using DomainModels.Enums;
using System;

namespace DomainModels.Models
{
    public class Order : BaseModel
    {
        public Order(int quotes, decimal unitPrice, DateTime liquidateAt, OrderDirection direction, long productId, long portfolioId)
        {
            Quotes = quotes;
            NetValue = unitPrice * quotes;
            LiquidateAt = liquidateAt;
            Direction = direction;
            ProductId = productId;
            PortfolioId = portfolioId;
        }

        public int Quotes { get; set; }
        public decimal NetValue { get; set; }
        public DateTime LiquidateAt { get; set; }
        public OrderDirection Direction { get; set; }
        public Product Product { get; set; }
        public long ProductId { get; set; }
        public Portfolio Portfolio { get; set; }
        public long PortfolioId { get; set; }
    }
}
