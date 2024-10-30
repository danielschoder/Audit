using Audit.Application.Handlers.QueryHandlers;
using MediatR;

namespace Audit.Api.EndPoints;

public static class AliveEndpoints
{
    public static void MapAliveEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", GetVersionAsync).WithOpenApi();
        app.MapGet("/api", GetVersionAsync).WithOpenApi();

        static async Task<IResult> GetVersionAsync(IMediator mediator)
            => Results.Ok(await mediator.Send(new GetVersion.Query()));
    }
}
