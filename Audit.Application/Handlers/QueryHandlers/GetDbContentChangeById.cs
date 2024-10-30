using Audit.Application.Interfaces.Persistence;
using Audit.Contracts.DTOs;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Audit.Application.Handlers.QueryHandlers;

public class GetDbContentChangeById(IApplicationDbContext dbContext)
    : HandlerBase(dbContext), IRequestHandler<GetDbContentChangeById.Query, IResult>
{
    public record Query(Guid Id) : IRequest<IResult>;

    public async Task<IResult> Handle(Query query, CancellationToken cancellationToken)
    {
        var dbContentChange = await _dbContext.DbContentChanges
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id.Equals(query.Id), cancellationToken);
        return Results.Ok(dbContentChange?.Adapt<DbContentChangeDto>()) ?? Results.NotFound();
    }
}
