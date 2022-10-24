using DomainModels.Enums;
using System;
using System.Collections.Generic;

namespace DomainModels.Models
{
    public class Product : BaseModel
    {
        public Product(string symbol, DateTime issuanceAt, DateTime expirationAt, ProductType type)
        {
            Symbol = symbol;
            IssuanceAt = issuanceAt;
            ExpirationAt = expirationAt;
            DaysToExpire = (expirationAt.Date - issuanceAt.Date).Days;
            Type = type;
        }
        public string Symbol { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime IssuanceAt { get; set; }
        public DateTime ExpirationAt { get; set; }
        public int DaysToExpire { get; set; }
        public ProductType Type { get; set; }
        public ICollection<PortfolioProduct> PortfolioProducts { get; set; }
        public List<Order> Orders { get; set; }

    }
}
