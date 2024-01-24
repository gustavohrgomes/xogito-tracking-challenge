using FluentValidation;
using MediatR;
using MediatR.NotificationPublishers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using RetailSystem.API.Middlewares;
using RetailSystem.Application;
using RetailSystem.Application.Behaviors;
using RetailSystem.Infrastructure;
using RetailSystem.Infrastructure.Persistence;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseSerilog((context, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(context.Configuration));

var assemblies = new Assembly[] { typeof(Program).Assembly, typeof(ApplicationAssemblyMarker).Assembly, typeof(InfrastructureAssemblyMarker).Assembly };

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Retail API", Version = "v1" });
        c.EnableAnnotations();
    })
    .AddSqlServerDbContext<ApplicationDbContext>(
        builder.Configuration,
        builder.Configuration.GetConnectionString("RetailDB"),
        ServiceLifetime.Scoped)
    .AddMediatR(config =>
    {
        config.RegisterServicesFromAssemblies(assemblies);
        config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        config.NotificationPublisherType = typeof(TaskWhenAllPublisher);
    })
    .AddUnitOfWork<ApplicationDbContext>()
    .AddInfrastructureServices()
    .AddValidatorsFromAssemblies(assemblies)
    .AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseInfrastructureServices();

app.UseMiddleware<RequestLogContextMiddleware>();
app.UseSerilogRequestLogging();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }