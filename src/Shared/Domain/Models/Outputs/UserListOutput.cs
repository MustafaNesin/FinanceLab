using FinanceLab.Shared.Domain.Models.Dtos;
using JetBrains.Annotations;

namespace FinanceLab.Shared.Domain.Models.Outputs;

[PublicAPI]
public sealed class UserListOutput : BaseListOutput<UserDto>
{
    public UserListOutput(IReadOnlyCollection<UserDto> items, int total) : base(items, total)
    {
    }
}