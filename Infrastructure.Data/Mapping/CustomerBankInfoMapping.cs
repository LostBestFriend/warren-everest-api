using DomainModels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Mapping
{
    internal class CustomerBankInfoMapping : IEntityTypeConfiguration<CustomerBankInfo>
    {
        public void Configure(EntityTypeBuilder<CustomerBankInfo> builder)
        {
            builder.ToTable("CustomerBankInfos");

            builder.HasKey(bankInfo => bankInfo.Id);

            builder.Property(bankInfo => bankInfo.Id)
                   .ValueGeneratedOnAdd()
                   .HasColumnName("Id");

            builder.Property(bankinfo => bankinfo.AccountBalance)
                   .IsRequired()
                   .HasColumnName("AccountBalance");

            builder.Property(bankinfo => bankinfo.CustomerId)
                   .IsRequired()
                   .HasColumnName("CustomerId");
        }
    }
}
