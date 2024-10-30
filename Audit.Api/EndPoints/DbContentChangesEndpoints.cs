using Audit.Application.Handlers.CommandHandlers;
using Audit.Application.Handlers.QueryHandlers;
using IntegrationEvents;
using MediatR;

namespace Audit.Api.EndPoints;

public static class DbContentChangesEndpoints
{
    public static void MapDbContentChangesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/dbcontentchanges", ListDbContentChangesAsync).WithOpenApi();
        app.MapGet("/api/dbcontentchanges/{id:guid}", GetDbContentChangeAsync).WithOpenApi();
        app.MapPost("/api/dbcontentchanges", CreateDbContentChangeAsync).WithOpenApi();
    }

    public static async Task<IResult> ListDbContentChangesAsync(
        IMediator mediator,
        int pageNumber = 1,
        int pageSize = 10)
        => Results.Ok(await mediator.Send(new GetDbContentChanges.Query(pageNumber, pageSize)));

    public static async Task<IResult> GetDbContentChangeAsync(
        IMediator mediator,
        Guid id)
        => await mediator.Send(new GetDbContentChangeById.Query(id));

    public static async Task<IResult> CreateDbContentChangeAsync(
        IMediator mediator,
        AuditLogMessage auditLogMessage)
    {
        await mediator.Send(new InsertDbContentChange.Command(auditLogMessage));
        return Results.Ok();
    }
}
