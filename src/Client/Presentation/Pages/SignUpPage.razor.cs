using FinanceLab.Shared.Application.Constants;
using FinanceLab.Shared.Application.Validators;
using FinanceLab.Shared.Domain.Models.Inputs;
using JetBrains.Annotations;
using MudBlazor;

namespace FinanceLab.Client.Presentation.Pages;

[UsedImplicitly]
public partial class SignUpPage
{
    private readonly SignUpInput _input = new();
    private MudForm _form = default!;
    private bool _isSubmitDisabled = true;
    private SignUpInputValidator _validator = default!;

    protected override Task OnParametersSetAsync()
    {
        _isSubmitDisabled = false;
        _validator = new SignUpInputValidator(L);
        return Task.CompletedTask;
    }

    private async Task SubmitAsync()
    {
        _isSubmitDisabled = true;
        await _form.Validate();

        if (!_form.IsValid)
        {
            _isSubmitDisabled = false;
            return;
        }

        var response = await HttpClientService.PostAsync(ApiRouteConstants.SignUp, _input);

        if (response.IsSuccessful)
        {
            NavigationManager.NavigateTo("/SignIn?UserName=" + _input.UserName);
            Snackbar.Add(L["SuccessfulSignUp"], Severity.Success);
        }
        else
            ShowProblem(response.ProblemDetails, false);

        _isSubmitDisabled = false;
    }
}