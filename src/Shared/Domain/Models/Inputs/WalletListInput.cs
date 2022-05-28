using JetBrains.Annotations;

namespace FinanceLab.Shared.Domain.Models.Inputs;

[PublicAPI]
public sealed class WalletListInput : BaseListInput
{
    public string? UserName { get; set; }
}