using FinanceLab.Shared.Domain.Models.Enums;

namespace FinanceLab.Shared.Domain.Models.Inputs;

public sealed class NewGameInput
{
    public GameDifficulty GameDifficulty { get; set; }
}