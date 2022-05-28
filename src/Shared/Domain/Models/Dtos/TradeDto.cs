using FinanceLab.Shared.Domain.Models.Enums;
using JetBrains.Annotations;

namespace FinanceLab.Shared.Domain.Models.Dtos;

[PublicAPI]
public sealed record TradeDto(
    TradeSide Side,
    string BaseCoinCode,
    string QuoteCoinCode,
    double Amount,
    double Price,
    DateTimeOffset OccurredAt);