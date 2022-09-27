using AppServices.Interfaces;
using AppServices.Services;
using DomainServices.Interfaces;
using DomainServices.Services;
using EntityFrameworkCore.UnitOfWork.Extensions;
using FluentValidation;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<WarrenContext>(
    options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<ICustomerAppService, CustomerAppService>();
builder.Services.AddAutoMapper(Assembly.Load("AppServices"));
builder.Services.AddValidatorsFromAssembly(Assembly.Load(nameof(AppServices)));
builder.Services.AddUnitOfWork<WarrenContext>();

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
