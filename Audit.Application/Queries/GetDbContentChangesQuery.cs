using Audit.Contracts.DTOs;
using MediatR;

namespace Audit.Application.Queries;

public record GetDbContentChangesQuery(int PageNumber, int PageSize)
    : IRequest<PaginatedResult<DbContentChangeDto>>;
