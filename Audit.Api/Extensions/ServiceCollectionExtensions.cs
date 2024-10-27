using Audit.Application.Interfaces.Persistence;
using Audit.Infrastructure.Consumers;
using Audit.Infrastructure.Persistence;
using MassTransit;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Audit.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static void ConfigureJsonOptions(this IServiceCollection services)
    {
        services.Configure<JsonOptions>(options =>
        {
            options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });
    }

    public static void AddApplicationDbContext(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
            sqlServerOptionsAction: sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).GetTypeInfo().Assembly.GetName().Name);
                sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
            }));
#pragma warning disable CS8603 // Possible null reference return.
        services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
#pragma warning restore CS8603 // Possible null reference return.

        using var serviceProvider = services.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.Migrate();
    }

    public static void AddRabbitMqMassTransit(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<AuditLogConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration["EventBusConnection"] ?? "localhost", "/", h =>
                {
                    h.Username(configuration["EventBusUserName"] ?? "guest");
                    h.Password(configuration["EventBusPassword"] ?? "guest");
                });

                cfg.ConfigureEndpoints(context);
            });
        });

        services.AddMassTransitHostedService();
    }

    public static void AddCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins",
                builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
        });
    }
}
