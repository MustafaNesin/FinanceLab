using FinanceLab.Client.Application.Abstractions;
using FinanceLab.Shared.Application.Constants;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FinanceLab.Client.Presentation.Pages;

[UsedImplicitly]
public partial class SignOutPage
{
    [Inject]
    private IHostAuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        var response = await HttpClientService.PostAsync(ApiRouteConstants.UserSignOut);

        if (response.IsSuccessful)
        {
            AuthenticationStateProvider.RenewAuthenticationState();
            Snackbar.Add("Signed out successfully!", Severity.Success);
        }
        else
            ShowProblem(response.ProblemDetails, false);

        NavigationManager.NavigateTo("/");
    }
}