using JetBrains.Annotations;
using MediatR;

namespace FinanceLab.Server.Domain.Models.Commands;

[PublicAPI]
public sealed record TransferCommand(
    string CoinCode,
    double Amount) : IRequest;