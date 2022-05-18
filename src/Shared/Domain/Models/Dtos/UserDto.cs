namespace FinanceLab.Shared.Domain.Models.Dtos;

public record UserDto(string FirstName, string LastName, string UserName, string? Role, DateTimeOffset RegisteredAt);