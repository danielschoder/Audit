using Audit.Application.Interfaces.Persistence;
using Audit.Domain.Entities;
using IntegrationEvents;
using Mapster;
using MediatR;

namespace Audit.Application.Handlers.CommandHandlers;

public class InsertDbContentChange(IApplicationDbContext context)
    : IRequestHandler<InsertDbContentChange.Command>
{
    private readonly IApplicationDbContext _context = context;

    public record Command(AuditLogMessage AuditLogMessage) : IRequest;

    public async Task Handle(Command request, CancellationToken cancellationToken)
    {
        var entityFieldContentChange = request.AuditLogMessage.Adapt<DbContentChange>();
        entityFieldContentChange.Id = Guid.NewGuid();
        await _context.DbContentChanges.AddAsync(entityFieldContentChange, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
