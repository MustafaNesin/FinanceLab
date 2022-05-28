using JetBrains.Annotations;

namespace FinanceLab.Server.Domain.Models.Entities;

[PublicAPI]
public sealed record Transfer(string CoinCode, double Amount, DateTimeOffset OccurredAt)
{
    public Transfer(string coinCode, double amount) : this(coinCode, amount, DateTimeOffset.UtcNow)
    {
    }
}