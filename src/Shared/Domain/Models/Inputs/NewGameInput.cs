using FinanceLab.Shared.Domain.Models.Enums;
using JetBrains.Annotations;

namespace FinanceLab.Shared.Domain.Models.Inputs;

[PublicAPI]
public sealed class NewGameInput
{
    public GameDifficulty GameDifficulty { get; set; }
}