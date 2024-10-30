using Audit.Application.Interfaces.Persistence;

namespace Audit.Application.Handlers;

public abstract class HandlerBase(IApplicationDbContext dbContext)
{
    protected readonly IApplicationDbContext _dbContext = dbContext;
}
