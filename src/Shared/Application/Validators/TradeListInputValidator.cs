using FinanceLab.Shared.Application.Abstractions;
using FinanceLab.Shared.Domain.Models.Dtos;
using FinanceLab.Shared.Domain.Models.Inputs;
using FluentValidation;
using JetBrains.Annotations;

namespace FinanceLab.Shared.Application.Validators;

[UsedImplicitly]
public sealed class TradeListInputValidator : BaseListInputValidator<TradeListInput>
{
    private static readonly string[] SortFields =
    {
        nameof(TradeDto.Side), nameof(TradeDto.BaseCoinCode), nameof(TradeDto.QuoteCoinCode),
        nameof(TradeDto.Amount), nameof(TradeDto.Price), nameof(TradeDto.OccurredAt)
    };

    private static readonly string SortFieldsCsv = string.Join(", ", SortFields);

    public TradeListInputValidator(ISharedResources l) : base(l, SortFields, SortFieldsCsv)
        => RuleFor(input => input.UserName).NotEmpty();
}