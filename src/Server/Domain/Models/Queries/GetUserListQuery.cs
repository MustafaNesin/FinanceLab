using FinanceLab.Shared.Domain.Models.Dtos;
using FinanceLab.Shared.Domain.Models.Outputs;
using JetBrains.Annotations;
using MediatR;

namespace FinanceLab.Server.Domain.Models.Queries;

[PublicAPI]
public sealed class GetUserListQuery : BaseListQuery, IRequest<UserListOutput>
{
    public GetUserListQuery(string? filter, PaginationDto page, SortingDto sort) : base(filter, page, sort)
    {
    }
}