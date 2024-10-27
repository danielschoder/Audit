using Audit.Application.Queries;
using MediatR;

namespace Audit.Api.EndPoints;

public static class AliveEndpoints
{
    public static void MapAliveEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", GetVersionAsync).WithOpenApi();
        app.MapGet("/api", GetVersionAsync).WithOpenApi();
        //app.MapGet("/error", ThrowError);

        static async Task<IResult> GetVersionAsync(IMediator mediator)
            => Results.Ok(await mediator.Send(new GetVersionQuery()));

        //static IResult ThrowError(IScopedLogService logService)
        //{
        //    logService.Log("Before error");
        //    var zero = 0;
        //    var y = 1 / zero;
        //    logService.Log("After error");
        //    return Results.Ok();
        //}
    }
}
