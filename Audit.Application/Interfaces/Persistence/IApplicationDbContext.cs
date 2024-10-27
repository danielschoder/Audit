using Audit.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Audit.Application.Interfaces.Persistence;

public interface IApplicationDbContext
{
    DbSet<DbContentChange> DbContentChanges { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
