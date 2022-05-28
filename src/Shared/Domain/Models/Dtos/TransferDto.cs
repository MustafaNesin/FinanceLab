using JetBrains.Annotations;

namespace FinanceLab.Shared.Domain.Models.Dtos;

[PublicAPI]
public record TransferDto(
    string CoinCode,
    double Amount,
    DateTimeOffset OccurredAt);