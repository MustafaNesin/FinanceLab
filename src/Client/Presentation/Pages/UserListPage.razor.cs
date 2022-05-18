using System.Collections.Specialized;
using System.ComponentModel;
using FinanceLab.Shared.Application.Constants;
using FinanceLab.Shared.Domain.Models.Dtos;
using FinanceLab.Shared.Domain.Models.Inputs;
using FinanceLab.Shared.Domain.Models.Outputs;
using JetBrains.Annotations;
using MudBlazor;

namespace FinanceLab.Client.Presentation.Pages;

[UsedImplicitly]
public partial class UserListPage
{
    private string _searchQuery = default!;
    private MudTable<UserDto> _table = default!;

    private Task OnSearch(string query)
    {
        _searchQuery = query;
        return _table.ReloadServerData();
    }

    private async Task<TableData<UserDto>> ServerReload(TableState state)
    {
        var parameters = new NameValueCollection();

        if (_searchQuery is { Length: > 0 })
            parameters.Add(nameof(UserListInput.Search), _searchQuery);

        parameters.Add(nameof(UserListInput.Sort), state.SortLabel);

        ListSortDirection? sortDirection = state.SortDirection switch
        {
            SortDirection.Ascending => ListSortDirection.Ascending,
            SortDirection.Descending => ListSortDirection.Descending,
            _ => null
        };

        if (sortDirection is not null)
            parameters.Add(nameof(UserListInput.SortDirection), ((int)sortDirection.Value).ToString());

        parameters.Add(nameof(UserListInput.Page), state.Page.ToString());
        parameters.Add(nameof(UserListInput.PageSize), state.PageSize.ToString());

        var query = string.Join('&', parameters.Cast<string>().Select(key => $"{key}={parameters[key]}"));
        var response = await HttpClientService.GetAsync<UserListOutput>($"{ApiRouteConstants.UserGetList}?{query}");

        if (response.IsSuccessful)
            return new TableData<UserDto>
            {
                TotalItems = response.Data.TotalItems,
                Items = response.Data.Items
            };

        ShowProblem(response.ProblemDetails, false);
        return new TableData<UserDto>();
    }
}