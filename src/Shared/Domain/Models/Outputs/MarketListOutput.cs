using FinanceLab.Shared.Domain.Models.Dtos;
using JetBrains.Annotations;

namespace FinanceLab.Shared.Domain.Models.Outputs;

[PublicAPI]
public sealed class MarketListOutput : BaseListOutput<MarketDto>
{
    public MarketListOutput(IReadOnlyCollection<MarketDto> items, int total) : base(items, total)
    {
    }
}