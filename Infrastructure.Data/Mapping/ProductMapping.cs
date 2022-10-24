using DomainModels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Mapping
{
    internal class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(product => product.Id);

            builder.Property(product => product.Id)
                   .ValueGeneratedOnAdd()
                   .HasColumnName("Id");

            builder.HasIndex(product => product.Symbol);

            builder.HasIndex(product => product.Type);

            builder.Property(product => product.Symbol)
                    .IsRequired()
                    .HasColumnName("Symbol")
                    .HasColumnType("varchar(15)");

            builder.Property(product => product.UnitPrice)
                    .IsRequired()
                    .HasColumnName("UnitPrice");

            builder.Property(product => product.IssuanceAt)
                    .IsRequired()
                    .HasColumnName("IssuanceAt");

            builder.Property(product => product.ExpirationAt)
                    .IsRequired()
                    .HasColumnName("ExpirationAt");

            builder.Property(product => product.DaysToExpire)
                    .IsRequired()
                    .HasColumnName("DaysToExpire");

            builder.Property(product => product.Type)
                    .IsRequired()
                    .HasColumnName("Type");

            builder.HasMany(product => product.Orders)
                .WithOne(cus => cus.Product)
                .HasForeignKey(c => c.ProductId);
        }
    }
}
