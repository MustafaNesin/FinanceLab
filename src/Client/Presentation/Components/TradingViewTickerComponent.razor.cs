using System.Globalization;
using FinanceLab.Shared.Domain.Models.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace FinanceLab.Client.Presentation.Components;

public partial class TradingViewTickerComponent
{
    private const string ContainerId = "tradingview-ticker";
    private bool _isRendered;

    [Inject]
    private IJSRuntime JsRuntime { get; set; } = default!;

    [Parameter]
    public IReadOnlyCollection<MarketDto>? Markets { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (_isRendered || Markets is null)
            return;

        const string theme = "light";
        var symbols = Markets.Select(market => market.Symbol).ToArray();
        var locale = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
        var baseUri = NavigationManager.BaseUri;

        await JsRuntime.InvokeVoidAsync("createTradingViewTickerWidget", symbols, theme, locale, baseUri, ContainerId);
        _isRendered = true;
    }
}