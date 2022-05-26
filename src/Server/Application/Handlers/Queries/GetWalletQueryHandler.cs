using FinanceLab.Server.Application.Abstractions;
using FinanceLab.Server.Domain.Models.Queries;
using FinanceLab.Shared.Domain.Models.Dtos;
using FinanceLab.Shared.Domain.Models.Outputs;
using Mapster;
using MongoDB.Driver;

namespace FinanceLab.Server.Application.Handlers.Queries;

public sealed class GetWalletQueryHandler : BaseRequestHandler<GetUserWalletQuery, UserWalletOutput>
{
    private readonly IMongoDbContext _dbContext;

    public GetWalletQueryHandler(IMongoDbContext dbContext) => _dbContext = dbContext;

    public override Task<UserWalletOutput> Handle(GetUserWalletQuery request, CancellationToken cancellationToken)
    {
        var queryResult = _dbContext.Wallets.Find(_ => _.UserId == request.UserId).FirstOrDefaultAsync();

        var items = queryResult.Result.Adapt<IReadOnlyCollection<WalletDto>>();

        var output = new UserWalletOutput(items);

        return Task.FromResult(output);
    }
}