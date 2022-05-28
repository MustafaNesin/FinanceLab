using FinanceLab.Shared.Application.Abstractions;
using FinanceLab.Shared.Domain.Models.Inputs;
using FluentValidation;

namespace FinanceLab.Shared.Application.Validators;

public abstract class BaseListInputValidator<T> : BaseValidator<T>
    where T : BaseListInput
{
    protected BaseListInputValidator(ISharedResources l, IEnumerable<string> sortFields, string sortFieldsCsv) : base(l)
    {
        RuleFor(input => input.Page.Current).GreaterThanOrEqualTo(0);
        RuleFor(input => input.Page.Size).GreaterThan(0);
        RuleFor(input => input.Sort.Direction).IsInEnum();

        RuleFor(input => input.Sort.By)
            .Must(sortFields.Contains)
            .WithMessage(L["SortMustBe", sortFieldsCsv])
            .When(input => input.Sort.By is not null);
    }
}