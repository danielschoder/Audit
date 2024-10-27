using Audit.Infrastructure.Consumers;
using MassTransit;

namespace Audit.Api.Extensions;

public static class ServiceCollectionExtensions
{
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
}
