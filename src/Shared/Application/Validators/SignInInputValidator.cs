using FinanceLab.Shared.Domain.Models.Inputs;
using FluentValidation;

namespace FinanceLab.Shared.Application.Validators;

public class SignInInputValidator : BaseValidator<SignInInput>
{
    public SignInInputValidator()
    {
        RuleFor(input => input.UserName).NotEmpty();
        RuleFor(input => input.Password).NotEmpty();
    }
}