using DomainModels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Mapping
{
    internal class PortfolioProductMapping : IEntityTypeConfiguration<PortfolioProduct>
    {
        public void Configure(EntityTypeBuilder<PortfolioProduct> builder)
        {
            builder.ToTable("PortfolioProducts");

            builder.HasKey(portfolioproduct => portfolioproduct.Id);

            builder.Property(portfolioproduct => portfolioproduct.Id)
                   .ValueGeneratedOnAdd()
                   .HasColumnName("Id");

            builder.HasOne(portfolioproduct => portfolioproduct.Product)
                .WithMany(product => product.PortfolioProducts)
                .HasForeignKey(portfolioproduct => portfolioproduct.ProductId);

            builder.HasOne(portfolioproduct => portfolioproduct.Portfolio)
                .WithMany(portfolio => portfolio.PortfolioProducts)
                .HasForeignKey(portfolioproduct => portfolioproduct.PortfolioId);
        }
    }
}
