using JetBrains.Annotations;

namespace FinanceLab.Client.Presentation.Pages;

[UsedImplicitly]
public partial class IndexPage
{
    private string _fullName = default!;
    private string _userName = default!;

    protected override void OnParametersSet()
    {
        _userName = "@" + StateContainer.User!.UserName;

        var firstName = StateContainer.User!.FirstName;
        var lastName = StateContainer.User!.LastName;

        _fullName = $"{firstName} {lastName}";
    }
}