using JetBrains.Annotations;

namespace FinanceLab.Shared.Domain.Models.Outputs;

[PublicAPI]
public sealed class TradeOutput
{
    public double TradingPrice { get; init; }
}