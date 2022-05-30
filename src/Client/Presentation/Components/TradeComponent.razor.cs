using FinanceLab.Shared.Domain.Models.Enums;
using Microsoft.AspNetCore.Components;

namespace FinanceLab.Client.Presentation.Components;

public partial class TradeComponent
{
    [Parameter]
    public string? BaseCoinCode { get; set; }

    [Parameter]
    public string? QuoteCoinCode { get; set; }

    [Parameter]
    public TradeSide Side { get; set; }
}