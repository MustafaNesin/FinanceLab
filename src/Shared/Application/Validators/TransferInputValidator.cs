using FinanceLab.Shared.Application.Abstractions;
using FinanceLab.Shared.Domain.Models.Inputs;
using FluentValidation;
using JetBrains.Annotations;

namespace FinanceLab.Shared.Application.Validators;

[UsedImplicitly]
public sealed class TransferInputValidator : BaseValidator<TransferInput>
{
    public TransferInputValidator(ISharedResources l) : base(l)
    {
        RuleFor(input => input.CoinCode).NotEmpty().MaximumLength(7);
        RuleFor(input => input.Amount).GreaterThan(0d);
    }
}