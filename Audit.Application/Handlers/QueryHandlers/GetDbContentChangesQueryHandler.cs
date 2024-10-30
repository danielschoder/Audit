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
        var dbContentChangesQuery = _dbContext.DbContentChanges.AsNoTracking();
        var result = await dbContentChangesQuery
            .Select(e => new
            {
                TotalCount = dbContentChangesQuery.Count(),
                Records = dbContentChangesQuery
                    .OrderByDescending(e => e.ChangedDateTime)
                    .Skip(skip)
                    .Take(request.PageSize)
                    .ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

        return new PaginatedResult<DbContentChangeDto>
        {
            Data = result?.Records.Adapt<List<DbContentChangeDto>>() ?? [],
            CurrentPage = request.PageNumber,
            PageSize = request.PageSize,
            TotalRecords = result?.TotalCount ?? 0
        };
    }
}
