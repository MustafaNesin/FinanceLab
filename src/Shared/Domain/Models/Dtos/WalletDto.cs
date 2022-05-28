using JetBrains.Annotations;

namespace FinanceLab.Shared.Domain.Models.Dtos;

[PublicAPI]
public record WalletDto(string CoinCode, double Amount);