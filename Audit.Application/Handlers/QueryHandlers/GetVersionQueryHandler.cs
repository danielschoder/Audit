using Audit.Application.Queries;
using Audit.Contracts.Responses;
using MediatR;
using System.Reflection;

namespace Audit.Application.Handlers.QueryHandlers;

public class GetVersionQueryHandler() : IRequestHandler<GetVersionQuery, Alive>
{
    public Task<Alive> Handle(GetVersionQuery request, CancellationToken cancellationToken)
        => Task.FromResult(new Alive
        {
            UtcNow = DateTime.UtcNow,
            Version = Assembly.GetEntryAssembly()?.GetName()?.Version?.ToString()
        });
}
