using FinanceLab.Server.Application.Abstractions;
using FinanceLab.Server.Domain.Models.Entities;
using FinanceLab.Server.Persistence.Contexts;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace FinanceLab.Server.Persistence.Extensions;

[PublicAPI]
public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddMongoDb(this IServiceCollection services, string connectionString)
    {
        RegisterEntities();

        services.AddSingleton<IMongoDbContext>(serviceProvider =>
            ActivatorUtilities.CreateInstance<MongoDbContext>(serviceProvider, connectionString, "FinanceLab"));

        return services;
    }

    private static void RegisterEntities()
    {
        var dateTimeOffsetSerializer = new DateTimeOffsetSerializer(BsonType.String);

        BsonClassMap.RegisterClassMap<Market>(bcm =>
        {
            bcm.AutoMap();
            bcm.MapIdMember(market => market.Symbol);
        });

        BsonClassMap.RegisterClassMap<Trade>(bcm =>
        {
            bcm.AutoMap();
            bcm.MapIdMember(trade => trade.Id).SetIdGenerator(StringObjectIdGenerator.Instance);
            bcm.MapMember(trade => trade.OccurredAt).SetSerializer(dateTimeOffsetSerializer);
        });

        BsonClassMap.RegisterClassMap<Transfer>(bcm =>
        {
            bcm.AutoMap();
            bcm.MapIdMember(transfer => transfer.CoinCode);
            bcm.MapMember(transfer => transfer.OccurredAt).SetSerializer(dateTimeOffsetSerializer);
        });

        BsonClassMap.RegisterClassMap<User>(bcm =>
        {
            bcm.AutoMap();
            bcm.MapIdMember(user => user.Id).SetIdGenerator(StringObjectIdGenerator.Instance);
            bcm.MapMember(user => user.RegisteredAt).SetSerializer(dateTimeOffsetSerializer);
            bcm.MapMember(user => user.GameRestartedAt).SetSerializer(dateTimeOffsetSerializer);
        });

        BsonClassMap.RegisterClassMap<Wallet>(bcm =>
        {
            bcm.AutoMap();
            bcm.MapIdMember(wallet => wallet.CoinCode);
        });
    }
}