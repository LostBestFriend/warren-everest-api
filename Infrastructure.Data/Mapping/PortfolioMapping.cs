using DomainModels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Mapping
{
    internal class PortfolioMapping : IEntityTypeConfiguration<Portfolio>
    {
        public void Configure(EntityTypeBuilder<Portfolio> builder)
        {
            builder.ToTable("Portfolios");

            builder.HasKey(portfolio => portfolio.Id);

            builder.Property(potfolio => potfolio.Id)
                   .ValueGeneratedOnAdd()
                   .HasColumnName("Id");

            builder.Property(portfolio => portfolio.Name)
                   .IsRequired()
                   .HasColumnName("Name")
                   .HasColumnType("varchar(50)");

            builder.Property(portfolio => portfolio.Description)
                   .IsRequired()
                   .HasColumnName("Description")
                   .HasColumnType("varchar(200)");

            builder.Property(portfolio => portfolio.TotalBalance)
                    .IsRequired()
                    .HasColumnName("TotalBalance");

            builder.Property(portfolio => portfolio.AccountBalance)
                    .IsRequired()
                    .HasColumnName("AccountBalance");

            builder.Property(portfolio => portfolio.CustomerId)
                    .IsRequired()
                    .HasColumnName("CustomerId");
        }
    }
}
