using FinanceLab.Shared.Application.Abstractions;
using Microsoft.Extensions.Localization;

namespace FinanceLab.Shared.Presentation.Resources;

public abstract class BaseSharedResources<TSharedResources> : ISharedResources
{
    private readonly IStringLocalizer<TSharedResources> _stringLocalizer;

    protected BaseSharedResources(IStringLocalizer<TSharedResources> stringLocalizer)
        => _stringLocalizer = stringLocalizer;

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        => _stringLocalizer.GetAllStrings(includeParentCultures);

    public LocalizedString this[string name] => _stringLocalizer[name];

    public LocalizedString this[string name, params object[] arguments] => _stringLocalizer[name, arguments];
}