using Microsoft.AspNetCore.Components;

namespace FinanceLab.Client.Presentation.Components;

public partial class UserProfileComponent
{
    private string _avatar = default!;

    private string _fullName = default!;
    private string _gameDifficulty = default!;

    [Parameter]
    public string UserName { get; set; } = default!;

    protected override void OnParametersSet()
    {
        UserName = StateContainer.User!.UserName;
        _gameDifficulty = StateContainer.User.GameDifficulty.ToString();
        var firstName = StateContainer.User!.FirstName;
        var lastName = StateContainer.User!.LastName;
        _avatar = $"{firstName[0]}{lastName[0]}";
        _fullName = $"{firstName} {lastName}";
    }
}