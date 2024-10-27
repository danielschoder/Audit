using Audit.Contracts.Responses;
using MediatR;

namespace Audit.Application.Queries;

public class GetVersionQuery : IRequest<Alive> { }
