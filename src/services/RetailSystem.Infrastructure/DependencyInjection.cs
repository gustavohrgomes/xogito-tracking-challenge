using EFCoreSecondLevelCacheInterceptor;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RetailSystem.Infrastructure.ExpectionHandler;
using RetailSystem.Infrastructure.Persistence;
using RetailSystem.Infrastructure.Persistence.Adpaters.EntityFrameworkCore;
using System.Reflection;

namespace RetailSystem.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services
            .AddExceptionHandler<GlobalExceptionHandler>()
            .AddProblemDetails();

        return services;
    }

    public static WebApplication UseInfrastructureServices(this WebApplication app)
    {
        app.UseExceptionHandler();

        return app;
    }

    public static IServiceCollection AddSqlServerDbContext<TDbContext>(
        this IServiceCollection services, 
        IConfiguration configuration,
        string? connectionString,
        ServiceLifetime? serviceLifetime) where TDbContext : DbContext
    {
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException($"Connection string for {nameof(TDbContext)} was not found.");
        }

        var maxRetryCount = configuration.GetValue<int>("SqlServer:MaxRetryCount");
        var enableSecondLevelCache = configuration.GetValue<bool>("SqlServer:EnableSecondLevelCache");

        if (enableSecondLevelCache)
        {
            services.AddEFSecondLevelCache(options =>
                options.UseMemoryCacheProvider().DisableLogging(value: true)
            );
        }

        services.AddDbContext<TDbContext>(sqlServerBuilder =>
        {
            sqlServerBuilder.UseSqlServer(connectionString, options =>
            {
                options.EnableRetryOnFailure(maxRetryCount);
            });
        }, serviceLifetime ?? ServiceLifetime.Scoped);

        return services;
    }

    public static IServiceCollection AddUnitOfWork<TDbContext>(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        where TDbContext : DbContext
    {
        services.TryAdd(new ServiceDescriptor(typeof(IUnitOfWork), typeof(UnitOfWork<TDbContext>), serviceLifetime));

        return services;
    }
}
