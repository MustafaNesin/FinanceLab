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
    private readonly SignUpInputValidator _validator = new();
    private MudForm _form = default!;
    private bool _isSubmitDisabled = true;

    protected override Task OnInitializedAsync()
    {
        _isSubmitDisabled = false;
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

        var response = await HttpClientService.PostAsync(ApiRouteConstants.UserSignUp, _input);

        if (response.IsSuccessful)
        {
            NavigationManager.NavigateTo("/SignIn?UserName=" + _input.UserName);
            Snackbar.Add("Successfully registered!", Severity.Success);
        }
        else
            ShowProblem(response.ProblemDetails, false);

        _isSubmitDisabled = false;
    }
}