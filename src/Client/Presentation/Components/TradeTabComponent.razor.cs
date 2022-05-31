using FinanceLab.Shared.Domain.Models.Dtos;
using FinanceLab.Shared.Domain.Models.Enums;
using FinanceLab.Shared.Domain.Models.Inputs;
using Microsoft.AspNetCore.Components;

namespace FinanceLab.Client.Presentation.Components;

public partial class TradeTabComponent
{
    private readonly TradeInput _input = new();
    private bool _isSubmitDisabled = true;

    [Parameter]
    public string BaseCoinCode { get; set; } = default!;

    [Parameter]
    public string QuoteCoinCode { get; set; } = default!;

    [Parameter]
    public TradeSide Side { get; set; }

    [Parameter]
    public WalletDto Wallet { get; set; } = default!;

    [Parameter]
    public EventCallback<TradeInput> Trade { get; set; }

    protected override void OnInitialized()
    {
        _input.BaseCoinCode = BaseCoinCode;
        _input.QuoteCoinCode = QuoteCoinCode;
        _input.Side = Side;
        _isSubmitDisabled = false;
    }

    private async Task SubmitAsync()
    {
        _isSubmitDisabled = true;
        await Trade.InvokeAsync(_input);
        _isSubmitDisabled = false;
    }
}