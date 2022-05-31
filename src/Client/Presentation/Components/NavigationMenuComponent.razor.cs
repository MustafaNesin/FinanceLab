namespace FinanceLab.Client.Presentation.Components;

public partial class NavigationMenuComponent
{
    private string? _fullName;
    private string? _userName;
    private string? _userUri;
    private string? _avatar;

    protected override async Task OnParametersSetAsync()
    {
        await AuthenticationStateTask;

        if (StateContainer.User is null)
            return;

        var firstName = StateContainer.User.FirstName;
        var lastName = StateContainer.User.LastName;
        var avatar =StateContainer.User.FirstName.Substring(0,1)+StateContainer.User.LastName.Substring(0,1)+" "; 

        _userName = StateContainer.User.UserName;
        _userUri = $"/Users/{_userName}";
        _fullName = $"{firstName} {lastName}";
        _avatar = $"{avatar}";
    }
    
}