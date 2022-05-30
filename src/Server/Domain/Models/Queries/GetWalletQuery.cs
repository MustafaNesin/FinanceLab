using FinanceLab.Shared.Domain.Models.Dtos;
using JetBrains.Annotations;
using MediatR;

namespace FinanceLab.Server.Domain.Models.Queries;

[PublicAPI]
public sealed record GetWalletQuery(
    string UserName, 
    string CoinCode) : IRequest<WalletDto>;