using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MongoDB.Bson.Serialization.Attributes;

namespace FinanceLab.Server.Domain.Models.Entities;

[PublicAPI]
public sealed class Wallet
{
    public Wallet(string coinCode) => CoinCode = coinCode;

    public Wallet(string coinCode, double amount) => (CoinCode, Amount) = (coinCode, amount);


    [JsonPropertyName("_id")]
    [BsonElement("_id")]
    public string CoinCode { get; init; }

    [BsonElement("Amount")]
    public double Amount { get; set; }
}