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
public sealed class TradeCommandHandler : BaseRequestHandler<TradeCommand>
{
    private readonly IMongoDbContext _dbContext;

    public TradeCommandHandler(IMongoDbContext dbContext, ISharedResources sharedResources)
        : base(sharedResources)
        => _dbContext = dbContext;

    public override async Task<Unit> Handle(TradeCommand request, CancellationToken cancellationToken)
    {
        // {"symbol":"BTCTRY","price":"475768.00000000"}
        // Request:
        //  BaseCoinCode: BTC
        //  QuoteCoinCode: TRY =>  Birim : QuoteCoinCode
        //  Quantity: 0.0000001
        //  Price: 475768.00000000
        var user = await _dbContext.Users
            .Find(u => u.UserName == request.UserName)
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
            Throw(HttpStatusCode.NotFound, L["UserNotFound"]);

        if (request.Price <= 0)
            Throw(HttpStatusCode.BadRequest,
                L["FaultyPriceInformation", request.BaseCoinCode, request.QuoteCoinCode, request.Price]);

        //Wallets
        var quoteWallet = user.Wallets.FirstOrDefault(w => w.CoinCode == request.QuoteCoinCode);
        var baseWallet = user.Wallets.FirstOrDefault(w => w.CoinCode == request.BaseCoinCode);

        //Filters
        var filterUserName = Builders<User>.Filter.Eq(nameof(user.UserName), request.UserName);
        var filterQuoteWallet =
            Builders<User>.Filter.ElemMatch(u => u.Wallets, w => w.CoinCode == request.QuoteCoinCode);
        var filterBaseWallet =
            Builders<User>.Filter.ElemMatch(u => u.Wallets, w => w.CoinCode == request.BaseCoinCode);

        //Options
        //If updateOne does not find with given filters, this option makes it create a new one
        var upsertOption = new UpdateOptions { IsUpsert = true };

        switch (request.Side)
        {
            case TradeSide.Buy:
                if (quoteWallet is not null)
                {
                    var purchaseAmount = request.Quantity * request.Price;
                    var newAmount = quoteWallet.Amount - purchaseAmount;

                    if (newAmount > 0d)
                    {
                        await AddTrade(request, cancellationToken, user);

                        var updateQuoteWallet =
                            Builders<User>.Update.Set(
                                string.Join(".", nameof(User.Wallets), "$", nameof(Wallet.Amount)), newAmount);

                        //Updates the amount in wallet with quoteCoinCode given in request.  
                        await _dbContext.Users.UpdateOneAsync(filterUserName & filterQuoteWallet, updateQuoteWallet,
                            null, cancellationToken);

                        //If target coin already exist, take its amount and add purchased amount to it
                        //Else create new wallet and assign its amount as purchasedAmount
                        if (baseWallet is not null)
                        {
                            baseWallet.Amount += request.Quantity;

                            var updateBaseWallet = Builders<User>.Update.Set(
                                string.Join(".", nameof(User.Wallets), "$", nameof(Wallet.Amount)), baseWallet.Amount);

                            //Update for target wallet
                            await _dbContext.Users.UpdateOneAsync(filterUserName & filterBaseWallet, updateBaseWallet,
                                null, cancellationToken);
                        }
                        else
                        {
                            var newWalletBson = new BsonDocument("_id", request.BaseCoinCode);

                            newWalletBson.Add(nameof(Wallet.Amount), request.Quantity);

                            baseWallet = BsonSerializer.Deserialize<Wallet>(newWalletBson);

                            var updateBaseWallet = Builders<User>.Update.Push(nameof(User.Wallets), baseWallet);

                            //Update for target wallet
                            await _dbContext.Users.UpdateOneAsync(filterUserName, updateBaseWallet,
                                upsertOption, cancellationToken);
                        }
                    }
                    else
                        Throw(HttpStatusCode.NotAcceptable, L["PurchaseNotAuthorized"]);
                }
                else
                    Throw(HttpStatusCode.NotFound, L["WalletTypeDoesNotExist", request.QuoteCoinCode]);

                break;
            case TradeSide.Sell:
                var soldAmount = request.Quantity * request.Price;

                if (baseWallet is not null)
                {
                    if (baseWallet.Amount >= request.Quantity)
                    {
                        await AddTrade(request, cancellationToken, user);

                        var newAmount = baseWallet.Amount - request.Quantity;

                        var updateBaseWallet =
                            Builders<User>.Update
                                .Set(string.Join(".", nameof(User.Wallets), "$", nameof(Wallet.Amount)), newAmount);

                        //Update base wallet after selling
                        await _dbContext.Users.UpdateOneAsync(filterUserName & filterBaseWallet, updateBaseWallet, null,
                            cancellationToken);

                        if (quoteWallet is not null)
                        {
                            quoteWallet.Amount += soldAmount;

                            var updateQuoteWallet = Builders<User>.Update
                                .Set(string.Join(".", nameof(User.Wallets), "$", nameof(Wallet.Amount)),
                                    quoteWallet.Amount);

                            //Update for quote wallet
                            await _dbContext.Users.UpdateOneAsync(filterUserName & filterQuoteWallet, updateQuoteWallet,
                                null, cancellationToken);
                        }
                        else
                        {
                            var newWalletBson = new BsonDocument("_id", request.QuoteCoinCode);

                            newWalletBson.Add(nameof(Wallet.Amount), soldAmount);

                            quoteWallet = BsonSerializer.Deserialize<Wallet>(newWalletBson);

                            var updateQuoteWallet = Builders<User>.Update.Push(nameof(User.Wallets), quoteWallet);

                            //Update for target wallet
                            await _dbContext.Users.UpdateOneAsync(filterUserName, updateQuoteWallet,
                                upsertOption, cancellationToken);
                        }
                    }
                    else
                        Throw(HttpStatusCode.NotAcceptable, L["PurchaseNotAuthorized"]);
                }
                else
                {
                    var coinCode = request.BaseCoinCode;
                    Throw(HttpStatusCode.NotFound, L["WalletTypeDoesNotExist", coinCode]);
                }

                break;
            default:
                Throw(HttpStatusCode.BadRequest, L["InvalidTradeSide"]);
                break;
        }

        return Unit.Value;
    }

    public async Task<Unit> AddTrade(TradeCommand request, CancellationToken cancellationToken, User user)
    {
        var trade = new Trade
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Side = request.Side,
            BaseCoinCode = request.BaseCoinCode,
            QuoteCoinCode = request.QuoteCoinCode,
            Quantity = request.Quantity,
            Price = request.Price
        };

        var filter = Builders<User>.Filter.Eq(nameof(User.UserName), request.UserName);
        var update = Builders<User>.Update.Push(nameof(user.Trades), trade);

        await _dbContext.Users.UpdateOneAsync(filter, update, null, cancellationToken);

        return Unit.Value;
    }
}