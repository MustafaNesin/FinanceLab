using FinanceLab.Shared.Domain.Models.Dtos;
using FinanceLab.Shared.Domain.Models.Outputs;
using JetBrains.Annotations;
using MediatR;

namespace FinanceLab.Server.Domain.Models.Queries;

[PublicAPI]
public sealed class GetMarketListQuery : BaseListQuery, IRequest<MarketListOutput>
{
    public GetMarketListQuery(string? filter, PaginationDto page, SortingDto sort)
        : base(filter, page, sort)
    {
    }
}