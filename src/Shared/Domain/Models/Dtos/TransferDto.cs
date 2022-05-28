using JetBrains.Annotations;

namespace FinanceLab.Shared.Domain.Models.Dtos;

[PublicAPI]
public sealed record TransferDto(
    string CoinCode,
    double Amount,
    DateTimeOffset OccurredAt);