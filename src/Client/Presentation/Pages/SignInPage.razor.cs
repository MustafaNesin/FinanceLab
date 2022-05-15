using FinanceLab.Client.Application.Abstractions;
using FinanceLab.Shared.Application.Constants;
using FinanceLab.Shared.Application.Validators;
using FinanceLab.Shared.Domain.Models.Inputs;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FinanceLab.Client.Presentation.Pages;

[UsedImplicitly]
public partial class SignInPage
{
    private readonly SignInInput _input = new();
    private readonly SignInInputValidator _validator = new();
    private MudForm _form = default!;
    private bool _isSubmitDisabled;

    [Parameter]
    [SupplyParameterFromQuery]
    public string? UserName { get; set; }

    [Inject]
    private IHostAuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

    protected override async Task OnParametersSetAsync()
    {
        var authenticationState = await AuthenticationStateTask;

        if (authenticationState.User.Identity?.IsAuthenticated == true)
            NavigationManager.NavigateTo("/");

        _input.UserName = UserName!;
    }

    private async Task OnSubmitAsync()
    {
        _isSubmitDisabled = true;
        await _form.Validate();

        if (!_form.IsValid)
        {
            _isSubmitDisabled = false;
            return;
        }

        var signInResponse = await HttpClientService.PostAsync(ApiRouteConstants.UserSignIn, _input);

        if (signInResponse.IsSuccessful)
        {
            AuthenticationStateProvider.RenewAuthenticationState();
            Snackbar.Add("Signed in successfully!", Severity.Success);
        }
        else
            ShowProblem(signInResponse.ProblemDetails, false);

        _isSubmitDisabled = false;
    }
}