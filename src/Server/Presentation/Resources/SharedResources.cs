using FinanceLab.Shared.Presentation.Resources;
using Microsoft.Extensions.Localization;

namespace FinanceLab.Server.Presentation.Resources;

public sealed class SharedResources : BaseSharedResources<SharedResources>
{
    public SharedResources(IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
    {
    }
}