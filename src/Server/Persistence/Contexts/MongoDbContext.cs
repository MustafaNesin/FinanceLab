using FinanceLab.Server.Application.Abstractions;
using FinanceLab.Server.Domain.Models.Entities;
using MongoDB.Driver;

namespace FinanceLab.Server.Persistence.Contexts;

public sealed class MongoDbContext : IMongoDbContext
{
    public MongoDbContext(string connectionString, string databaseName)
    {
        var mongoClient = new MongoClient(connectionString);
        var mongoDatabase = mongoClient.GetDatabase(databaseName);

        Markets = mongoDatabase.GetCollection<Market>(nameof(IMongoDbContext.Markets));
        Users = mongoDatabase.GetCollection<User>(nameof(IMongoDbContext.Users));
    }

    public IMongoCollection<Market> Markets { get; }
    public IMongoCollection<User> Users { get; }
}