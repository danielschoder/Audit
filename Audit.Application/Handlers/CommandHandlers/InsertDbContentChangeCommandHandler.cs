using Audit.Application.Commands;
using Audit.Application.Interfaces.Persistence;
using Audit.Domain.Entities;
using Mapster;
using MediatR;

namespace Audit.Application.Handlers.CommandHandlers;

public class InsertDbContentChangeCommandHandler(IApplicationDbContext context)
    : IRequestHandler<InsertDbContentChangeCommand>
{
    private readonly IApplicationDbContext _context = context;

    public async Task Handle(InsertDbContentChangeCommand request, CancellationToken cancellationToken)
    {
        var entityFieldContentChange = request.AuditLogMessage.Adapt<EntityFieldContentChange>();
        entityFieldContentChange.Id = Guid.NewGuid();
        await _context.EntityFieldContentChanges.AddAsync(entityFieldContentChange, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
