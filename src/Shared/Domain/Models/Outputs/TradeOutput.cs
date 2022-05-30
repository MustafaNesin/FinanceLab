using FinanceLab.Shared.Domain.Models.Dtos;
using JetBrains.Annotations;

namespace FinanceLab.Shared.Domain.Models.Outputs;

[PublicAPI]
public sealed class TradeOutput
{
    public WalletDto BaseWallet { get; init; } = default!;
    public WalletDto QuoteWallet { get; init; } = default!;
    public double Price { get; init; }
}