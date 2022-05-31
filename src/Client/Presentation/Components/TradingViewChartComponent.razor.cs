using System.Globalization;
using FinanceLab.Shared.Domain.Models.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace FinanceLab.Client.Presentation.Components;

public partial class TradingViewChartComponent
{
    private const string ContainerId = "tradingview-chart";
    private bool _isRendered;

    [Inject]
    private IJSRuntime JsRuntime { get; set; } = default!;

    [Parameter]
    public MarketDto? Market { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (_isRendered || Market is null)
            return;

        const string theme = "light";
        var locale = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

        await JsRuntime.InvokeVoidAsync("createTradingViewChartWidget", Market.Symbol, theme, locale, ContainerId);
        _isRendered = true;
    }
}