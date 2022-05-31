using FinanceLab.Shared.Application.Constants;
using FinanceLab.Shared.Domain.Models.Inputs;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FinanceLab.Client.Presentation.Components;

public partial class DepositDialogComponent
{
    private readonly TransferInput _input = new();

    [CascadingParameter]
    private MudDialogInstance MudDialog { get; set; } = default!;

    private async Task SubmitAsync()
    {
        var response = await HttpClientService.PostAsync(ApiRouteConstants.PostTransfer, _input);

        if (response.IsSuccessful)
        {
            NavigationManager.NavigateTo(NavigationManager.Uri, true);
            // Snackbar.Add(L["SuccessfulTransfer", _input.CoinCode, _input.Amount], Severity.Success);
        }
        else
            ShowProblem(response.ProblemDetails, true);
        
        MudDialog.Close(DialogResult.Ok(true));
    }
    
    private void Cancel() => MudDialog.Cancel();
}