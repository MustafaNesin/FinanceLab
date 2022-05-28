using FinanceLab.Shared.Domain.Models.Dtos;
using JetBrains.Annotations;

namespace FinanceLab.Shared.Domain.Models.Outputs;

[PublicAPI]
public sealed class TradeListOutput : BaseListOutput<TradeDto>
{
    public TradeListOutput(IReadOnlyCollection<TradeDto> items, int total) : base(items, total)
    {
    }
}