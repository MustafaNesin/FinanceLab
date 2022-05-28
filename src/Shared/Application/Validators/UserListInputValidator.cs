using FinanceLab.Shared.Application.Abstractions;
using FinanceLab.Shared.Domain.Models.Dtos;
using FinanceLab.Shared.Domain.Models.Inputs;
using FluentValidation;
using JetBrains.Annotations;

namespace FinanceLab.Shared.Application.Validators;

[PublicAPI]
public class UserListInputValidator : BaseValidator<UserListInput>
{
    private static readonly string[] SortFields =
    {
        nameof(UserDto.UserName), nameof(UserDto.FirstName), nameof(UserDto.LastName), nameof(UserDto.Role),
        nameof(UserDto.RegisteredAt), nameof(UserDto.GameDifficulty), nameof(UserDto.GameRestartedAt)
    };

    private static readonly string SortFieldsCsv = string.Join(", ", SortFields);

    public UserListInputValidator(ISharedResources l) : base(l)
    {
        RuleFor(input => input.Page).GreaterThanOrEqualTo(0);
        RuleFor(input => input.PageSize).GreaterThan(0);
        RuleFor(input => input.SortDirection).IsInEnum();

        RuleFor(input => input.Sort)
            .Must(p => SortFields.Contains(p))
            .WithMessage(L["SortMustBe", SortFieldsCsv])
            .When(input => input.Sort is not null);
    }
}