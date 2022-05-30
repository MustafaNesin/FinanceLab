using FinanceLab.Client.Application.Abstractions;
using FinanceLab.Shared.Application.Constants;
using FinanceLab.Shared.Application.Validators;
using FinanceLab.Shared.Domain.Models.Dtos;
using FinanceLab.Shared.Domain.Models.Inputs;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FinanceLab.Client.Presentation.Pages;

[UsedImplicitly]
public partial class SignInPage
{
    private readonly SignInInput _input = new();
    private MudForm _form = default!;
    private bool _isSubmitDisabled;
    private SignInInputValidator _validator = default!;

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
        _validator = new SignInInputValidator(L);
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

        var response = await HttpClientService.PostAsync<SignInInput, UserDto>(ApiRouteConstants.SignIn, _input);

        if (response.IsSuccessful)
        {
            AuthenticationStateProvider.SetAuthenticationState(response.Data);
            Snackbar.Add(L["SuccessfulSignIn"], Severity.Success);
        }
        else
            ShowProblem(response.ProblemDetails, false);

        _isSubmitDisabled = false;
    }
}