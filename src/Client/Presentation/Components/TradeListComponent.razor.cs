using FinanceLab.Shared.Application.Constants;
using FinanceLab.Shared.Domain.Models.Dtos;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FinanceLab.Client.Presentation.Components;

public partial class TradeListComponent
{
    [Parameter]
    public string UserName { get; set; } = default!;

    private Task<TableData<TradeDto>> GetTableDataAsync(TableState tableState)
        => base.GetTableDataAsync(tableState, ApiRouteConstants.GetTradeList, UserName);
}