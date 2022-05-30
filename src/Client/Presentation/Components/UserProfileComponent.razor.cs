using FinanceLab.Shared.Application.Constants;
using FinanceLab.Shared.Domain.Models.Dtos;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FinanceLab.Client.Presentation.Components;

public partial class UserProfileComponent
{
    private readonly DialogOptions _dialogOptions = new() { MaxWidth = MaxWidth.Medium, FullWidth = true };
    private string? _fullName;
    private string? _initials;
    private bool? _isAdmin;
    private bool? _isSelf;
    private UserDto? _user;

    [Parameter]
    public string UserName { get; set; } = default!;

    protected override async Task OnParametersSetAsync()
    {
        var query = $"userName={UserName}";
        var uri = $"{ApiRouteConstants.GetUser}?{query}";
        var response = await HttpClientService.GetAsync<UserDto>(uri);

        if (response.IsSuccessful)
        {
            _user = response.Data;

            var firstName = response.Data.FirstName;
            var lastName = response.Data.LastName;

            _initials = $"{firstName[0]}{lastName[0]}";
            _fullName = $"{firstName} {lastName}";

            _isAdmin = StateContainer.User?.Role is RoleConstants.Admin;
            _isSelf = StateContainer.User?.UserName == UserName;
        }
        else
        {
            ShowProblem(response.ProblemDetails, false);

            _fullName = default;
            _initials = default;
            _isAdmin = default;
            _isSelf = default;
            _user = default;
        }
    }

    private void OpenDepositDialog() => DialogService.Show<DepositComponent>(L["Deposit"], _dialogOptions);
    private void OpenNewGameDialog() => DialogService.Show<NewGameComponent>(L["NewGame"], _dialogOptions);
}