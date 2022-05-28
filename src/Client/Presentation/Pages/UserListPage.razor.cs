using FinanceLab.Shared.Application.Constants;
using FinanceLab.Shared.Domain.Models.Dtos;
using JetBrains.Annotations;
using MudBlazor;

namespace FinanceLab.Client.Presentation.Pages;

[UsedImplicitly]
public partial class UserListPage
{
    private Task<TableData<UserDto>> GetTableDataAsync(TableState tableState)
        => base.GetTableDataAsync(tableState, ApiRouteConstants.GetUserList);
}