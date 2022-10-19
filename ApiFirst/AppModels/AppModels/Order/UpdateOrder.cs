using AppModels.EnumModels;
using System;

namespace AppModels.AppModels.Order
{
    public class UpdateOrder
    {

        public UpdateOrder(int quotes, int unitPrice, DateTime liquidateAt, OrderEnum direction, int productId, int portfolioId)
        {
            Quotes = quotes;
            UnitPrice = unitPrice;
            LiquidateAt = liquidateAt;
            NetValue = quotes * unitPrice;
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
