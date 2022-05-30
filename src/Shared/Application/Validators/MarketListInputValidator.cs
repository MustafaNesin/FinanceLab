using FinanceLab.Shared.Application.Abstractions;
using FinanceLab.Shared.Domain.Models.Dtos;
using FinanceLab.Shared.Domain.Models.Inputs;
using JetBrains.Annotations;

namespace FinanceLab.Shared.Application.Validators;

[UsedImplicitly]
public sealed class MarketListInputValidator : BaseListInputValidator<MarketListInput>
{
    private static readonly string[] SortFields =
    {
        nameof(MarketDto.BaseCoinCode), nameof(MarketDto.QuoteCoinCode)
    };

    private static readonly string SortFieldsCsv = string.Join(", ", SortFields);

    public MarketListInputValidator(ISharedResources l) : base(l, SortFields, SortFieldsCsv)
    {
    }
}