﻿using AppModels.EnumModels;
using System;

namespace AppModels.AppModels.Orders
{
    public class OrderResponse
    {
        public OrderResponse(int id, int quotes, int unitPrice, DateTime liquidateAt, OrderEnum direction, int productId, int portfolioId)
        {
            Id = id;
            Quotes = quotes;
            UnitPrice = unitPrice;
            NetValue = quotes * unitPrice;
            LiquidateAt = liquidateAt;
            Direction = direction;
            ProductId = productId;
            PortfolioId = portfolioId;
        }

        public long Id { get; set; }
        public int Quotes { get; set; }
        public decimal NetValue { get; set; }
        public DateTime LiquidateAt { get; set; }
        public OrderDirection Direction { get; set; }
        public long ProductId { get; set; }
        public long PortfolioId { get; set; }
    }
}
