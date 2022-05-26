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

        Users = mongoDatabase.GetCollection<User>(nameof(IMongoDbContext.Users));
        Wallets = mongoDatabase.GetCollection<Wallet>(nameof(IMongoDbContext.Wallets));
    }

    public IMongoCollection<User> Users { get; }
    public IMongoCollection<Wallet> Wallets { get; }
}