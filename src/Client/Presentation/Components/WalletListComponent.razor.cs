using FinanceLab.Shared.Application.Constants;
using FinanceLab.Shared.Domain.Models.Dtos;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FinanceLab.Client.Presentation.Components;

public partial class WalletListComponent
{
    [Parameter]
    public string? UserName { get; set; }

    private Task<TableData<WalletDto>> GetTableDataAsync(TableState tableState)
        => base.GetTableDataAsync(tableState, ApiRouteConstants.GetWalletList, UserName);
}