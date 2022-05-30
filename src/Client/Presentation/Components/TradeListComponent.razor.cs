using FinanceLab.Client.Domain.Models;
using FinanceLab.Shared.Application.Constants;
using FinanceLab.Shared.Domain.Models.Dtos;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FinanceLab.Client.Presentation.Components;

public partial class TradeListComponent
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

        ShowProblem(new ProblemDetails {Title = L["NotAuthorized"]}, false);
        NavigationManager.NavigateTo("/Trades");
    }

    private Task<TableData<TradeDto>> GetTableDataAsync(TableState tableState)
        => base.GetTableDataAsync(tableState, ApiRouteConstants.GetTradeList, UserName);
}