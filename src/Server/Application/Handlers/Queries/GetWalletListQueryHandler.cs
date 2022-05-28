using System.Net;
using FinanceLab.Server.Application.Abstractions;
using FinanceLab.Server.Domain.Models.Queries;
using FinanceLab.Shared.Application.Abstractions;
using FinanceLab.Shared.Domain.Models.Dtos;
using FinanceLab.Shared.Domain.Models.Outputs;
using Mapster;
using MongoDB.Driver;

namespace FinanceLab.Server.Application.Handlers.Queries;

public sealed class GetWalletListQueryHandler : BaseRequestHandler<GetWalletListQuery, WalletListOutput>
{
    private readonly IMongoDbContext _dbContext;

    public GetWalletListQueryHandler(IMongoDbContext dbContext, ISharedResources sharedResources)
        : base(sharedResources)
        => _dbContext = dbContext;

    public override async Task<WalletListOutput> Handle(GetWalletListQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.Users.Find(user => user.UserName == request.UserName).Project(user => user.Wallets);
        var wallets = await query.SingleOrDefaultAsync(cancellationToken);

        if (wallets is null)
            Throw(HttpStatusCode.NotFound, L["UserNotFound"]);

        var walletsDto = wallets.Adapt<IReadOnlyCollection<WalletDto>>();
        return new WalletListOutput(walletsDto);
    }
}