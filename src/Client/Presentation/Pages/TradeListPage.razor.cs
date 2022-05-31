using FinanceLab.Client.Domain.Models;
using FinanceLab.Shared.Application.Constants;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;

namespace FinanceLab.Client.Presentation.Pages;

[UsedImplicitly]
public partial class TradeListPage
{
    [Parameter]
    public string UserName { get; set; } = default!;

    protected override void OnParametersSet()
    {
        // ReSharper disable once NullCoalescingConditionIsAlwaysNotNullAccordingToAPIContract
        UserName ??= StateContainer.User?.UserName!;

        if (StateContainer.User?.UserName == UserName ||
            StateContainer.User?.Role is RoleConstants.Admin)
            return;

        ShowProblem(new ProblemDetails { Title = L["NotAuthorized"] }, false);
        NavigationManager.NavigateTo("/Trades");
    }
}