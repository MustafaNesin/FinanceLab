using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;

namespace FinanceLab.Client.Presentation.Pages;

[UsedImplicitly]
public partial class UserPage
{
    [Parameter]
    public string UserName { get; set; } = default!;

    protected override void OnParametersSet()
    {
        UserName ??= StateContainer.User?.UserName!;
    }
}