using FinanceLab.Server.Application.Abstractions;
using FinanceLab.Server.Domain.Models.Queries;
using FinanceLab.Shared.Domain.Models.Dtos;
using FinanceLab.Shared.Domain.Models.Outputs;
using Mapster;
using MongoDB.Driver;

namespace FinanceLab.Server.Application.Handlers.Queries;

public sealed class GetUserWalletQueryHandler : BaseRequestHandler<GetUserWalletQuery, UserWalletOutput>
{
    private readonly IMongoDbContext _dbContext;

    public GetUserWalletQueryHandler(IMongoDbContext dbContext) => _dbContext = dbContext;

    public override async Task<UserWalletOutput> Handle(GetUserWalletQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.Users.Find(wallet => wallet.UserName == request.UserName);
        var wallet = await query.FirstOrDefaultAsync(cancellationToken);
        var walletDto = wallet.Adapt<WalletDto>();

        return new UserWalletOutput(walletDto);
    }
}