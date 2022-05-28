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

        BsonClassMap.RegisterClassMap<Wallet>(bcm =>
        {
            bcm.AutoMap();
            bcm.MapIdMember(asset => asset.CoinCode);
        });

        BsonClassMap.RegisterClassMap<User>(bcm =>
        {
            bcm.AutoMap();
            bcm.MapIdMember(user => user.Id).SetIdGenerator(StringObjectIdGenerator.Instance);
            bcm.MapMember(user => user.RegisteredAt).SetSerializer(dateTimeOffsetSerializer);
            bcm.MapMember(user => user.GameRestartedAt).SetSerializer(dateTimeOffsetSerializer);
        });
    }
}