using AppModels.EnumModels;
using System;

namespace AppModels.AppModels.Product
{
    public class CreateProduct
    {
        public string Symbol { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime IssuanceAt { get; set; }
        public DateTime ExpirationAt { get; set; }
        public ProductEnum Type { get; set; }
    }
}
