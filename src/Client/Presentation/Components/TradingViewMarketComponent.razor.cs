using System.Globalization;
using FinanceLab.Shared.Domain.Models.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace FinanceLab.Client.Presentation.Components;

public partial class TradingViewMarketComponent
{
    private const string ContainerId = "tradingview-market";
    private bool _secondRender;

    [Inject]
    private IJSRuntime JsRuntime { get; set; } = default!;

    [Parameter]
    public IReadOnlyCollection<MarketDto> Markets { get; set; } = default!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _secondRender = true;
            return;
        }

        if (!_secondRender)
            return;

        _secondRender = false;

        const string theme = "light";
        var symbols = Markets.Select(market => market.Symbol).ToArray();
        var locale = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
        var baseUri = NavigationManager.BaseUri;

        await JsRuntime.InvokeVoidAsync("createTradingViewMarketWidget", symbols, theme, locale, baseUri, ContainerId);
    }
}