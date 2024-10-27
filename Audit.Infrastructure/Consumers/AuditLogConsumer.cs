using IntegrationEvents;
using MassTransit;

namespace Audit.Infrastructure.Consumers;

public class AuditLogConsumer : IConsumer<AuditLogMessage>
{
    public async Task Consume(ConsumeContext<AuditLogMessage> context)
    {
        var message = context.Message;

        // Process the message here
        await context.ConsumeCompleted;
    }
}
