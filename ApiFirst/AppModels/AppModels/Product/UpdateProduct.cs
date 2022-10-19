using AppModels.EnumModels;
using System;

namespace AppModels.AppModels.Product
{
    public class UpdateProduct
    {
        public UpdateProduct(string symbol, decimal unitPrice, DateTime issuanceAt, DateTime expirationAt, ProductEnum type)
        {
            Symbol = symbol;
            UnitPrice = unitPrice;
            IssuanceAt = issuanceAt;
            ExpirationAt = expirationAt;
            DaysToExpire = expirationAt.Subtract(issuanceAt).Days;
            Type = type;
        }

        public string Symbol { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime IssuanceAt { get; set; }
        public DateTime ExpirationAt { get; set; }
        public int DaysToExpire { get; set; }
        public ProductEnum Type { get; set; }
    }
}
