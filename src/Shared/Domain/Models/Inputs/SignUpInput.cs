namespace FinanceLab.Shared.Domain.Models.Inputs;

public sealed class SignUpInput
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string PasswordRepeat { get; set; } = default!;
}