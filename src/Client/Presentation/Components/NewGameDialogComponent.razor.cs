using FinanceLab.Shared.Application.Constants;
using FinanceLab.Shared.Domain.Models.Inputs;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FinanceLab.Client.Presentation.Components;

public partial class NewGameDialogComponent
{
    private readonly NewGameInput _input = new();
    
    [CascadingParameter]
    private MudDialogInstance MudDialog { get; set; } = default!;

    private async Task SubmitAsync()
    {
        var response = await HttpClientService.PostAsync(ApiRouteConstants.NewGame, _input);

        if (response.IsSuccessful)
        {
            NavigationManager.NavigateTo(NavigationManager.Uri, true);
            // Snackbar.Add(L["SuccessfulNewGame"], Severity.Success);
        }
        else
            ShowProblem(response.ProblemDetails, true);
        
        MudDialog.Close(DialogResult.Ok(true));
    }
    
    private void Cancel() => MudDialog.Cancel();
}