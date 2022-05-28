using JetBrains.Annotations;

namespace FinanceLab.Server.Infrastructure.Models;

[PublicAPI]
public sealed record TickerPrice(
    string Symbol,
    double Price);