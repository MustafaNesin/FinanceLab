using FinanceLab.Shared.Application.Constants;
using FinanceLab.Shared.Domain.Models.Dtos;
using FinanceLab.Shared.Domain.Models.Enums;
using FinanceLab.Shared.Domain.Models.Inputs;
using FinanceLab.Shared.Domain.Models.Outputs;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FinanceLab.Client.Presentation.Components;

public partial class TradeComponent
{
    private WalletDto _baseWallet = default!;
    private WalletDto _quoteWallet = default!;

    [Parameter]
    public string BaseCoinCode { get; set; } = default!;

    [Parameter]
    public string QuoteCoinCode { get; set; } = default!;

    protected override async Task OnParametersSetAsync()
    {
        var baseWalletRequestUri = $"{ApiRouteConstants.GetWallet}?coinCode={BaseCoinCode}";
        var baseResponse = await HttpClientService.GetAsync<WalletDto>(baseWalletRequestUri);

        if (baseResponse.IsSuccessful)
        {
            var quoteWalletRequestUri = $"{ApiRouteConstants.GetWallet}?coinCode={QuoteCoinCode}";
            var quoteResponse = await HttpClientService.GetAsync<WalletDto>(quoteWalletRequestUri);

            if (quoteResponse.IsSuccessful)
            {
                _baseWallet = baseResponse.Data;
                _quoteWallet = quoteResponse.Data;
            }
            else
                ShowProblem(quoteResponse.ProblemDetails, false);
        }
        else
            ShowProblem(baseResponse.ProblemDetails, false);
    }

    private async Task TradeAsync(TradeInput input)
    {
        var response = await HttpClientService.PostAsync<TradeInput, TradeOutput>(ApiRouteConstants.PostTrade, input);

        if (response.IsSuccessful)
        {
            _baseWallet = response.Data.BaseWallet;
            _quoteWallet = response.Data.QuoteWallet;

            var lKey = input.Side == TradeSide.Buy ? "SuccessfulBuy" : "SuccessfulSell";
            Snackbar.Add(L[lKey, BaseCoinCode, input.Quantity, response.Data.Price], Severity.Success);
        }
        else
            ShowProblem(response.ProblemDetails, false);
    }
}