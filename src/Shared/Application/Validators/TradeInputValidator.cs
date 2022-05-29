using FinanceLab.Shared.Application.Abstractions;
using FinanceLab.Shared.Domain.Models.Inputs;
using FluentValidation;

namespace FinanceLab.Shared.Application.Validators;

public sealed class TradeInputValidator : BaseValidator<TradeInput>
{
    public TradeInputValidator(ISharedResources l) : base(l)
    {
        RuleFor(input => input.Side).IsInEnum();
        RuleFor(input => input.BaseCoinCode).NotEmpty().MaximumLength(7);
        RuleFor(input => input.QuoteCoinCode).NotEmpty().MaximumLength(7);
        RuleFor(input => input.Quantity).GreaterThan(0d);
    }
}