using FinanceLab.Shared.Application.Constants;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;

namespace FinanceLab.Client.Presentation.Pages;

[UsedImplicitly]
public partial class WalletListPage
{
    [Parameter]
    public string? UserName { get; set; }

    protected override void OnParametersSet()
    {
        if (UserName is not null &&
            UserName != StateContainer.User?.UserName &&
            StateContainer.User?.Role is not RoleConstants.Admin)
            NavigationManager.NavigateTo("/Wallets");
    }
}