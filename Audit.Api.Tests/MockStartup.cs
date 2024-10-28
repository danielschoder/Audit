using Audit.Api.EndPoints;
using Audit.Application.Queries;
using Audit.Contracts.DTOs;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Audit.Api.Tests;

public class MockStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        var mediatorMock = new Mock<IMediator>();
        mediatorMock.Setup(m => m.Send(It.IsAny<GetDbContentChangesQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new PaginatedResult<DbContentChangeDto> { TotalRecords = 0 });

        services.AddSingleton(mediatorMock.Object);
        services.AddRouting();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDbContentChangesEndpoints();
        });
    }
}
