using FinanceLab.Shared.Domain.Models.Dtos;

namespace FinanceLab.Shared.Domain.Models.Outputs;

public record UserListOutput(IReadOnlyCollection<UserDto> Items, int TotalItems);