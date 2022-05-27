using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace FinanceLab.Server.Domain.Models.Entities;

[PublicAPI]
public sealed class Asset
{
    [JsonConstructor]
    public Asset(string coinCode) => CoinCode = coinCode;

    public string CoinCode { get; init; }
    public double Amount { get; set; }
}