using System.Net;
using FinanceLab.Server.Application.Abstractions;
using FinanceLab.Server.Domain.Models.Commands;
using FinanceLab.Server.Domain.Models.Entities;
using FinanceLab.Shared.Application.Abstractions;
using FinanceLab.Shared.Domain.Models.Enums;
using JetBrains.Annotations;
using MediatR;
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

        //Wallets
        var quoteWallet = user.Wallets.FirstOrDefault(w => w.CoinCode == request.QuoteCoinCode);
        var baseWallet = user.Wallets.FirstOrDefault(w => w.CoinCode == request.BaseCoinCode + request.QuoteCoinCode);

        //Filters
        var filterUserName = Builders<User>.Filter.Eq("UserName", request.UserName);
        var filterQuoteWallet =
            Builders<User>.Filter.ElemMatch(wallet => user.Wallets, w => w.CoinCode == request.QuoteCoinCode);
        var filterBaseWallet = Builders<User>.Filter.ElemMatch(wallet => user.Wallets,
            w => w.CoinCode == request.BaseCoinCode + request.QuoteCoinCode);

        //Options
        //If updateOne does not find with given filters, this option makes it create a new one
        var upsertOption = new UpdateOptions {IsUpsert = true};

        switch (request.Side)
        {
            case TradeSide.Buy:
                if (quoteWallet is not null)
                {
                    var purchaseAmount = request.Quantity * request.Price;
                    var newAmount = quoteWallet.Amount - purchaseAmount;

                    if (newAmount > 0d)
                    {
                        var updateQuoteWallet = Builders<User>.Update.Set(
                            x => x.Wallets.Single(w => w.CoinCode.Equals(request.QuoteCoinCode)).Amount, newAmount);

                        //Updates the amount in wallet with quoteCoinCode given in request.  
                        await _dbContext.Users.UpdateOneAsync(filterUserName & filterQuoteWallet, updateQuoteWallet,
                            null, cancellationToken);

                        //If target coin already exist, take its amount and add purchased amount to it
                        //Else create new wallet and assign its amount as purchasedAmount
                        baseWallet.Amount = baseWallet is null ? purchaseAmount : baseWallet.Amount + purchaseAmount;

                        var updateBaseWallet = Builders<User>.Update.Push(nameof(User.Wallets), baseWallet);

                        //Update for target wallet
                        await _dbContext.Users.UpdateOneAsync(filterUserName & filterBaseWallet, updateBaseWallet,
                            upsertOption, cancellationToken);
                    }
                    else
                        Throw(HttpStatusCode.NotAcceptable, L["PurchaseNotAuthorized"]);
                }
                else
                    Throw(HttpStatusCode.NotFound, L["WalletTypeDoesNotExist", request.QuoteCoinCode]);

                break;
            case TradeSide.Sell:
                var soldAmount = request.Quantity * request.Price;

                if (baseWallet is not null && baseWallet.Amount >= soldAmount)
                {
                    if (baseWallet.Amount >= soldAmount)
                    {
                        var newAmount = baseWallet.Amount - soldAmount;

                        var updateBaseWallet = Builders<User>.Update.Set(
                            x => x.Wallets.Single(w => w.CoinCode.Equals(request.BaseCoinCode + request.QuoteCoinCode))
                                .Amount, newAmount);

                        //Update base wallet after selling
                        await _dbContext.Users.UpdateOneAsync(filterUserName & filterBaseWallet, updateBaseWallet, null,
                            cancellationToken);

                        quoteWallet.Amount = quoteWallet is null ? soldAmount : quoteWallet.Amount + soldAmount;

                        var updateQuoteWallet = Builders<User>.Update.Push(nameof(User.Wallets), quoteWallet);

                        //Update for quote wallet
                        await _dbContext.Users.UpdateOneAsync(filterUserName & filterQuoteWallet, updateQuoteWallet,
                            upsertOption, cancellationToken);
                    }
                    else
                        Throw(HttpStatusCode.NotAcceptable, L["PurchaseNotAuthorized"]);
                }
                else
                {
                    var coinCode = request.BaseCoinCode + request.QuoteCoinCode;
                    Throw(HttpStatusCode.NotFound, L["WalletTypeDoesNotExist", coinCode]);
                }

                break;
            default:
                Throw(HttpStatusCode.BadRequest, L["InvalidTradeSide"]);
                break;
        }

        return Unit.Value;
    }
}