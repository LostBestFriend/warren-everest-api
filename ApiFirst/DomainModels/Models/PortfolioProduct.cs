namespace DomainModels.Models
{
    public class PortfolioProduct : BaseModel
    {
        public PortfolioProduct(long portfolioId, long productId)
        {
            PortfolioId = portfolioId;
            ProductId = productId;
        }

        public PortfolioProduct()
        {

        }

        public Portfolio Portfolio { get; set; }
        public long PortfolioId { get; set; }
        public Product Product { get; set; }
        public long ProductId { get; set; }

    }
}
