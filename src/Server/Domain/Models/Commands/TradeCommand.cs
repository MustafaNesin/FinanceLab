using FinanceLab.Shared.Domain.Models.Enums;
using JetBrains.Annotations;
using MediatR;

namespace FinanceLab.Server.Domain.Models.Commands;

[PublicAPI]
public sealed record TradeCommand(
    TradeSide Side,
    string BaseCoinCode,
    string QuoteCoinCode,
    double Amount) : IRequest;