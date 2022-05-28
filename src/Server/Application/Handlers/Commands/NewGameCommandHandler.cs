using FinanceLab.Server.Application.Abstractions;
using FinanceLab.Server.Domain.Models.Commands;
using FinanceLab.Server.Domain.Models.Entities;
using FinanceLab.Shared.Application.Abstractions;
using JetBrains.Annotations;
using MediatR;
using MongoDB.Driver;

namespace FinanceLab.Server.Application.Handlers.Commands;

[UsedImplicitly]
public sealed class NewGameCommandHandler : BaseRequestHandler<NewGameCommand>
{
    private readonly IMongoDbContext _dbContext;

    public NewGameCommandHandler(IMongoDbContext dbContext, ISharedResources sharedResources)
        : base(sharedResources)
        => _dbContext = dbContext;

    public override async Task<Unit> Handle(NewGameCommand request, CancellationToken cancellationToken)
    {
        var filterDefinition = new ExpressionFilterDefinition<User>(user => user.UserName == request.UserName);
        var updateDefinition = new UpdateDefinitionBuilder<User>()
            .Set(user => user.GameDifficulty, request.GameDifficulty)
            .Set(user => user.GameRestartedAt, DateTimeOffset.UtcNow)
            .Set(user => user.Trades, new List<Trade>())
            .Set(user => user.Transfers, new List<Transfer>())
            .Set(user => user.Wallets, new List<Wallet>());

        await _dbContext.Users.FindOneAndUpdateAsync(filterDefinition, updateDefinition, null, cancellationToken);
        return Unit.Value;
    }
}