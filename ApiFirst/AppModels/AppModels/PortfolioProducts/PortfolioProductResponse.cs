using AppModels.AppModels.Products;

namespace AppModels.AppModels.PortfolioProducts
{
    public class PortfolioProductResponse
    {
        public PortfolioProductResponse(ProductResponse product)
        {
            Product = product;
        }

        public PortfolioProductResponse()
        {

        }

        public ProductResponse Product { get; set; }
    }
}
