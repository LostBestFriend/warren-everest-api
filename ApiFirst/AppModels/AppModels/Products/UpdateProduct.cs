using AppModels.EnumModels;
using System;

namespace AppModels.AppModels.Products
{
    public class UpdateProduct
    {
        public UpdateProduct(string symbol, decimal unitPrice, DateTime issuanceAt, DateTime expirationAt, ProductType type)
        {
            Symbol = symbol;
            IssuanceAt = issuanceAt;
            ExpirationAt = expirationAt;
            UnitPrice = unitPrice;
            DaysToExpire = expirationAt.Subtract(issuanceAt).Days;
            Type = type;
        }

        public UpdateProduct()
        {

        }

        public string Symbol { get; set; }
        public DateTime IssuanceAt { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime ExpirationAt { get; set; }
        public int DaysToExpire { get; set; }
        public ProductType Type { get; set; }
    }
}
