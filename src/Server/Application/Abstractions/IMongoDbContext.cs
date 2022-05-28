using FinanceLab.Server.Domain.Models.Entities;
using MongoDB.Driver;

namespace FinanceLab.Server.Application.Abstractions;

public interface IMongoDbContext
{
    IMongoCollection<Market> Markets { get; }
    IMongoCollection<User> Users { get; }
}