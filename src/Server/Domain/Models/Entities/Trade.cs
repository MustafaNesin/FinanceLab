using FinanceLab.Shared.Domain.Models.Enums;
using JetBrains.Annotations;

namespace FinanceLab.Server.Domain.Models.Entities;

[PublicAPI]
public sealed class Trade
{
    public string Id { get; init; } = default!;
    public TradeSide Side { get; init; }
    public string BaseCoinCode { get; init; } = default!;
    public string QuoteCoinCode { get; init; } = default!;
    public double Quantity { get; init; }
    public double Price { get; init; }
    public DateTimeOffset OccurredAt { get; init; } = DateTimeOffset.UtcNow;
}