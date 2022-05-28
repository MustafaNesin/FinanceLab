using FinanceLab.Server.Application.Abstractions;
using FinanceLab.Server.Domain.Models.Commands;
using FinanceLab.Shared.Application.Abstractions;
using JetBrains.Annotations;
using MediatR;

namespace FinanceLab.Server.Application.Handlers.Commands;

[UsedImplicitly]
public sealed class TradeCommandHandler : BaseRequestHandler<TradeCommand>
{
    private readonly IMongoDbContext _dbContext;

    public TradeCommandHandler(IMongoDbContext dbContext, ISharedResources sharedResources)
        : base(sharedResources)
        => _dbContext = dbContext;

    public override async Task<Unit> Handle(TradeCommand request, CancellationToken cancellationToken)
        => throw new NotImplementedException();
}