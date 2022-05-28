using JetBrains.Annotations;

namespace FinanceLab.Shared.Domain.Models.Inputs;

[PublicAPI]
public sealed class UserNameInput
{
    public string? UserName { get; set; }
}