using AppServices.Dependencies;
using DomainServices.Dependencies;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Data.Dependencies;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options => options.SuppressAsyncSuffixInActionNames = false);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
AppServicesExtensions.AddAppServicesDependencies(builder.Services);
DomainServicesExtensions.AddDomainServiceDependencies(builder.Services);
InfrastructureDataExtensions.AddInfrastructureDataDependencies(builder.Services, builder.Configuration);
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
