﻿using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    [DbContext(typeof(WarrenContext))]
    partial class WarrenEverestContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DomainModels.Models.Customer", b =>
            {
                b.Property<long>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("bigint")
                    .HasColumnName("Id");

                b.Property<string>("Address")
                    .IsRequired()
                    .HasColumnType("varchar(60)")
                    .HasColumnName("Address");

                b.Property<string>("Cellphone")
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasColumnName("Cellphone");

                b.Property<string>("City")
                    .IsRequired()
                    .HasColumnType("varchar(30)")
                    .HasColumnName("City");

                b.Property<string>("Country")
                    .IsRequired()
                    .HasColumnType("varchar(30)")
                    .HasColumnName("Country");

                b.Property<string>("Cpf")
                    .IsRequired()
                    .HasColumnType("varchar(11)")
                    .HasColumnName("Cpf");

                b.Property<DateTime>("DateOfBirth")
                    .HasColumnType("datetime(6)")
                    .HasColumnName("DateOfBirth");

                b.Property<string>("Email")
                    .IsRequired()
                    .HasColumnType("varchar(60)")
                    .HasColumnName("Email");

                b.Property<bool>("EmailSms")
                    .HasColumnType("tinyint(1)")
                    .HasColumnName("EmailSms");

                b.Property<string>("FullName")
                    .IsRequired()
                    .HasColumnType("varchar(250)")
                    .HasColumnName("FullName");

                b.Property<int>("Number")
                    .HasColumnType("int")
                    .HasColumnName("number");

                b.Property<string>("PostalCode")
                    .IsRequired()
                    .HasColumnType("varchar(11)")
                    .HasColumnName("PostalCode");

                b.Property<bool>("Whatsapp")
                    .HasColumnType("tinyint(1)")
                    .HasColumnName("Whatsapp");

                b.HasKey("Id");

                b.HasIndex("Cpf")
                    .IsUnique();

                b.HasIndex("Email")
                    .IsUnique();

                b.ToTable("Customers", (string)null);
            });

            modelBuilder.Entity("DomainModels.Models.CustomerBankInfo", b =>
            {
                b.Property<long>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("bigint")
                    .HasColumnName("Id");

                b.Property<decimal>("AccountBalance")
                    .HasColumnType("decimal(65,30)")
                    .HasColumnName("AccountBalance");

                b.Property<long>("CustomerId")
                    .HasColumnType("bigint")
                    .HasColumnName("CustomerId");

                b.HasKey("Id");

                b.HasIndex("CustomerId");

                b.ToTable("CustomerBankInfos", (string)null);
            });

            modelBuilder.Entity("DomainModels.Models.Order", b =>
            {
                b.Property<long>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("bigint")
                    .HasColumnName("Id");

                b.Property<int>("Direction")
                    .HasColumnType("int")
                    .HasColumnName("Direction");

                b.Property<DateTime>("LiquidateAt")
                    .HasColumnType("datetime(6)")
                    .HasColumnName("LiquidateAt");

                b.Property<decimal>("NetValue")
                    .HasColumnType("decimal(65,30)")
                    .HasColumnName("NetValue");

                b.Property<long>("PortfolioId")
                    .HasColumnType("bigint")
                    .HasColumnName("PorfolioId");

                b.Property<long>("ProductId")
                    .HasColumnType("bigint")
                    .HasColumnName("ProductId");

                b.Property<int>("Quotes")
                    .HasColumnType("int")
                    .HasColumnName("Quotes");

                b.Property<decimal>("UnitPrice")
                    .HasColumnType("decimal(65,30)")
                    .HasColumnName("UnitPrice");

                b.HasKey("Id");

                b.HasIndex("LiquidateAt");

                b.HasIndex("PortfolioId");

                b.HasIndex("ProductId");

                b.ToTable("Orders", (string)null);
            });

            modelBuilder.Entity("DomainModels.Models.Portfolio", b =>
            {
                b.Property<long>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("bigint")
                    .HasColumnName("Id");

                b.Property<decimal>("AccountBalance")
                    .HasColumnType("decimal(65,30)")
                    .HasColumnName("AccountBalance");

                b.Property<long>("CustomerId")
                    .HasColumnType("bigint")
                    .HasColumnName("CustomerId");

                b.Property<string>("Description")
                    .IsRequired()
                    .HasColumnType("varchar(200)")
                    .HasColumnName("Description");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("Name");

                b.Property<decimal>("TotalBalance")
                    .HasColumnType("decimal(65,30)")
                    .HasColumnName("TotalBalance");

                b.HasKey("Id");

                b.HasIndex("CustomerId");

                b.ToTable("Portfolios", (string)null);
            });

            modelBuilder.Entity("DomainModels.Models.PortfolioProduct", b =>
            {
                b.Property<long>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("bigint")
                    .HasColumnName("Id");

                b.Property<long>("PortfolioId")
                    .HasColumnType("bigint");

                b.Property<long>("ProductId")
                    .HasColumnType("bigint");

                b.HasKey("Id");

                b.HasIndex("PortfolioId");

                b.HasIndex("ProductId");

                b.ToTable("PortfolioProducts", (string)null);
            });

            modelBuilder.Entity("DomainModels.Models.Product", b =>
            {
                b.Property<long>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("bigint")
                    .HasColumnName("Id");

                b.Property<int>("DaysToExpire")
                    .HasColumnType("int")
                    .HasColumnName("DaysToExpire");

                b.Property<DateTime>("ExpirationAt")
                    .HasColumnType("datetime(6)")
                    .HasColumnName("ExpirationAt");

                b.Property<DateTime>("IssuanceAt")
                    .HasColumnType("datetime(6)")
                    .HasColumnName("IssuanceAt");

                b.Property<long?>("PortfolioId")
                    .HasColumnType("bigint");

                b.Property<string>("Symbol")
                    .IsRequired()
                    .HasColumnType("varchar(15)")
                    .HasColumnName("Symbol");

                b.Property<int>("Type")
                    .HasColumnType("int")
                    .HasColumnName("Type");

                b.Property<decimal>("UnitPrice")
                    .HasColumnType("decimal(65,30)")
                    .HasColumnName("UnitPrice");

                b.HasKey("Id");

                b.HasIndex("PortfolioId");

                b.HasIndex("Symbol");

                b.HasIndex("Type");

                b.ToTable("Products", (string)null);
            });

            modelBuilder.Entity("DomainModels.Models.CustomerBankInfo", b =>
            {
                b.HasOne("DomainModels.Models.Customer", "Customer")
                    .WithMany()
                    .HasForeignKey("CustomerId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Customer");
            });

            modelBuilder.Entity("DomainModels.Models.Order", b =>
            {
                b.HasOne("DomainModels.Models.Portfolio", "Portfolio")
                    .WithMany("Orders")
                    .HasForeignKey("PortfolioId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("DomainModels.Models.Product", "Product")
                    .WithMany()
                    .HasForeignKey("ProductId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Portfolio");

                b.Navigation("Product");
            });

            modelBuilder.Entity("DomainModels.Models.Portfolio", b =>
            {
                b.HasOne("DomainModels.Models.Customer", "Customer")
                    .WithMany()
                    .HasForeignKey("CustomerId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Customer");
            });

            modelBuilder.Entity("DomainModels.Models.PortfolioProduct", b =>
            {
                b.HasOne("DomainModels.Models.Portfolio", "Portfolio")
                    .WithMany("PortfolioProducts")
                    .HasForeignKey("PortfolioId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("DomainModels.Models.Product", "Product")
                    .WithMany("PortfolioProducts")
                    .HasForeignKey("ProductId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Portfolio");

                b.Navigation("Product");
            });

            modelBuilder.Entity("DomainModels.Models.Product", b =>
            {
                b.HasOne("DomainModels.Models.Portfolio", null)
                    .WithMany("Products")
                    .HasForeignKey("PortfolioId");
            });

            modelBuilder.Entity("DomainModels.Models.Portfolio", b =>
            {
                b.Navigation("Orders");

                b.Navigation("PortfolioProducts");

                b.Navigation("Products");
            });

            modelBuilder.Entity("DomainModels.Models.Product", b =>
            {
                b.Navigation("PortfolioProducts");
            });
#pragma warning restore 612, 618
        }
    }
}