using FluentValidation;
using MediatR;
using MediatR.NotificationPublishers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using RetailSystem.API.Middlewares;
using RetailSystem.Application;
using RetailSystem.Application.Behaviors;
using RetailSystem.Infrastructure;
using RetailSystem.Infrastructure.Persistence;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseSerilog((context, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(context.Configuration));

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
    .AddInfrastructureServices()
    .AddMediatR(config =>
    {
        config.RegisterServicesFromAssemblyContaining<ApplicationAssemblyMarker>();
        config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        config.NotificationPublisher = new TaskWhenAllPublisher();
        config.NotificationPublisherType = typeof(TaskWhenAllPublisher);
    })
    .AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
    .AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<RequestLogContextMiddleware>();
app.UseSerilogRequestLogging();

app.UseAuthorization();

app.MapControllers();

app.Run();
