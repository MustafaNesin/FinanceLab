using FinanceLab.Shared.Domain.Models.Outputs;
using MediatR;

namespace FinanceLab.Server.Domain.Models.Queries;

public sealed record GetUserIdQuery(string UserName) : IRequest<UserIdOutput>;