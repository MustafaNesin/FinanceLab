using FinanceLab.Shared.Domain.Models.Dtos;
using JetBrains.Annotations;

namespace FinanceLab.Shared.Domain.Models.Outputs;

[PublicAPI]
public sealed class WalletListOutput : BaseListOutput<WalletDto>
{
    public WalletListOutput(IReadOnlyCollection<WalletDto> items, int total) : base(items, total)
    {
    }
}