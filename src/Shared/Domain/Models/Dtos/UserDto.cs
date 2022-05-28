using FinanceLab.Shared.Domain.Models.Enums;
using JetBrains.Annotations;

namespace FinanceLab.Shared.Domain.Models.Dtos;

[PublicAPI]
public record UserDto(
    string UserName,
    string FirstName,
    string LastName,
    string? Role,
    DateTimeOffset RegisteredAt,
    GameDifficulty GameDifficulty,
    DateTimeOffset GameRestartedAt);