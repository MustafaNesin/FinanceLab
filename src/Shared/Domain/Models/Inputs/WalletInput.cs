using JetBrains.Annotations;

namespace FinanceLab.Shared.Domain.Models.Inputs;

[PublicAPI]
public sealed class WalletInput
{
    public string? UserName { get; set; }
    public string CoinCode { get; set; } = default!;
}