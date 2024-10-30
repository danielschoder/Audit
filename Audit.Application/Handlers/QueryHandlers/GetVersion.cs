using Audit.Contracts.Responses;
using MediatR;
using System.Reflection;

namespace Audit.Application.Handlers.QueryHandlers;

public class GetVersion() : IRequestHandler<GetVersion.Query, Alive>
{
    public record Query : IRequest<Alive>;

    public Task<Alive> Handle(Query request, CancellationToken cancellationToken)
        => Task.FromResult(new Alive(default, null)
        {
            UtcNow = DateTime.UtcNow,
            Version = Assembly.GetEntryAssembly()?.GetName()?.Version?.ToString()
        });
}
