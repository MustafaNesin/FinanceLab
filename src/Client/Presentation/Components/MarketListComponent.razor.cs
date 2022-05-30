using FinanceLab.Shared.Application.Constants;
using FinanceLab.Shared.Domain.Models.Dtos;
using FinanceLab.Shared.Domain.Models.Inputs;
using FinanceLab.Shared.Domain.Models.Outputs;

namespace FinanceLab.Client.Presentation.Components;

public partial class MarketListComponent
{
    private IReadOnlyCollection<MarketDto> _markets = default!;

    protected override async Task OnParametersSetAsync()
    {
        const string pageSizeParameterName = $"{nameof(BaseListInput.Page)}.{nameof(BaseListInput.Page.Size)}";
        const string url = $"{ApiRouteConstants.GetMarketList}?{pageSizeParameterName}=1000";

        var response = await HttpClientService.GetAsync<MarketListOutput>(url);

        if (response.IsSuccessful)
            _markets = response.Data.Items;
        else
        {
            _markets = Array.Empty<MarketDto>();
            ShowProblem(response.ProblemDetails, false);
        }
    }
}