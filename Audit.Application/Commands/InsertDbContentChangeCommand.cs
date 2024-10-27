using IntegrationEvents;
using MediatR;

namespace Audit.Application.Commands;

public class InsertDbContentChangeCommand(AuditLogMessage auditLogMessage) : IRequest
{
    public AuditLogMessage AuditLogMessage { get; set; } = auditLogMessage;
}
