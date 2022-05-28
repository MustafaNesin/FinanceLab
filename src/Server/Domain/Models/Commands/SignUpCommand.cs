using FinanceLab.Shared.Domain.Models.Enums;
using MediatR;

namespace FinanceLab.Server.Domain.Models.Commands;

public sealed record SignUpCommand(
    string UserName,
    string FirstName,
    string LastName,
    string Password,
    GameDifficulty GameDifficulty) : IRequest;