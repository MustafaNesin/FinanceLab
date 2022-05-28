using FinanceLab.Server.Application.Abstractions;
using FinanceLab.Server.Domain.Models.Queries;
using FinanceLab.Shared.Application.Abstractions;
using FinanceLab.Shared.Domain.Models.Outputs;
using JetBrains.Annotations;

namespace FinanceLab.Server.Application.Handlers.Queries;

[UsedImplicitly]
public sealed class GetTransferListQueryHandler : BaseRequestHandler<GetTransferListQuery, TransferListOutput>
{
    private readonly IMongoDbContext _dbContext;

    public GetTransferListQueryHandler(IMongoDbContext dbContext, ISharedResources sharedResources)
        : base(sharedResources)
        => _dbContext = dbContext;

    public override async Task<TransferListOutput> Handle(
        GetTransferListQuery request, CancellationToken cancellationToken)
        => throw new NotImplementedException();
}