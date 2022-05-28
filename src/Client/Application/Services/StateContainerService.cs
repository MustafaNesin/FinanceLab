using FinanceLab.Client.Application.Abstractions;
using FinanceLab.Shared.Domain.Models.Dtos;

namespace FinanceLab.Client.Application.Services;

public sealed class StateContainerService : IStateContainerService
{
    public event Action? StateHasChanged;
    public UserDto? User { get; private set; }

    public void SetUser(UserDto? user, bool notifyStateHasChanged = true)
    {
        User = user;

        if (notifyStateHasChanged)
            NotifyStateHasChanged();
    }

    private void NotifyStateHasChanged() => StateHasChanged?.Invoke();
}