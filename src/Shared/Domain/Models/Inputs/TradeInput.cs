using FinanceLab.Shared.Domain.Models.Enums;
using JetBrains.Annotations;

namespace FinanceLab.Shared.Domain.Models.Inputs;

[PublicAPI]
public sealed class TradeInput
{
    public TradeSide Side { get; set; }
    public string BaseCoinCode { get; set; } = default!;
    public string QuoteCoinCode { get; set; } = default!;
    public double Quantity { get; set; }
}