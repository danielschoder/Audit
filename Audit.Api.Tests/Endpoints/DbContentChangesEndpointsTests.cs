using Audit.Api.EndPoints;
using Audit.Application.Queries;
using Audit.Contracts.DTOs;
using MediatR;
using Moq;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Audit.Api.Tests.Endpoints;

[TestFixture]
public class DbContentChangesEndpointsTests
{
    private Mock<IMediator> _mediatorMock;

    [SetUp]
    public void SetUp()
    {
        _mediatorMock = new Mock<IMediator>();
    }

    [Test]
    public async Task ListDriversAsync_ShouldReturnOkResult_WithExpectedData()
    {
        // Arrange
        int pageNumber = 1;
        int pageSize = 10;
        var expectedResponse = new PaginatedResult<DbContentChangeDto>();

        _mediatorMock
            .Setup(m => m.Send(It.Is<GetDbContentChangesQuery>(q => q.PageNumber == pageNumber && q.PageSize == pageSize), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await DbContentChangesEndpoints.ListDbContentChangesAsync(_mediatorMock.Object, pageNumber, pageSize);

        // Assert
        Assert.That(result, Is.InstanceOf<Ok<PaginatedResult<DbContentChangeDto>>>()); ;
        var okResult = result as Ok<PaginatedResult<DbContentChangeDto>>;
        Assert.That(okResult?.Value, Is.EqualTo(expectedResponse));
    }
}
