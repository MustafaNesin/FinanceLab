using FinanceLab.Client.Domain.Models;
using FinanceLab.Shared.Application.Constants;
using FinanceLab.Shared.Domain.Models.Dtos;
using FinanceLab.Shared.Domain.Models.Inputs;
using FinanceLab.Shared.Domain.Models.Outputs;
using Microsoft.AspNetCore.Components;

namespace FinanceLab.Client.Presentation.Components;

public partial class MarketComponent
{
    private MarketDto? _currentMarket;
    private IReadOnlyCollection<MarketDto>? _markets;

    [Parameter]
    public string Symbol { get; set; } = default!;

    protected override async Task OnParametersSetAsync()
    {
        const string pageSizeParameterName = $"{nameof(BaseListInput.Page)}.{nameof(BaseListInput.Page.Size)}";
        const string url = $"{ApiRouteConstants.GetMarketList}?{pageSizeParameterName}=1000";

        var response = await HttpClientService.GetAsync<MarketListOutput>(url);

        if (response.IsSuccessful)
        {
            _markets = response.Data.Items;
            _currentMarket = _markets.SingleOrDefault(m => m.Symbol == Symbol);

            if (_currentMarket is null)
            {
                ShowProblem(new ProblemDetails { Title = L["MarketNotFound"] }, false);
                NavigationManager.NavigateTo("/Markets");
            }
        }
        else
        {
            ShowProblem(response.ProblemDetails, false);
            NavigationManager.NavigateTo("/Markets");
        }
    }
}