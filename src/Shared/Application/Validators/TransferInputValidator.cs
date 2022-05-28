using FinanceLab.Shared.Application.Abstractions;
using FinanceLab.Shared.Domain.Models.Inputs;
using FluentValidation;

namespace FinanceLab.Shared.Application.Validators;

public sealed class TransferInputValidator : BaseValidator<TransferInput>
{
    public TransferInputValidator(ISharedResources l) : base(l)
    {
        RuleFor(input => input.CoinCode).NotEmpty().Length(3);
        RuleFor(input => input.Amount).GreaterThan(0d);
    }
}