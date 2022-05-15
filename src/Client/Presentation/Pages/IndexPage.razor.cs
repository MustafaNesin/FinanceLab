using System.Security.Claims;
using JetBrains.Annotations;

namespace FinanceLab.Client.Presentation.Pages;

[UsedImplicitly]
public partial class IndexPage
{
    private string _fullName = default!;
    private string _userName = default!;

    protected override async Task OnParametersSetAsync()
    {
        var authenticationState = await AuthenticationStateTask;

        _userName = "@" + authenticationState.User.FindFirst(ClaimTypes.Name)!.Value;

        var firstName = authenticationState.User.FindFirst(ClaimTypes.GivenName)!.Value;
        var lastName = authenticationState.User.FindFirst(ClaimTypes.Surname)!.Value;

        _fullName = $"{firstName} {lastName}";
    }
}