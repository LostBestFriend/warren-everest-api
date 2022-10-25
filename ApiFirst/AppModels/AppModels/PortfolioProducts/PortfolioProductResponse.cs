using AppModels.AppModels.Products;

namespace AppModels.AppModels.PortfolioProducts
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
