using Audit.Application.Queries;
using Audit.Contracts.DTOs;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Net.Http.Json;

namespace Audit.Api.Tests.Integration;

[TestFixture]
public class DbContentChangesEndpointsTests
{
    private TestWebApplication _factory;
    private HttpClient _httpClient;

    [SetUp]
    public void SetUp()
    {
        _factory = new TestWebApplication();
        _httpClient = _factory.CreateClient();
    }

    [Test]
    public async Task ListDriversAsync_ShouldReturnOk_WithExpectedData()
    {
        // Arrange
        var mediator = _factory.Services.GetService<IMediator>() ?? throw new Exception();
        var mockMediator = Mock.Get(mediator);
        int pageNumber = 1;
        int pageSize = 10;

        // Act
        var response = await _httpClient.GetAsync($"/api/dbcontentchanges?pageNumber={pageNumber}&pageSize={pageSize}");
        response.EnsureSuccessStatusCode();

        var actualResponse = await response.Content.ReadFromJsonAsync<PaginatedResult<DbContentChangeDto>>();

        // Assert
        mockMediator.Verify(m => m.Send(
            It.Is<GetDbContentChangesQuery>(query => query.PageNumber == pageNumber && query.PageSize == pageSize),
            It.IsAny<CancellationToken>()
        ), Times.Once);
        Assert.That(actualResponse, Is.Not.Null);
        Assert.That(actualResponse.TotalRecords, Is.EqualTo(0));
    }

    [TearDown]
    public void TearDown()
    {
        _factory?.Dispose();
        _httpClient?.Dispose();
    }
}
