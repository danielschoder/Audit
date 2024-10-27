using IntegrationEvents;
using MassTransit;

namespace Audit.Infrastructure.Consumers;

public class AuditLogConsumer : IConsumer<AuditLogMessage>
{
    public Task Consume(ConsumeContext<AuditLogMessage> context)
    {
        var message = context.Message;

        // Process the message here

        return Task.CompletedTask;
    }
}
