using FinanceLab.Shared.Domain.Models.Enums;
using MediatR;

namespace FinanceLab.Server.Domain.Models.Commands;

public sealed record NewGameCommand(
    string UserName,
    GameDifficulty GameDifficulty) : IRequest;