using FinanceLab.Shared.Domain.Models.Enums;

namespace FinanceLab.Shared.Domain.Models.Inputs;

public sealed class SignUpInput
{
    public string UserName { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string PasswordRepeat { get; set; } = default!;
    public GameDifficulty GameDifficulty { get; set; } = GameDifficulty.Normal;
}