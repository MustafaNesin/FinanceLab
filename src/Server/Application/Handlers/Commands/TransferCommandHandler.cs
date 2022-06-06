using System.Net;
using FinanceLab.Server.Application.Abstractions;
using FinanceLab.Server.Domain.Models.Commands;
using FinanceLab.Server.Domain.Models.Entities;
using FinanceLab.Shared.Application.Abstractions;
using FinanceLab.Shared.Domain.Models.Enums;
using JetBrains.Annotations;
using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
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

        if (user.GameDifficulty != GameDifficulty.Sandbox)
            Throw(HttpStatusCode.BadRequest, L["WrongGameType"]);

        var transfer = new Transfer(request.CoinCode, request.Amount);

        var filter = Builders<User>.Filter.Eq(nameof(User.UserName), request.UserName);
        var update = Builders<User>.Update.Push(nameof(user.Transfers), transfer);

        await _dbContext.Users.UpdateOneAsync(filter, update, null, cancellationToken);

        var wallet = user.Wallets.FirstOrDefault(w => w.CoinCode == request.CoinCode);

        var filterUserName = Builders<User>.Filter.Eq(nameof(user.UserName), request.UserName);
        var filterWallet =
            Builders<User>.Filter.ElemMatch(u => u.Wallets, w => w.CoinCode == request.CoinCode);

        if (wallet is not null)
        {
            var updateBaseWallet = Builders<User>.Update.Set(
                string.Join(".", nameof(User.Wallets), "$", nameof(Wallet.Amount)), wallet.Amount + request.Amount);

            //Update for target wallet
            await _dbContext.Users.UpdateOneAsync(filterUserName & filterWallet, updateBaseWallet,
                null, cancellationToken);
        }
        else
        {
            var newWalletBson = new BsonDocument("_id", request.CoinCode);

            newWalletBson.Add(nameof(Wallet.Amount), request.Amount);

            wallet = BsonSerializer.Deserialize<Wallet>(newWalletBson);

            var updateWallet = Builders<User>.Update.Push(nameof(User.Wallets), wallet);

            //Update for target wallet
            await _dbContext.Users.UpdateOneAsync(filterUserName, updateWallet,
                new UpdateOptions {IsUpsert = true}, cancellationToken);
        }

        return Unit.Value;
    }
}