﻿using FinanceLab.Shared.Domain.Models.Inputs;
using FluentValidation;

namespace FinanceLab.Shared.Application.Validators;

public class SignUpInputValidator : BaseValidator<SignUpInput>
{
    public SignUpInputValidator()
    {
        RuleFor(input => input.FirstName).NotEmpty();
        RuleFor(input => input.LastName).NotEmpty();
        RuleFor(input => input.UserName).NotEmpty();
        RuleFor(input => input.Password).NotEmpty().MinimumLength(6);
        RuleFor(input => input.PasswordRepeat).Equal(input => input.Password).WithMessage("Passwords do not match.");
    }
}