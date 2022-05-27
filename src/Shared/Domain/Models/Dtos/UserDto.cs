using JetBrains.Annotations;

namespace FinanceLab.Shared.Domain.Models.Dtos;

[PublicAPI]
public record UserDto(string FirstName, string LastName, string UserName, string? Role, DateTimeOffset RegisteredAt);