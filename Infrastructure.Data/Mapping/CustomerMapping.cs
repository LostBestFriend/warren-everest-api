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
                .HasColumnName("id");

            builder.Property(customer => customer.Email)
                .IsRequired()
                .HasColumnName("Email");

            builder.HasIndex(customer => customer.Email)
                .IsUnique();

            builder.Property(customer => customer.Cpf)
                .IsRequired()
                .HasColumnName("Cpf");

            builder.HasIndex(customer => customer.Cpf)
                .IsUnique();

            builder.Property(customer => customer.Cellphone)
                .IsRequired()
                .HasColumnName("Cellphone");

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
                .HasColumnName("Country");

            builder.Property(customer => customer.City)
                .IsRequired()
                .HasColumnName("City");

            builder.Property(customer => customer.PostalCode)
                .IsRequired()
                .HasColumnName("PostalCode");

            builder.Property(customer => customer.Address)
                .IsRequired()
                .HasColumnName("Address");

            builder.Property(customer => customer.Number)
                .IsRequired()
                .HasColumnName("number");
        }
    }
}
