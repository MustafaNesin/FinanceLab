using FinanceLab.Shared.Application.Abstractions;
using FinanceLab.Shared.Domain.Models.Inputs;
using FluentValidation;
using JetBrains.Annotations;

namespace FinanceLab.Shared.Application.Validators;

[UsedImplicitly]
public sealed class WalletInputValidator : BaseValidator<WalletInput>
{
    public WalletInputValidator(ISharedResources l) : base(l)
        => RuleFor(input => input.CoinCode).NotEmpty().MaximumLength(7);
}