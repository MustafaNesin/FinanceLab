using System.ComponentModel;
using System.Net;
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
public sealed class GetTradeListQueryHandler : BaseRequestHandler<GetTradeListQuery, TradeListOutput>
{
    private readonly IMongoDbContext _dbContext;

    public GetTradeListQueryHandler(IMongoDbContext dbContext, ISharedResources sharedResources)
        : base(sharedResources)
        => _dbContext = dbContext;

    public override async Task<TradeListOutput> Handle(GetTradeListQuery request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .Find(u => u.UserName == request.UserName)
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
            Throw(HttpStatusCode.NotFound, L["UserNotFound"]);

        var query = user.Trades.AsQueryable();

        if (request.Filter is {Length: > 0})
            query = from trade in query
                where trade.Side.ToString().Contains(request.Filter) ||
                      trade.OccurredAt.ToString().Contains(request.Filter) ||
                      trade.BaseCoinCode.Contains(request.Filter) ||
                      trade.QuoteCoinCode.Contains(request.Filter)
                select trade;

        query = request.Sort.By switch
        {
            nameof(TradeDto.Amount) => request.Sort.Direction switch
            {
                ListSortDirection.Ascending => from trade in query orderby trade.Amount select trade,
                ListSortDirection.Descending => from trade in query orderby trade.Amount descending select trade,
                _ => query
            },
            nameof(TradeDto.Price) => request.Sort.Direction switch
            {
                ListSortDirection.Ascending => from trade in query orderby trade.Price select trade,
                ListSortDirection.Descending => from trade in query orderby trade.Price descending select trade,
                _ => query
            },
            nameof(TradeDto.Side) => request.Sort.Direction switch
            {
                ListSortDirection.Ascending => from trade in query orderby trade.Side select trade,
                ListSortDirection.Descending => from trade in query orderby trade.Side descending select trade,
                _ => query
            },
            nameof(TradeDto.BaseCoinCode) => request.Sort.Direction switch
            {
                ListSortDirection.Ascending => from trade in query orderby trade.BaseCoinCode select trade,
                ListSortDirection.Descending => from trade in query orderby trade.BaseCoinCode descending select trade,
                _ => query
            },
            nameof(TradeDto.QuoteCoinCode) => request.Sort.Direction switch
            {
                ListSortDirection.Ascending => from trade in query orderby trade.QuoteCoinCode select trade,
                ListSortDirection.Descending => from trade in query orderby trade.QuoteCoinCode descending select trade,
                _ => query
            },
            //TODO: Cast date time to string
            /*nameof(TradeDto.OccurredAt) => request.Sort.Direction switch
            {
                ListSortDirection.Ascending => from trade in query orderby trade.OccuredAt select trade,
                ListSortDirection.Descending => from trade in query orderby trade.OccuredAt descending select trade,
                _ => query
            },*/
            _ => query
        };

        var total = query.Count();

        query = query.Skip(request.Page.Current * request.Page.Size).Take(request.Page.Size);

        var items = query.Adapt<IReadOnlyCollection<TradeDto>>();
        var output = new TradeListOutput(items, total);

        return output;
    }
}