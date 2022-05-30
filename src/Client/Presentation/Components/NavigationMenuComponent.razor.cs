namespace FinanceLab.Client.Presentation.Components;

public partial class NavigationMenuComponent
{
    private string _fullName = default!;
    private string _userName = default!;
    private string _userUri = default!;

    protected override async Task OnParametersSetAsync()
    {
        await AuthenticationStateTask;

        if (StateContainer.User is null)
            return;

        var firstName = StateContainer.User.FirstName;
        var lastName = StateContainer.User.LastName;

        _userName = StateContainer.User.UserName;
        _userUri = $"/Users/{_userName}";
        _fullName = $"{firstName} {lastName}";
    }
}