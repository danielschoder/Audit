using Audit.Contracts.DTOs;
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
        // Act
        var response = await _httpClient.GetAsync("/api/dbcontentchanges?pageNumber=1&pageSize=10");
        response.EnsureSuccessStatusCode();

        var actualResponse = await response.Content.ReadFromJsonAsync<PaginatedResult<DbContentChangeDto>>();

        // Assert
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
