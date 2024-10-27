using Audit.Application.Queries;
using MediatR;

namespace Audit.Api.EndPoints;

public static class DbContentChangesEndpoints
{
    public static void MapDbContentChangesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/dbcontentchanges", ListDriversAsync);

        static async Task<IResult> ListDriversAsync(
            IMediator mediator,
            int pageNumber = 1,
            int pageSize = 10)
            => Results.Ok(await mediator.Send(new GetDbContentChangesQuery(pageNumber, pageSize)));
    }
}
