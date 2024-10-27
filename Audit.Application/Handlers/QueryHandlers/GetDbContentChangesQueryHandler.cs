using Audit.Application.Interfaces.Persistence;
using Audit.Application.Queries;
using Audit.Contracts.DTOs;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Audit.Application.Handlers.QueryHandlers;

public class GetDbContentChangesQueryHandler(IApplicationDbContext dbContext)
    : HandlerBase(dbContext), IRequestHandler<GetDbContentChangesQuery, PaginatedResult<DbContentChangeDto>>
{
    public async Task<PaginatedResult<DbContentChangeDto>> Handle(GetDbContentChangesQuery request, CancellationToken cancellationToken)
    {
        var skip = (request.PageNumber - 1) * request.PageSize;
        var query = _dbContext.DbContentChanges.AsNoTracking();
        var totalRecords = await query.CountAsync(cancellationToken);
        var dbContentChanges = await _dbContext.DbContentChanges
            .AsNoTracking()
            .OrderByDescending(e => e.ChangedDateTime)
            .Skip(skip)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<DbContentChangeDto>
        {
            Data = dbContentChanges.Adapt<List<DbContentChangeDto>>(),
            CurrentPage = request.PageNumber,
            PageSize = request.PageSize,
            TotalRecords = totalRecords
        };
    }
}
