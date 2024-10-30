using Audit.Application.Interfaces.Persistence;
using Audit.Contracts.DTOs;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Audit.Application.Handlers.QueryHandlers;

public class GetDbContentChanges(IApplicationDbContext dbContext)
    : HandlerBase(dbContext), IRequestHandler<GetDbContentChanges.Query, PaginatedResult<DbContentChangeDto>>
{
    public record Query(int PageNumber, int PageSize) : IRequest<PaginatedResult<DbContentChangeDto>>;

    public async Task<PaginatedResult<DbContentChangeDto>> Handle(Query query, CancellationToken cancellationToken)
    {
        var skip = (query.PageNumber - 1) * query.PageSize;
        var dbContentChangesQuery = _dbContext.DbContentChanges.AsNoTracking();
        var result = await dbContentChangesQuery
            .Select(e => new
            {
                TotalCount = dbContentChangesQuery.Count(),
                Records = dbContentChangesQuery
                    .OrderByDescending(e => e.ChangedDateTime)
                    .Skip(skip)
                    .Take(query.PageSize)
                    .ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

        return new PaginatedResult<DbContentChangeDto>(default, default, default)
        {
            Data = result?.Records.Adapt<List<DbContentChangeDto>>() ?? [],
            CurrentPage = query.PageNumber,
            PageSize = query.PageSize,
            TotalRecords = result?.TotalCount ?? 0
        };
    }
}
