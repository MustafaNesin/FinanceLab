using FinanceLab.Server.Application.Abstractions;
using FinanceLab.Server.Domain.Models.Entities;
using FinanceLab.Server.Domain.Models.Queries;
using FinanceLab.Shared.Application.Abstractions;
using MongoDB.Driver;

namespace FinanceLab.Server.Application.Handlers.Queries;

public class CheckMarketQueryHandler : BaseRequestHandler<CheckMarketQuery, bool>
{
    private readonly IMongoDbContext _dbContext;

    public CheckMarketQueryHandler(IMongoDbContext dbContext, ISharedResources sharedResources) : base(sharedResources)
        => _dbContext = dbContext;

    public override async Task<bool> Handle(CheckMarketQuery request, CancellationToken cancellationToken)
    {
        var markets = _dbContext.Markets;

        var filter = Builders<Market>.Filter.Eq("_id", request.BaseCoinCode + request.QuoteCoinCode);

        var query = await markets.Find(filter).SingleOrDefaultAsync(cancellationToken);
        return query is not null;
    }
}