using AppServices.DTOs;
using AppServices.Interfaces;
using AppServices.Services;
using AppServices.Validation;
using ChallengeFirst;
using Data.Context;
using DomainModels.DataBase;
using DomainServices.Interfaces;
using DomainServices.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddSingleton<ListCustomer>();
        builder.Services.AddSingleton<ICustomerRepository, RepositoryList>();
        builder.Services.AddTransient<ICustomerService, CustomerService>();
        builder.Services.AddScoped<IValidator<CustomerDTO>, CustomersValidator>();
        builder.Services.AddControllers();
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddValidatorsFromAssembly(Assembly.Load(nameof(AppServices)));

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<CustomerContext>(options =>
            options.UseMySql(
                    ServerVersion.AutoDetect("server=localhost;port=3306;database=teste;uid=root;password=132435"),
                    b => b.MigrationsAssembly(typeof(CustomerContext)
                            .Assembly.FullName)));
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = "redis:6379";
            options.InstanceName = "SampleInstance";
        });
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

    }

    public static IHostBuilder CreateHostBuilder(string[] args)
   => Host.CreateDefaultBuilder(args)
       .ConfigureWebHostDefaults(
           webBuilder => webBuilder.UseStartup<Startup>());
}
