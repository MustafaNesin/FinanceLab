using JetBrains.Annotations;

namespace FinanceLab.Shared.Domain.Models.Dtos;

[PublicAPI]
public sealed record WalletDto(
    string CoinCode,
    double Amount);