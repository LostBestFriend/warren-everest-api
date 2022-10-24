using DomainModels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Mapping
{
    public class CustomerMapping : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");

            builder.HasKey(customer => customer.Id);

            builder.Property(customer => customer.Id)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasColumnName("Id");

            builder.Property(customer => customer.FullName)
                .IsRequired()
                .HasColumnName("FullName")
                .HasColumnType("varchar(250)");

            builder.Property(customer => customer.Email)
                .IsRequired()
                .HasColumnName("Email")
                .HasColumnType("varchar(60)");

            builder.HasIndex(customer => customer.Email)
                .IsUnique();

            builder.Property(customer => customer.Cpf)
                .IsRequired()
                .HasColumnName("Cpf")
                .HasColumnType("varchar(11)");

            builder.HasIndex(customer => customer.Cpf)
                .IsUnique();

            builder.Property(customer => customer.Cellphone)
                .IsRequired()
                .HasColumnName("Cellphone")
                .HasColumnType("varchar(20)");

            builder.Property(customer => customer.DateOfBirth)
                .IsRequired()
                .HasColumnName("DateOfBirth");

            builder.Property(customer => customer.EmailSms)
                .IsRequired()
                .HasColumnName("EmailSms");

            builder.Property(customer => customer.Whatsapp)
                .IsRequired()
                .HasColumnName("Whatsapp");

            builder.Property(customer => customer.Country)
                .IsRequired()
                .HasColumnName("Country")
                .HasColumnType("varchar(30)");

            builder.Property(customer => customer.City)
                .IsRequired()
                .HasColumnName("City")
                .HasColumnType("varchar(30)");

            builder.Property(customer => customer.PostalCode)
                .IsRequired()
                .HasColumnName("PostalCode")
                .HasColumnType("varchar(11)");

            builder.Property(customer => customer.Address)
                .IsRequired()
                .HasColumnName("Address")
                .HasColumnType("varchar(60)");

            builder.Property(customer => customer.Number)
                .IsRequired()
                .HasColumnName("Number").
                HasColumnType("int");

            builder.HasOne(customer => customer.CustomerBankInfo)
                   .WithOne(customerBankInfo => customerBankInfo.Customer);

            builder.HasMany(customer => customer.Portfolios)
                .WithOne(port => port.Customer)
                .HasForeignKey(cus => cus.CustomerId);
        }
    }
}
