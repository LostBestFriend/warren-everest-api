using AppServices.Interfaces;
using AppServices.Services;
using AppServices.Validation;
using DomainModels.Models;
using DomainServices.Interfaces;
using DomainServices.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ICustomerServices, CustomerServices>();
builder.Services.AddSingleton<ICustomerAppServices, CustomerAppServices>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<Customer>, CustomersValidator>();

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
