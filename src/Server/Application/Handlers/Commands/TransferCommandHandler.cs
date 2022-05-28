using FinanceLab.Server.Application.Abstractions;
using FinanceLab.Server.Domain.Models.Commands;
using FinanceLab.Shared.Application.Abstractions;
using JetBrains.Annotations;
using MediatR;

namespace FinanceLab.Server.Application.Handlers.Commands;

[UsedImplicitly]
public sealed class TransferCommandHandler : BaseRequestHandler<TransferCommand>
{
    private readonly IMongoDbContext _dbContext;

    public TransferCommandHandler(IMongoDbContext dbContext, ISharedResources sharedResources)
        : base(sharedResources)
        => _dbContext = dbContext;

    public override async Task<Unit> Handle(TransferCommand request, CancellationToken cancellationToken)
        => throw new NotImplementedException();
}