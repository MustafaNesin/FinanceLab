using JetBrains.Annotations;

namespace FinanceLab.Shared.Domain.Models.Inputs;

[PublicAPI]
public sealed class TransferInput
{
    public string CoinCode { get; set; } = default!;
    public double Amount { get; set; }
}