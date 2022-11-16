using AppServices.Interfaces;
using AppServices.Services;
using DomainServices.Interfaces;
using DomainServices.Services;
using EntityFrameworkCore.UnitOfWork.Extensions;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options => options.SuppressAsyncSuffixInActionNames = false);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ICustomerAppService, CustomerAppService>();
builder.Services.AddTransient<IProductAppService, ProductAppService>();
builder.Services.AddTransient<ICustomerBankInfoAppService, CustomerBankInfoAppService>();
builder.Services.AddTransient<IOrderAppService, OrderAppService>();
builder.Services.AddTransient<IPortfolioProductAppService, PortfolioProductAppService>();
builder.Services.AddTransient<IPortfolioAppService, PortfolioAppService>();
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<ICustomerBankInfoService, CustomerBankInfoService>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IPortfolioProductService, PortfolioProductService>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IPortfolioService, PortfolioService>();
builder.Services.AddDbContext<WarrenContext>(
    options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)), contextLifetime: ServiceLifetime.Transient);
builder.Services.AddUnitOfWork<WarrenContext>(ServiceLifetime.Transient);
builder.Services.AddAutoMapper(Assembly.Load(nameof(AppServices)));
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(Assembly.Load(nameof(AppServices)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
