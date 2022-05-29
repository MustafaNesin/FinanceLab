using System.Collections.Specialized;
using System.ComponentModel;
using FinanceLab.Shared.Domain.Models.Inputs;
using FinanceLab.Shared.Domain.Models.Outputs;
using MudBlazor;

namespace FinanceLab.Client.Presentation.Components;

public abstract class BaseListComponent<TDto, TOutput> : BaseComponent
    where TOutput : BaseListOutput<TDto>
{
    private string? _filter;
    protected MudTable<TDto>? Table;

    protected Task OnSearch(string filter)
    {
        if (Table is null)
            return Task.CompletedTask;

        _filter = filter;
        return Table.ReloadServerData();
    }

    protected async Task<TableData<TDto>> GetTableDataAsync(
        TableState tableState, string endpointRoute, string? userName = default)
    {
        const string filterParameterName = nameof(BaseListInput.Filter);

        const string currentPageParameterName =
            $"{nameof(BaseListInput.Page)}.{nameof(BaseListInput.Page.Current)}";

        const string pageSizeParameterName =
            $"{nameof(BaseListInput.Page)}.{nameof(BaseListInput.Page.Size)}";

        const string sortByParameterName =
            $"{nameof(BaseListInput.Sort)}.{nameof(BaseListInput.Sort.By)}";

        const string sortDirectionParameterName =
            $"{nameof(BaseListInput.Sort)}.{nameof(BaseListInput.Sort.Direction)}";

        var parameters = new NameValueCollection();

        if (_filter is { Length: > 0 })
            parameters.Add(filterParameterName, _filter);

        parameters.Add(sortByParameterName, tableState.SortLabel);

        var sortDirection = tableState.SortDirection switch
        {
            SortDirection.Ascending => ListSortDirection.Ascending,
            SortDirection.Descending => ListSortDirection.Descending,
            _ => ListSortDirection.Ascending
        };

        parameters.Add(sortDirectionParameterName, ((int)sortDirection).ToString());
        parameters.Add(currentPageParameterName, tableState.Page.ToString());
        parameters.Add(pageSizeParameterName, tableState.PageSize.ToString());

        if (userName is { Length: > 0 })
            parameters.Add(nameof(userName), userName);

        var query = string.Join('&', parameters.Cast<string>().Select(key => $"{key}={parameters[key]}"));
        var endpointUrl = $"{endpointRoute}?{query}";
        var response = await HttpClientService.GetAsync<TOutput>(endpointUrl);

        if (response.IsSuccessful)
            return new TableData<TDto>
            {
                TotalItems = response.Data.Total,
                Items = response.Data.Items
            };

        ShowProblem(response.ProblemDetails, false);
        return new TableData<TDto>();
    }
}