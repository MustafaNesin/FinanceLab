using FinanceLab.Server.Application.Abstractions;
using FinanceLab.Server.Domain.Models.Entities;
using FinanceLab.Server.Persistence.Contexts;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;

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
        BsonClassMap.RegisterClassMap<User>(bcm =>
        {
            bcm.AutoMap();
            bcm.MapIdMember(d => d.Id).SetIdGenerator(StringObjectIdGenerator.Instance);
        });
    }
}