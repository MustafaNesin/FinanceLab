using FinanceLab.Shared.Application.Abstractions;
using FinanceLab.Shared.Domain.Models.Inputs;
using FluentValidation;
using JetBrains.Annotations;

namespace FinanceLab.Shared.Application.Validators;

[UsedImplicitly]
public sealed class SignInInputValidator : BaseValidator<SignInInput>
{
    public SignInInputValidator(ISharedResources l) : base(l)
    {
        RuleFor(input => input.UserName).NotEmpty();
        RuleFor(input => input.Password).NotEmpty();
    }
}