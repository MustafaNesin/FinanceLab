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
public sealed class GetWalletListQueryHandler : BaseRequestHandler<GetWalletListQuery, WalletListOutput>
{
    private readonly IMongoDbContext _dbContext;

    public GetWalletListQueryHandler(IMongoDbContext dbContext, ISharedResources sharedResources)
        : base(sharedResources)
        => _dbContext = dbContext;

    public override async Task<WalletListOutput> Handle(GetWalletListQuery request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .Find(u => u.UserName == request.UserName)
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
            Throw(HttpStatusCode.NotFound, L["UserNotFound"]);

        var query = user.Wallets.AsQueryable();

        if (request.Filter is { Length: > 0 })
            query = from wallet in query
                where wallet.CoinCode.Contains(request.Filter)
                select wallet;

        query = request.Sort.By switch
        {
            nameof(WalletDto.CoinCode) => request.Sort.Direction switch
            {
                ListSortDirection.Ascending => from wallet in query orderby wallet.CoinCode select wallet,
                ListSortDirection.Descending => from wallet in query orderby wallet.CoinCode descending select wallet,
                _ => query
            },
            nameof(WalletDto.Amount) => request.Sort.Direction switch
            {
                ListSortDirection.Ascending => from wallet in query orderby wallet.Amount select wallet,
                ListSortDirection.Descending => from wallet in query orderby wallet.Amount descending select wallet,
                _ => query
            },
            _ => query
        };

        var total = query.Count();

        query = query.Skip(request.Page.Current * request.Page.Size).Take(request.Page.Size);

        var items = query.ToList().Adapt<IReadOnlyCollection<WalletDto>>();
        var output = new WalletListOutput(items, total);

        return output;
    }
}