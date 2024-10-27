using Audit.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Audit.Application.Interfaces.Persistence;

public interface IApplicationDbContext
{
    DbSet<EntityFieldContentChange> EntityFieldContentChanges { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
