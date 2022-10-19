using AppModels.AppModels.Product;

namespace AppModels.AppModels.PortfolioProduct
{
    public class PortfolioProductResponse
    {
        public PortfolioProductResponse(ProductResponse product)
        {
            Product = product;
        }

        public ProductResponse Product { get; set; }
    }
}
