using FinanceLab.Shared.Application.Abstractions;
using FinanceLab.Shared.Domain.Models.Dtos;
using FinanceLab.Shared.Domain.Models.Inputs;
using FluentValidation;
using JetBrains.Annotations;

namespace FinanceLab.Shared.Application.Validators;

[UsedImplicitly]
public sealed class TransferListInputValidator : BaseListInputValidator<TransferListInput>
{
    private static readonly string[] SortFields =
    {
        nameof(TransferDto.CoinCode), nameof(TransferDto.Amount), nameof(TransferDto.OccurredAt)
    };

    private static readonly string SortFieldsCsv = string.Join(", ", SortFields);

    public TransferListInputValidator(ISharedResources l) : base(l, SortFields, SortFieldsCsv)
        => RuleFor(input => input.UserName).NotEmpty();
}