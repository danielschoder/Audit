namespace Audit.Contracts.DTOs;

public record PaginatedResult<T>(
    int CurrentPage,
    int PageSize,
    int TotalRecords)
{
    public IEnumerable<T> Data { get; set; } = [];
}
