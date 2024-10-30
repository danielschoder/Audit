using Audit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Audit.Application.Interfaces.Persistence;

public interface IApplicationDbContext
{
    DbSet<DbContentChange> DbContentChanges { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    public DatabaseFacade Database { get; }
}
