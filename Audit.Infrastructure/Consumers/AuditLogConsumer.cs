﻿using Audit.Application.Handlers.CommandHandlers;
using IntegrationEvents;
using MassTransit;
using MediatR;

namespace Audit.Infrastructure.Consumers;

public class AuditLogConsumer(IMediator mediator) : IConsumer<AuditLogMessage>
{
    private readonly IMediator _mediator = mediator;

    public async Task Consume(ConsumeContext<AuditLogMessage> context)
    {
        var message = context.Message;
        await _mediator.Send(new InsertDbContentChange.Command(message));
        await context.ConsumeCompleted;
    }
}
