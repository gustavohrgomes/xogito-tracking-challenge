using FluentValidation;
using MediatR.NotificationPublishers;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using Warehouse.Tracking.API.Middlewares;
using Warehouse.Tracking.Application;
using Warehouse.Tracking.Application.Behaviors;
using Warehouse.Tracking.Infrastructure;
using Warehouse.Tracking.Infrastructure.Persistence;

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
    var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    ApplicationDbContextSeeder.SeedAsync(context).Wait();

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