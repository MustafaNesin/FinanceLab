using FinanceLab.Shared.Application.Abstractions;
using FinanceLab.Shared.Domain.Models.Dtos;
using FinanceLab.Shared.Domain.Models.Inputs;
using JetBrains.Annotations;

namespace FinanceLab.Shared.Application.Validators;

[UsedImplicitly]
public sealed class WalletListInputValidator : BaseListInputValidator<WalletListInput>
{
    private static readonly string[] SortFields =
    {
        nameof(WalletDto.CoinCode), nameof(WalletDto.Amount)
    };

    private static readonly string SortFieldsCsv = string.Join(", ", SortFields);

    public WalletListInputValidator(ISharedResources l) : base(l, SortFields, SortFieldsCsv)
    {
    }
}