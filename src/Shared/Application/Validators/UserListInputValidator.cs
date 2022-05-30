using FinanceLab.Shared.Application.Abstractions;
using FinanceLab.Shared.Domain.Models.Dtos;
using FinanceLab.Shared.Domain.Models.Inputs;
using JetBrains.Annotations;

namespace FinanceLab.Shared.Application.Validators;

[UsedImplicitly]
public sealed class UserListInputValidator : BaseListInputValidator<UserListInput>
{
    private static readonly string[] SortFields =
    {
        nameof(UserDto.UserName), nameof(UserDto.FirstName), nameof(UserDto.LastName), nameof(UserDto.Role),
        nameof(UserDto.RegisteredAt), nameof(UserDto.GameDifficulty), nameof(UserDto.GameRestartedAt)
    };

    private static readonly string SortFieldsCsv = string.Join(", ", SortFields);

    public UserListInputValidator(ISharedResources l) : base(l, SortFields, SortFieldsCsv)
    {
    }
}