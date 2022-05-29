using FinanceLab.Shared.Domain.Models.Enums;
using JetBrains.Annotations;
using MediatR;

namespace FinanceLab.Server.Domain.Models.Commands;

[PublicAPI]
public sealed record TradeCommand(
    string UserName,
    TradeSide Side,
    string BaseCoinCode,
    string QuoteCoinCode,
    double Quantity,
    double Price) : IRequest;