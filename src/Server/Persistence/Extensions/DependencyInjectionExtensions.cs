using FinanceLab.Server.Application.Abstractions;
using FinanceLab.Server.Domain.Models.Entities;
using FinanceLab.Server.Persistence.Contexts;
using FinanceLab.Shared.Application.Constants;
using FinanceLab.Shared.Domain.Models.Enums;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

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

    public static IApplicationBuilder SeedDatabase(this IApplicationBuilder app)
    {
        var dbContext = app.ApplicationServices.GetRequiredService<IMongoDbContext>();

        if (!dbContext.Users.AsQueryable().Any(user => user.Role == RoleConstants.Admin))
        {
            using var scope = app.ApplicationServices.CreateScope();
            var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher<User>>();
            var adminUser = new User
            {
                UserName = "admin",
                FirstName = "Admin",
                LastName = "User",
                Role = RoleConstants.Admin,
                GameDifficulty = GameDifficulty.Sandbox
            };

            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "admin");
            dbContext.Users.InsertOne(adminUser);
        }
            
        if (dbContext.Markets.AsQueryable().Any())
            return app;

        dbContext.Markets.InsertMany(new Market[]
        {
            new("USDT", "TRY"),
            new("BTC", "TRY"),
            new("BTC", "USDT"),
            new("BNB", "TRY"),
            new("BNB", "USDT"),
            new("ETH", "TRY"),
            new("ETH", "USDT"),
            new("BNB", "BTC"),
            new("BNB", "ETH"),
            new("ETH", "BTC"),
            new("XRP", "BTC"),
            new("XRP", "BNB"),
            new("XRP", "ETH"),
            new("DOGE", "BTC"),
            new("ADA", "BTC"),
            new("ADA", "BNB"),
            new("ADA", "ETH"),
            new("SOL", "BTC"),
            new("SOL", "BNB"),
            new("SOL", "ETH"),
            new("TRX", "BTC"),
            new("TRX", "BNB"),
            new("TRX", "ETH"),
            new("TRX", "XRP"),
            new("XRP", "TRY"),
            new("XRP", "USDT"),
            new("DOGE", "TRY"),
            new("DOGE", "USDT"),
            new("ADA", "TRY"),
            new("ADA", "USDT"),
            new("SOL", "TRY"),
            new("SOL", "USDT"),
            new("TRX", "TRY"),
            new("TRX", "USDT"),
            new("BTC", "EUR"),
            new("BNB", "EUR"),
            new("ETH", "EUR"),
            new("XRP", "EUR"),
            new("DOGE", "EUR"),
            new("ADA", "EUR"),
            new("SOL", "EUR"),
            new("TRX", "EUR")
        });

        return app;
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