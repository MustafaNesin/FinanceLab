using Microsoft.AspNetCore.Components;

namespace FinanceLab.Client.Presentation.Pages;

public partial class MarketPage
{
    [Parameter]
    public string Symbol { get; set; } = default!;
}