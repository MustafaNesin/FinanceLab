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
public sealed class GetTransferListQueryHandler : BaseRequestHandler<GetTransferListQuery, TransferListOutput>
{
    private readonly IMongoDbContext _dbContext;

    public GetTransferListQueryHandler(IMongoDbContext dbContext, ISharedResources sharedResources)
        : base(sharedResources)
        => _dbContext = dbContext;

    public override async Task<TransferListOutput> Handle(GetTransferListQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .Find(u => u.UserName == request.UserName)
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
            Throw(HttpStatusCode.NotFound, L["UserNotFound"]);

        var query = user.Transfers.AsQueryable();

        if (request.Filter is {Length: > 0})
            query = from transfer in query
                where transfer.CoinCode.Contains(request.Filter) ||
                      transfer.OccurredAt.ToString().Contains(request.Filter)
                select transfer;

        query = request.Sort.By switch
        {
            nameof(TransferDto.Amount) => request.Sort.Direction switch
            {
                ListSortDirection.Ascending => from transfer in query orderby transfer.Amount select transfer,
                ListSortDirection.Descending => from transfer in query
                    orderby transfer.Amount descending
                    select transfer,
                _ => query
            },
            nameof(TransferDto.CoinCode) => request.Sort.Direction switch
            {
                ListSortDirection.Ascending => from transfer in query orderby transfer.CoinCode select transfer,
                ListSortDirection.Descending => from transfer in query
                    orderby transfer.CoinCode descending
                    select transfer,
                _ => query
            },
            nameof(TransferDto.OccurredAt) => request.Sort.Direction switch
            {
                ListSortDirection.Ascending => from transfer in query orderby transfer.OccurredAt select transfer,
                ListSortDirection.Descending => from transfer in query
                    orderby transfer.OccurredAt descending
                    select transfer,
                _ => query
            },
            _ => query
        };

        var total = query.Count();

        query = query.Skip(request.Page.Current * request.Page.Size).Take(request.Page.Size);

        var items = query.Adapt<IReadOnlyCollection<TransferDto>>();
        var output = new TransferListOutput(items, total);

        return output;
    }
}