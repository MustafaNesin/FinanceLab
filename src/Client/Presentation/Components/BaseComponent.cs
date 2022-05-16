using FinanceLab.Client.Application.Abstractions;
using FinanceLab.Client.Domain.Models;
using FinanceLab.Shared.Application.Abstractions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace FinanceLab.Client.Presentation.Components;

public class BaseComponent : ComponentBase, IAsyncDisposable
{
    [CascadingParameter]
    protected Task<AuthenticationState> AuthenticationStateTask { get; set; } = default!;

    [Inject]
    protected IDialogService DialogService { get; private set; } = default!;

    [Inject]
    protected IHttpClientService HttpClientService { get; private set; } = default!;

    [Inject]
    protected ISharedResources L { get; private set; } = default!;

    [Inject]
    protected NavigationManager NavigationManager { get; set; } = default!;

    [Inject]
    protected ISnackbar Snackbar { get; private set; } = default!;

    [Inject]
    protected IStateContainerService StateContainer { get; private set; } = default!;

    public virtual ValueTask DisposeAsync()
    {
        StateContainer.StateHasChanged -= StateHasChanged;
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    protected override void OnInitialized() => StateContainer.StateHasChanged += StateHasChanged;

    protected void ShowProblem(ProblemDetails problemDetails, bool showDialog)
    {
        if (showDialog)
            ShowProblemDialog(problemDetails);

        Snackbar.Add(problemDetails.Title ?? "Error", Severity.Error, options =>
            options.Onclick = _ =>
            {
                ShowProblemDialog(problemDetails);
                return Task.CompletedTask;
            });
    }

    private void ShowProblemDialog(ProblemDetails problemDetails)
    {
        var options = new DialogOptions { CloseOnEscapeKey = true };
        var parameters = new DialogParameters { [nameof(ProblemDialogComponent.ProblemDetails)] = problemDetails };

        DialogService.Show<ProblemDialogComponent>("Error", parameters, options);
    }
}