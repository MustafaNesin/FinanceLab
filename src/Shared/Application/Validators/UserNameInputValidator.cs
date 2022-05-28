using FinanceLab.Shared.Application.Abstractions;
using FinanceLab.Shared.Domain.Models.Inputs;

namespace FinanceLab.Shared.Application.Validators;

public sealed class UserNameInputValidator : BaseValidator<UserNameInput>
{
    public UserNameInputValidator(ISharedResources l) : base(l)
    {
    }
}