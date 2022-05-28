using FinanceLab.Shared.Application.Abstractions;
using FinanceLab.Shared.Domain.Models.Inputs;
using FluentValidation;

namespace FinanceLab.Shared.Application.Validators;

public sealed class TradeInputValidator : BaseValidator<TradeInput>
{
    public TradeInputValidator(ISharedResources l) : base(l)
    {
        RuleFor(input => input.Side).IsInEnum();
        RuleFor(input => input.BaseCoinCode).NotEmpty().Length(3);
        RuleFor(input => input.QuoteCoinCode).NotEmpty().Length(3);
        RuleFor(input => input.Amount).GreaterThan(0d);
    }
}