using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace FinanceLab.Server.Domain.Models.Entities;

[PublicAPI]
public sealed class Wallet
{
    [JsonConstructor]
    public Wallet(string coinCode) => CoinCode = coinCode;

    public Wallet(string coinCode, double amount) => (CoinCode, Amount) = (coinCode, amount);

    public string CoinCode { get; init; }
    public double Amount { get; set; }
}