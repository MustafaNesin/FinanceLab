using FinanceLab.Shared.Domain.Models.Enums;
using JetBrains.Annotations;
using MediatR;

namespace FinanceLab.Server.Domain.Models.Commands;

[PublicAPI]
public sealed record NewGameCommand(
    string UserName,
    GameDifficulty GameDifficulty) : IRequest;