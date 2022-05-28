using JetBrains.Annotations;

namespace FinanceLab.Shared.Domain.Models.Inputs;

[PublicAPI]
public sealed class TransferListInput : BaseListInput
{
    public string UserName { get; set; } = default!;
}