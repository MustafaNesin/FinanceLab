using FinanceLab.Server.Application.Abstractions;
using FinanceLab.Server.Domain.Models.Queries;
using FinanceLab.Shared.Application.Abstractions;
using FinanceLab.Shared.Domain.Models.Outputs;
using JetBrains.Annotations;

namespace FinanceLab.Server.Application.Handlers.Queries;

[UsedImplicitly]
public sealed class GetTradeListQueryHandler : BaseRequestHandler<GetTradeListQuery, TradeListOutput>
{
    private readonly IMongoDbContext _dbContext;

    public GetTradeListQueryHandler(IMongoDbContext dbContext, ISharedResources sharedResources)
        : base(sharedResources)
        => _dbContext = dbContext;

    public override async Task<TradeListOutput> Handle(GetTradeListQuery request, CancellationToken cancellationToken)
        => throw new NotImplementedException();
}