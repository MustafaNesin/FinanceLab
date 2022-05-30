using FinanceLab.Shared.Application.Abstractions;
using FinanceLab.Shared.Domain.Models.Inputs;
using FluentValidation;
using JetBrains.Annotations;

namespace FinanceLab.Shared.Application.Validators;

[UsedImplicitly]
public sealed class SignUpInputValidator : BaseValidator<SignUpInput>
{
    public SignUpInputValidator(ISharedResources l) : base(l)
    {
        RuleFor(input => input.FirstName).NotEmpty();
        RuleFor(input => input.LastName).NotEmpty();
        RuleFor(input => input.UserName).NotEmpty();
        RuleFor(input => input.Password).NotEmpty().MinimumLength(6);
        RuleFor(input => input.PasswordRepeat).Equal(input => input.Password).WithMessage("Passwords do not match.");
        RuleFor(input => input.GameDifficulty).IsInEnum();
    }
}