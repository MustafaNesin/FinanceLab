using FinanceLab.Shared.Domain.Models.Dtos;
using JetBrains.Annotations;

namespace FinanceLab.Shared.Domain.Models.Outputs;

[PublicAPI]
public record UserListOutput(
    IReadOnlyCollection<UserDto> Items,
    int TotalItems);