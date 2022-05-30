using FinanceLab.Shared.Application.Abstractions;
using FinanceLab.Shared.Domain.Models.Inputs;
using JetBrains.Annotations;

namespace FinanceLab.Shared.Application.Validators;

[UsedImplicitly]
public sealed class UserNameInputValidator : BaseValidator<UserNameInput>
{
    public UserNameInputValidator(ISharedResources l) : base(l)
    {
    }
}