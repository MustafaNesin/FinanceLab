using FinanceLab.Shared.Domain.Models.Dtos;
using JetBrains.Annotations;

namespace FinanceLab.Shared.Domain.Models.Outputs;

[PublicAPI]
public sealed class TransferListOutput : BaseListOutput<TransferDto>
{
    public TransferListOutput(IReadOnlyCollection<TransferDto> items, int total) : base(items, total)
    {
    }
}