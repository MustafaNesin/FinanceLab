using FinanceLab.Shared.Domain.Models.Dtos;
using FinanceLab.Shared.Domain.Models.Outputs;
using JetBrains.Annotations;
using MediatR;

namespace FinanceLab.Server.Domain.Models.Queries;

[PublicAPI]
public sealed class GetTradeListQuery : BaseListQuery, IRequest<TradeListOutput>
{
    public GetTradeListQuery(string userName, string? filter, PaginationDto page, SortingDto sort)
        : base(filter, page, sort)
        => UserName = userName;

    public string UserName { get; init; }
}