using System.Net;
using FinanceLab.Server.Application.Abstractions;
using FinanceLab.Server.Domain.Models.Commands;
using FinanceLab.Server.Domain.Models.Entities;
using FinanceLab.Shared.Application.Abstractions;
using JetBrains.Annotations;
using MediatR;
using MongoDB.Driver;

namespace FinanceLab.Server.Application.Handlers.Commands;

[UsedImplicitly]
public sealed class TransferCommandHandler : BaseRequestHandler<TransferCommand>
{
    private readonly IMongoDbContext _dbContext;

    public TransferCommandHandler(IMongoDbContext dbContext, ISharedResources sharedResources)
        : base(sharedResources)
        => _dbContext = dbContext;

    public override async Task<Unit> Handle(TransferCommand request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .Find(u => u.UserName == request.UserName)
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
            Throw(HttpStatusCode.NotFound, L["UserNotFound"]);

        var transfer = new Transfer(request.CoinCode, request.Amount);

        var filter = Builders<User>.Filter.Eq("UserName", request.UserName);
        var update = Builders<User>.Update.Push("Transfers", transfer);

        await _dbContext.Users.UpdateOneAsync(filter, update, null, cancellationToken);

        return Unit.Value;
    }
}