using JetBrains.Annotations;

namespace FinanceLab.Shared.Domain.Models.Inputs;

[PublicAPI]
public sealed class TradeListInput : BaseListInput
{
    public string UserName { get; set; } = default!;
}