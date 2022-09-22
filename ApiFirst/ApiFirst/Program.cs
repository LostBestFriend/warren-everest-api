using AppModels.MapperModels;
using AppServices.Interfaces;
using AppServices.Services;
using AppServices.Validation;
using AppServices.Validator;
using DomainServices.Interfaces;
using DomainServices.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ICustomerService, CustomerService>();
builder.Services.AddTransient<ICustomerAppService, CustomerAppServices>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<CustomerCreateDTO>, CustomerCreateDTOValidator>();
builder.Services.AddScoped<IValidator<CustomerUpdateDTO>, CustomerUpdateDTOValidator>();
builder.Services.AddAutoMapper(Assembly.Load("AppServices"));

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
