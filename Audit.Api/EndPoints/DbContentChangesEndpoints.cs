using Audit.Application.Queries;
using MediatR;

namespace Audit.Api.EndPoints;

public static class DbContentChangesEndpoints
{
    public static void MapDbContentChangesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/dbcontentchanges", ListDbContentChangesAsync);
    }

    public static async Task<IResult> ListDbContentChangesAsync(
        IMediator mediator,
        int pageNumber = 1,
        int pageSize = 10)
        => Results.Ok(await mediator.Send(new GetDbContentChangesQuery(pageNumber, pageSize)));
}
