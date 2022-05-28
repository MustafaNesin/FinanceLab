using System.ComponentModel;
using FinanceLab.Server.Application.Abstractions;
using FinanceLab.Server.Domain.Models.Queries;
using FinanceLab.Shared.Application.Abstractions;
using FinanceLab.Shared.Domain.Models.Dtos;
using FinanceLab.Shared.Domain.Models.Outputs;
using JetBrains.Annotations;
using Mapster;
using MongoDB.Driver;

namespace FinanceLab.Server.Application.Handlers.Queries;

[UsedImplicitly]
public sealed class GetMarketListQueryHandler : BaseRequestHandler<GetMarketListQuery, MarketListOutput>
{
    private readonly IMongoDbContext _dbContext;

    public GetMarketListQueryHandler(IMongoDbContext dbContext, ISharedResources sharedResources)
        : base(sharedResources)
        => _dbContext = dbContext;

    public override Task<MarketListOutput> Handle(GetMarketListQuery request, CancellationToken cancellationToken)
    {
        var markets = _dbContext.Markets;

        var query = markets.AsQueryable().AsQueryable();

        if (request.Filter is {Length: > 0})
            query = from market in query
                where market.Symbol.Contains(request.Filter) ||
                      market.BaseCoinCode.Contains(request.Filter) ||
                      market.QuoteCoinCode.Contains(request.Filter)
                select market;

        query = request.Sort.By switch
        {
            nameof(MarketDto.Symbol) => request.Sort.Direction switch
            {
                ListSortDirection.Ascending => from market in query orderby market.Symbol select market,
                ListSortDirection.Descending => from market in query orderby market.Symbol descending select market,
                _ => query
            },
            nameof(MarketDto.BaseCoinCode) => request.Sort.Direction switch
            {
                ListSortDirection.Ascending => from market in query orderby market.BaseCoinCode select market,
                ListSortDirection.Descending => from market in query
                    orderby market.BaseCoinCode descending
                    select market,
                _ => query
            },
            nameof(MarketDto.QuoteCoinCode) => request.Sort.Direction switch
            {
                ListSortDirection.Ascending => from market in query orderby market.QuoteCoinCode select market,
                ListSortDirection.Descending => from market in query
                    orderby market.QuoteCoinCode descending
                    select market,
                _ => query
            },
            _ => query
        };

        var total = query.Count();

        query = query.Skip(request.Page.Current * request.Page.Size).Take(request.Page.Size);

        var items = query.Adapt<IReadOnlyCollection<MarketDto>>();
        var output = new MarketListOutput(items, total);

        return Task.FromResult(output);
    }
}