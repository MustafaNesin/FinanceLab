using FinanceLab.Client.Domain.Models;
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
        UserName ??= StateContainer.User?.UserName;

        if (StateContainer.User?.UserName == UserName ||
            StateContainer.User?.Role is RoleConstants.Admin)
            return;
        
        ShowProblem(new ProblemDetails { Title = "You're not authorized to view this page."}, false);
        NavigationManager.NavigateTo("/Wallets");
    }
}