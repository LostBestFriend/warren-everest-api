using System;

namespace DomainModels.Models
{
    internal class Order : BaseModel
    {
        public Order(int quotes, decimal unitPrice, decimal netValue, DateTime liquidateAt, OrderEnum direction, long productId, long portfolioId)
        {
            Quotes = quotes;
            UnitPrice = unitPrice;
            NetValue = netValue;
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
        public Product Product { get; set; }
        public long ProductId { get; set; }
        public Portfolio Portfolio { get; set; }
        public long PortfolioId { get; set; }
    }
}
