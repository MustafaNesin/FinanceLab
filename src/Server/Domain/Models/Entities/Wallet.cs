using JetBrains.Annotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FinanceLab.Server.Domain.Models.Entities;

[PublicAPI]
public sealed class Wallet
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; init; }

    public string? UserId { get; init; }

    [BsonRepresentation(BsonType.Array)]
    public List<CoinPurchase>? Assets { get; set; }
}