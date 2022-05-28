using JetBrains.Annotations;

namespace FinanceLab.Server.Infrastructure.Models;

[PublicAPI]
public record TickerPrice(string Symbol, double Price);