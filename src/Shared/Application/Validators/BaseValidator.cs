using FluentValidation;

namespace FinanceLab.Shared.Application.Validators;

public abstract class BaseValidator<T> : AbstractValidator<T>
{
    public async Task<IEnumerable<string>> ValidatePropertyAsync(object model, string propertyName)
    {
        var context = ValidationContext<T>.CreateWithOptions((T)model, x => x.IncludeProperties(propertyName));
        var result = await ValidateAsync(context);
        return result.IsValid ? Array.Empty<string>() : result.Errors.Select(e => e.ErrorMessage);
    }
}