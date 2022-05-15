namespace FinanceLab.Client.Presentation.Components;

public partial class RedirectToSignInComponent
{
    protected override Task OnInitializedAsync()
    {
        NavigationManager.NavigateTo("/SignIn");
        return Task.CompletedTask;
    }
}